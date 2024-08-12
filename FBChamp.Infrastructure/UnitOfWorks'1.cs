using FBChamp.Core.Entities.Socker;
using FBChamp.Core.Entities;
using FBChamp.Core.Repositories.Membership;
using FBChamp.Infrastructure.Repositories.Membership;
using Microsoft.IdentityModel.Tokens;
using FBChamp.Core.UnitOfWork;
using FBChamp.Core.DataValidator;
using FBChamp.Infrastructure.Validators;

namespace FBChamp.Infrastructure;

public sealed partial class UnitOfWork : IUnitOfWork 
{
    private bool _isDisposed;

    private IValidateEntity _crudDataValidator;

    private PlayerRepository _playerRepository;
    private PlayerPositionsRepository _playerPositionRepository;
    private CoachRepository _coachRepository;
    private TeamRepository _teamRepository;
    private PlayerAssignmentInfoRepository _playerAssignmentInfoRepository;
    private CoachAssignmentInfoRepository _coachAssignmentInfoRepository;

    private IPlayerRepository PlayerRepository =>
        _playerRepository ??= new PlayerRepository();

    private IPlayerPositionsRepository PlayerPositionsRepository =>
        _playerPositionRepository ??= new PlayerPositionsRepository();

    private ICoachRepository CoachRepository =>
       _coachRepository ??= new CoachRepository();

    private ITeamRepository TeamRepository =>
        _teamRepository ??= new TeamRepository();

    private IPlayerAssignmentInfoRepository PlayerAssignmentInfoRepository =>
        _playerAssignmentInfoRepository ??= new PlayerAssignmentInfoRepository();

    private ICoachAssignmentInfoRepository CoachAssignmentInfoRepository =>
        _coachAssignmentInfoRepository ??= new CoachAssignmentInfoRepository();
    
    public UnitOfWork()
    {
        _crudDataValidator = new CRUDDataValidator(this);
    }

    // Reload entities due to fail of repositories changes
    private CRUDResult RequestReload()
    {
       _playerPositionRepository = null;
       _coachRepository = null;
       _teamRepository = null;
       _playerAssignmentInfoRepository = null;
       _coachAssignmentInfoRepository = null;
        // false is to indicate the fail
        return CRUDResult.Failed;
    }

    private CRUDResult Commit()
    {
        try
        {
            PlayerRepository.Commit();
            CoachRepository.Commit();
            TeamRepository.Commit();
            PlayerAssignmentInfoRepository.Commit();
            CoachAssignmentInfoRepository.Commit();
            return CRUDResult.Success;
        }
        catch (Exception)
        {
            return CRUDResult.Failed;
        }
    }

    private bool RemovePlayer(Guid playerId) =>
        PlayerRepository.Remove(playerId) && 
        (PlayerAssignmentInfoRepository.Find(playerId) is not null ? DeassignPlayer(playerId) : true);

    private bool RemoveCoach(Guid coachId) =>
        CoachAssignmentInfoRepository.Remove(coachId) &&
        (CoachAssignmentInfoRepository.Find(coachId) is not null ? CoachAssignmentInfoRepository.Remove(coachId) : true);

    private bool RemoveTeam(Guid teamId) 
    {
        var team = TeamRepository.Find(teamId);
        if (team is null)
        {
            return false;
        }

        var assignedCoaches = GetAssignedCoachIds(teamId);
        var assignedPlayers = GetAssignedPlayerIds(teamId);

        var coachDeassignResult = assignedCoaches.IsNullOrEmpty() ? true :
            assignedCoaches.Select(x => DeassignCoach(x)).Where(x => x == false).Count() == 0;

        var playerDeassignResult = assignedPlayers.IsNullOrEmpty() ? true :
            assignedPlayers.Select(x => DeassignPlayer(x)).Where(x => x == false).Count() == 0;

        return coachDeassignResult && playerDeassignResult && TeamRepository.Remove(teamId);
    }

    public CRUDResult Remove(Guid id, Type type) =>  type.Name switch
    {
        "Player" => RemovePlayer(id),
        "Coach" => RemoveCoach(id),
        "Team" => RemoveTeam(id),
        "PlayerAssignmentInfo" => DeassignPlayer(id),
        _ => false
    } ? Commit() : RequestReload();
    
    public void Dispose()
    {
        _isDisposed = true;
    }

    private bool AddOrUpdateEntity(Entity entity) => entity.GetType().Name
        switch
    {
        "Team" => TeamRepository.AddOrUpdate(entity as Team),
        "Player" => PlayerRepository.AddOrUpdate(entity as Player),
        "Coach" => CoachRepository.AddOrUpdate(entity as Coach),
        "PlayerAssignmentInfo" => PlayerAssignmentInfoRepository.AddOrUpdate(entity as PlayerAssignmentInfo),
        "CoachAssignmentInfo" => CoachAssignmentInfoRepository.AddOrUpdate(entity as CoachAssignmentInfo),
        _ => false
    };

    public CRUDResult Commit(Entity entity)
    {
        var validationResult = _crudDataValidator.Validate(entity);
        if (validationResult != CRUDResult.Success)
        {
            return validationResult;
        }
        
        return AddOrUpdateEntity(entity as Entity<Guid>) ? Commit() : CRUDResult.Failed;
    }
    

}
