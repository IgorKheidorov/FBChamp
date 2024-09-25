using FBChamp.Core.DataValidator;
using FBChamp.Core.Entities;
using FBChamp.Core.Entities.Soccer;
using FBChamp.Core.UnitOfWork;

namespace FBChamp.Infrastructure.Validators
{
    public class PlayerMatchAssignmentValidator(IUnitOfWork unitOfWork) : IValidateEntity
    {
        public Type GetValidatedType()
        {
            return typeof(PlayerMatchAssignment);
        }

        public CRUDResult Validate(Entity entity)
        {
            if (entity is null)
            {
                return CRUDResult.InvalidOperation;
            }

            if (entity is not PlayerMatchAssignment assignment)
            {
                return CRUDResult.InvalidOperation;
            }

            if(unitOfWork.GetPlayerModel(assignment.Id) is null)
            {
                return CRUDResult.ObjectDoesNotExists;
            }

            if(unitOfWork.GetMatchModel(assignment.MatchId) is null)
            {
                return CRUDResult.ObjectDoesNotExists;
            }

            if(assignment.StartTime > assignment.FinishTime)
            {
                return CRUDResult.EntityValidationFailed;
            }

            if (assignment.Role is null)
            {
                return CRUDResult.EntityValidationFailed;
            }

            return CRUDResult.Success;
        }
    }
}
