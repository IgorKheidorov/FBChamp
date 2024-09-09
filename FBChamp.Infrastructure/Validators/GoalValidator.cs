using FBChamp.Core.DataValidator;
using FBChamp.Core.Entities;
using FBChamp.Core.Entities.Soccer;
using FBChamp.Core.UnitOfWork;

namespace FBChamp.Infrastructure.Validators;

public class GoalValidator(IUnitOfWork unitOfWork) : IValidateEntity
{
    public Type GetValidatedType() => typeof(Goal);

    public CRUDResult Validate(Entity entity) =>
        entity switch
        {
            Goal goal when ValidateProperties(goal) => CRUDResult.Success,
            Goal => CRUDResult.EntityValidationFailed,
            _ => CRUDResult.InvalidOperation
        };

    // TODO: Uncomment when Match CRUD is introduced
    private bool ValidateProperties(Goal goal) =>
        //ValidateMatch(goal.MatchId) &&
        ValidateGoalAuthor(goal.GoalAuthorId);

    private bool ValidateGoalAuthor(Guid goalAuthorId) =>
        goalAuthorId != Guid.Empty && unitOfWork.Exists(goalAuthorId, typeof(Player));

    private bool ValidateMatch(Guid matchId) =>
        matchId != Guid.Empty && unitOfWork.Exists(matchId, typeof(Match));
}