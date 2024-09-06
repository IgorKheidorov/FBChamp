using FBChamp.Core.DataValidator;
using FBChamp.Core.Entities;
using FBChamp.Core.Entities.Soccer;
using FBChamp.Core.UnitOfWork;
using FBChamp.Infrastructure.BusinessRules;

namespace FBChamp.Infrastructure.Validators;

public class PlayerAssignmentInfoValidator(IUnitOfWork unitOfWork) : IValidateEntity
{
    public Type GetValidatedType() => typeof(PlayerAssignmentInfo);

    public CRUDResult Validate(Entity entity)
    {
        if (entity is not PlayerAssignmentInfo assignmentInfo)
        {
            return CRUDResult.InvalidOperation;
        }

        var player = unitOfWork.GetPlayerModel(assignmentInfo.Id);
        var team = unitOfWork.GetTeamModel(assignmentInfo.TeamId);

        //Check for existing player and team
        if (player is null || team is null)
        {
            return CRUDResult.InvalidOperation;
        }

        // Only free players can be assigned
        if (!string.Equals(player.CurrentTeam, DataRestrictions.NoAssignmentString))
        {
            return CRUDResult.Failed;
        }

        // Check if playing number is already assigned
        if (IsPlayerNumberAssigned(assignmentInfo))
        {
            return CRUDResult.Failed;
        }

        return CRUDResult.Success;
    }

    private bool IsPlayerNumberAssigned(PlayerAssignmentInfo entity) =>
        unitOfWork.GetAssignedPlayerModels(entity.TeamId)
            .Any(x => string.Equals(x.PlayingNumber, entity.PlayingNumber.ToString()));
}