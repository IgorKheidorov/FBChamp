using FBChamp.Core.DataValidator;
using FBChamp.Core.Entities;
using FBChamp.Core.Entities.Soccer;
using FBChamp.Core.UnitOfWork;

namespace FBChamp.Infrastructure.Validators;

public class MatchValidator(IUnitOfWork unitOfWork) : IValidateEntity
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public Type GetValidatedType() => typeof(Match);

    public CRUDResult Validate(Entity entity) =>
        entity switch
        {
            Match match when ValidateProperties(match) => CRUDResult.Success,
            Match _ => CRUDResult.EntityValidationFailed,
            _ => CRUDResult.InvalidOperation,
        };

    private bool ValidateProperties(Match match) =>
          IsTeamExists(match.HostTeamId) &&
          IsTeamExists(match.GuestTeamId) &&
          IsLeagueExists(match.LeagueId) &&
          IsStadiumExists(match.StadiumId) &&
          IsFinishTimeValid(match);

    private bool IsTeamExists(Guid teamId) =>
        _unitOfWork.Exists(teamId, typeof(Team));

    private bool IsLeagueExists(Guid leagueId) =>
        _unitOfWork.Exists(leagueId, typeof(League));

    private bool IsStadiumExists(Guid stadiumId) =>
        _unitOfWork.Exists(stadiumId, typeof(Stadium));

    private bool IsFinishTimeValid(Match match) => match.FinishTimeOfMatch == default || match.FinishTimeOfMatch > match.StartTimeOfMatch;
}
