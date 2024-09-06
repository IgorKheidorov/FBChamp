using FBChamp.Core.DataValidator;
using FBChamp.Core.Entities;
using FBChamp.Core.Entities.Soccer;
using FBChamp.Core.UnitOfWork;

namespace FBChamp.Infrastructure.Validators;

public class MatchValidator : IValidateEntity
{
    private IUnitOfWork _unitOfWork;

    public MatchValidator(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public Type GetValidatedType() => typeof(Match);

    public CRUDResult Validate(Entity entity) =>
        entity switch
        {
            Match match when ValidateProperties(match) => CRUDResult.Success,
            Match _ => CRUDResult.EntityValidationFailed,
            _ => CRUDResult.InvalidOperation,
        };

    private bool ValidateProperties(Match match)
    {
        if (!IsTeamExists(match.HostTeamId))
        {
            return false;
        }

        if (!IsTeamExists(match.GuestTeamId))
        {
            return false;
        }

        if (!IsLeagueExists(match.LeagueId))
        {
            return false;
        }

        if (!IsStadiumExists(match.StadiumId))
        {
            return false;
        }

        return true;
    }

    private bool IsTeamExists(Guid teamId) =>
        _unitOfWork.Exists(teamId, typeof(Team));

    private bool IsLeagueExists(Guid leagueId) =>
        _unitOfWork.Exists(leagueId, typeof(League));

    private bool IsStadiumExists(Guid stadiumId) =>
        _unitOfWork.Exists(stadiumId, typeof(Stadium));
}
