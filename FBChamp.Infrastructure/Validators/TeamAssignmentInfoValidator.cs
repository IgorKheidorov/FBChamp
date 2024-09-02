using FBChamp.Core.DataValidator;
using FBChamp.Core.Entities;
using FBChamp.Core.Entities.Soccer;
using FBChamp.Core.UnitOfWork;

namespace FBChamp.Infrastructure.Validators;

public class TeamAssignmentInfoValidator : IValidateEntity
{
    private IUnitOfWork _unitOfWork;

    public TeamAssignmentInfoValidator(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public Type GetValidatedType() => typeof(TeamAssignmentInfo);

    public CRUDResult Validate(Entity entity)
    {
        if (entity is not TeamAssignmentInfo assignmentInfo)
        {
            return CRUDResult.InvalidOperation;
        }

        var league = _unitOfWork.GetLeagueModel(assignmentInfo.LeagueId);

        if (league is null || _unitOfWork.GetTeamModel(assignmentInfo.Id) is null)
        {
            return CRUDResult.InvalidOperation;
        }

        var assignedTeams = _unitOfWork.GetAssignedTeamsModels(assignmentInfo.LeagueId);

        if (assignedTeams.Count >= league.League.NumberOfTeams)
        {
            return CRUDResult.Failed;
        }

        return CRUDResult.Success;
    }
}
