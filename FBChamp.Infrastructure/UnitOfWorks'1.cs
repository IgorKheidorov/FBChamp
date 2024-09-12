using System.Data;
using FBChamp.Core.DataValidator;
using FBChamp.Core.Entities;
using FBChamp.Core.Entities.Soccer;
using FBChamp.Core.Entities.Soccer.Enums;
using FBChamp.Core.Repositories;
using FBChamp.Core.Repositories.Membership;
using FBChamp.Core.UnitOfWork;
using FBChamp.Infrastructure.Repositories.Membership;
using FBChamp.Infrastructure.Validators;
using Microsoft.IdentityModel.Tokens;

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
    private LeagueRepository _leagueRepository;
    private TeamAssignmentInfoRepository _teamAssignmentInfoRepository;
    private LocationRepository _locationRepository;
    private StadiumRepository _stadiumRepository;
    private GoalRepository _goalRepository;
    private MatchRepository _matchRepository;
    private MatchStatisticsRepository _matchStatisticsRepository;
    private PlayerMatchAssignmentRepository _playerMatchAssignmentRepository;

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

    private ILeagueRepository LeagueRepository =>
        _leagueRepository ??= new LeagueRepository();

    private ITeamAssignmentInfoRepository TeamAssignmentInfoRepository =>
        _teamAssignmentInfoRepository ??=new TeamAssignmentInfoRepository();

    private ILocationRepository LocationRepository =>
        _locationRepository ??= new LocationRepository();

    private IStadiumRepository StadiumRepository =>
        _stadiumRepository ??= new StadiumRepository();

    private IMatchRepository MatchRepository =>
        _matchRepository ??= new MatchRepository();

    private IMatchStatisticsRepository MatchStatisticsRepository =>
        _matchStatisticsRepository ??= new MatchStatisticsRepository();

    private IPlayerMatchAssignmentRepository PlayerMatchAssignmentRepository =>
        _playerMatchAssignmentRepository ??=new PlayerMatchAssignmentRepository();

    private IGoalRepository GoalRepository =>
        _goalRepository ??= new GoalRepository();

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
        _leagueRepository = null;
        _teamAssignmentInfoRepository = null;
        _stadiumRepository = null;
        _goalRepository = null;
        _matchRepository = null;
        _matchStatisticsRepository = null;
        _playerPositionRepository = null;
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
            LeagueRepository.Commit();
            TeamAssignmentInfoRepository.Commit();
            StadiumRepository.Commit();
            GoalRepository.Commit();

            MatchRepository.Commit();
            MatchStatisticsRepository.Commit();
            PlayerMatchAssignmentRepository.Commit();
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
        CoachRepository.Remove(coachId) &&
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

    private bool DeassignTeams(Guid leagueId) =>
      TeamAssignmentInfoRepository.Remove(leagueId);

    private bool RemoveLeague(Guid leagueId)
    {
        var league = LeagueRepository.Find(leagueId);

        if (league == null)
        {
            return false;
        }

        var assignedTeams = GetAssignedTeamsId(leagueId);

        bool allTeamsDeassigned = assignedTeams.All(leagueId => DeassignTeams(leagueId));

        var unfinishedMatches = GetUnfinishedMatchesIds(leagueId);
        bool allMatchesRemoved = unfinishedMatches.All(matchId => RemoveMatch(matchId));

        return allTeamsDeassigned && allMatchesRemoved && LeagueRepository.Remove(leagueId);
    }

    private bool RemoveStadium(Guid stadiumId) =>
        StadiumRepository.Remove(stadiumId);

    private bool RemoveMatch(Guid matchId) =>
        MatchRepository.Remove(matchId);

    private bool RemoveMatchStatistics(Guid matchStatisticsId) =>
        MatchStatisticsRepository.Remove(matchStatisticsId);

    private bool RemoveGoal(Guid goalId) =>
        GoalRepository.Remove(goalId);

    public CRUDResult Remove(Guid id, Type type) => type.Name switch
    {
        "Player" => RemovePlayer(id),
        "Coach" => RemoveCoach(id),
        "Team" => RemoveTeam(id),
        "PlayerAssignmentInfo" => DeassignPlayer(id),
        "League" => RemoveLeague(id),
        "TeamAssignmentInfo" => DeassignTeam(id),
        "Match" => RemoveMatch(id),
        "CoachAssignmentInfo" => DeassignCoach(id),
        "Goal" => RemoveGoal(id),
        "MatchStatistics" => RemoveMatchStatistics(id),
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
        "League" => LeagueRepository.AddOrUpdate(entity as League),
        "TeamAssignmentInfo" => TeamAssignmentInfoRepository.AddOrUpdate(entity as TeamAssignmentInfo),
        "Stadium" => StadiumRepository.AddOrUpdate(entity as Stadium),
        "Goal" => GoalRepository.AddOrUpdate(entity as Goal),
        "Match" => MatchRepository.AddOrUpdate(entity as Match),
        "MatchStatistics" => MatchStatisticsRepository.AddOrUpdate(entity as MatchStatistics),
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

    public bool Exists(Guid id, Type type) =>
        type.Name switch
        {
            nameof(Player) => Exists(id, PlayerRepository),
            nameof(Team) => Exists(id, TeamRepository),
            nameof(Coach) => Exists(id, CoachRepository),
            nameof(PlayerPosition) => Exists(id, PlayerPositionsRepository),
            nameof(Location) => Exists(id, LocationRepository),
            nameof(Match) => Exists(id, MatchRepository),
            nameof(League) => Exists(id, LeagueRepository),
            nameof(Stadium) => Exists(id, StadiumRepository),
            _ => false
        };

    private bool Exists<T>(Guid id, IRepository<T> repository) where T : Entity<Guid>
    {
        if (id == Guid.Empty)
        {
            return false;
        }

        return repository.Find(x => x.Id == id) is not null;
    }
}
