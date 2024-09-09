using FBChamp.Core.DataValidator;
using FBChamp.Core.Entities;
using FBChamp.Core.Entities.Soccer;
using FBChamp.Core.UnitOfWork;

namespace FBChamp.Infrastructure.Validators;

public class CoachAssignmentInfoValidator(IUnitOfWork unitOfWork) : IValidateEntity
{
    public Type GetValidatedType() => typeof(CoachAssignmentInfo);

    public CRUDResult Validate(Entity entity)
    {
        if (entity is not CoachAssignmentInfo assignmentInfo)
        {
            return CRUDResult.InvalidOperation;
        }

        var coach = unitOfWork.GetCoachModel(assignmentInfo.Id);
        var team = unitOfWork.GetTeamModel(assignmentInfo.TeamId);

        if (coach is null || team is null)
        {
            return CRUDResult.ObjectDoesNotExists;
        }

        if (assignmentInfo.Role is null &&
            team.Coach is not null &&
            string.Equals(assignmentInfo.Role, team.Coach.Role))
        {
            return CRUDResult.EntityValidationFailed;
        }

        return CRUDResult.Success;
    }
}