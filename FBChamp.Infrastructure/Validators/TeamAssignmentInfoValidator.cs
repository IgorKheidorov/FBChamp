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

    public CRUDResult Validate(Entity entity) =>
        entity switch
        {
            TeamAssignmentInfo teamAssignmentInfo when ValidateProperties(teamAssignmentInfo) => CRUDResult.Success,
            TeamAssignmentInfo _ => CRUDResult.EntityValidationFailed,
            _ => CRUDResult.InvalidOperation
        };

    private bool ValidateProperties(TeamAssignmentInfo teamAssignmentInfo) =>
        ValidateLeagueAndTeamExistence(teamAssignmentInfo) &&
        ValidateAssignedTeamsCount(teamAssignmentInfo);

    private bool ValidateLeagueAndTeamExistence(TeamAssignmentInfo assignmentInfo)
    {
        var league = _unitOfWork.GetLeagueModel(assignmentInfo.LeagueId);
        var team = _unitOfWork.GetTeamModel(assignmentInfo.Id);

        return league != null && team != null;
    }

    private bool ValidateAssignedTeamsCount(TeamAssignmentInfo assignmentInfo)
    {
        var league = _unitOfWork.GetLeagueModel(assignmentInfo.LeagueId);

        if (league == null)
        {
            return false;
        }

        var assignedTeams = _unitOfWork.GetAssignedTeamsModels(assignmentInfo.LeagueId);

        return assignedTeams.Count < league.League.NumberOfTeams;
    }
}
