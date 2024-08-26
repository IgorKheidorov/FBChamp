using FBChamp.Core.DataValidator;
using FBChamp.Core.Entities;
using FBChamp.Core.Entities.Soccer;
using FBChamp.Core.UnitOfWork;

namespace FBChamp.Infrastructure.Validators;

public class PlayerAssignmentInfoValidator : IValidateEntity
{
    IUnitOfWork _unitOfWork;

    public PlayerAssignmentInfoValidator(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork; 
    
    public Type GetValidatedType() => typeof(PlayerAssignmentInfo);

    public CRUDResult Validate(Entity entity)
    {
        if (entity is PlayerAssignmentInfo assignmentInfo)
        {
            //Check for existing player and team
            var player = _unitOfWork.GetPlayerModel(assignmentInfo.Id);

            if (player is null || _unitOfWork.GetTeamModel(assignmentInfo.TeamId) is null) 
            {
                return CRUDResult.InvalidOperation;
            }

            // Only free players can be assigned
            if (player.CurrentTeam != "No assignment")
            {
                return CRUDResult.Failed;
            }

            // Check if playing number is already assigned
            if (_unitOfWork.GetAssignedPlayerModels(assignmentInfo.TeamId).
                Where(x => x.PlayingNumber == assignmentInfo.PlayingNumber.ToString()).
                Count() != 0)
                return CRUDResult.Failed;

            return CRUDResult.Success;
        }

        return CRUDResult.InvalidOperation;
    }
}
