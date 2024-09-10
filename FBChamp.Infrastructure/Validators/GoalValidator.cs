using FBChamp.Core.DALModels;
using FBChamp.Core.DataValidator;
using FBChamp.Core.Entities;
using FBChamp.Core.Entities.Soccer;
using FBChamp.Core.Entities.Soccer.Enums;
using FBChamp.Core.UnitOfWork;

namespace FBChamp.Infrastructure.Validators;

public class GoalValidator(IUnitOfWork unitOfWork) : IValidateEntity
{
    public Type GetValidatedType() => typeof(Goal);

    public CRUDResult Validate(Entity entity) =>
        entity switch
        {
            Goal goal when Validate(goal) => CRUDResult.Success,
            Goal => CRUDResult.EntityValidationFailed,
            _ => CRUDResult.InvalidOperation
        };

    private bool Validate(Goal goal) =>
        ValidateMatch(goal.MatchId) &&
        ValidateGoal(goal);

    // Validates if the goal meets the conditions based on its type
    private bool ValidateGoal(Goal goal) =>
        goal.Type switch
        {
            GoalType.Auto when ValidateAutoGoal(goal) => true,
            GoalType.Normal when ValidateNormalGoal(goal) => true,
            GoalType.Penalty when ValidatePenaltyGoal(goal) => true,
            GoalType.Technical when ValidateTechnicalGoal(goal) => true,
            _ => false
        };

    // Validates if the MatchId is valid and the match exists in the database
    private bool ValidateMatch(Guid matchId) =>
        matchId != Guid.Empty && unitOfWork.Exists(matchId, typeof(Match));

    // Validates if the player is assigned to the match and was participating at the time of the goal
    private bool ValidatePlayer(PlayerModel player, Goal goal)
    {
        var playerAssignmentToMatch = unitOfWork.GetPlayerMatchAssignmentModel(player.Player.Id);

        if (playerAssignmentToMatch is null)
        {
            return false;
        }

        return playerAssignmentToMatch.PlayerMatchAssignment.MatchId.Equals(goal.MatchId) &&
               goal.Time >= playerAssignmentToMatch.PlayerMatchAssignment.StartTime &&
               goal.Time <= playerAssignmentToMatch.PlayerMatchAssignment.FinishTime;
    }

    // Validates if all assistants are from the same team as the goal author
    private bool ValidateAssistants(Goal goal)
    {
        var goalAuthor = unitOfWork.GetPlayerModel(goal.GoalAuthorId);
        var assistants = unitOfWork.GetAllPlayerModels().Where(x => goal.AssistantIds.Contains(x.Player.Id)).ToList();

        return !assistants.Any() ||
               assistants.All(assistant => string.Equals(goalAuthor.CurrentTeam, assistant.CurrentTeam));
    }

    // Validates if both assistants and the goal author are participating in the match at the time of the goal
    private bool ValidatePlayersInMatch(Goal goal)
    {
        var goalAuthor = unitOfWork.GetPlayerModel(goal.GoalAuthorId);
        var assistants = unitOfWork.GetAllPlayerModels().Where(x => goal.AssistantIds.Contains(x.Player.Id)).ToList();

        return assistants.All(assistant => ValidatePlayer(assistant, goal)) &&
               ValidatePlayer(goalAuthor, goal);
    }

    // Validates if the auto goal is valid (scored against own team, no assistants)
    private bool ValidateAutoGoal(Goal goal)
    {
        var goalAuthor = unitOfWork.GetPlayerModel(goal.GoalAuthorId);
        var scoringTeam = unitOfWork.GetTeamModel(goal.ScoringTeamId);

        return !string.Equals(scoringTeam.FullName, goalAuthor.CurrentTeam) &&
               !goal.AssistantIds.Any() &&
               ValidatePlayer(goalAuthor, goal);
    }

    // Validates if the normal goal is valid (scored by the right team, all players participated in the match)
    private bool ValidateNormalGoal(Goal goal)
    {
        var goalAuthor = unitOfWork.GetPlayerModel(goal.GoalAuthorId);
        var scoringTeam = unitOfWork.GetTeamModel(goal.ScoringTeamId);

        return string.Equals(scoringTeam.FullName, goalAuthor.CurrentTeam) &&
               ValidatePlayersInMatch(goal) &&
               ValidateAssistants(goal);
    }

    // Validates if the penalty goal is valid (scored by the right team, no assistants)
    private bool ValidatePenaltyGoal(Goal goal)
    {
        var goalAuthor = unitOfWork.GetPlayerModel(goal.GoalAuthorId);
        var scoringTeam = unitOfWork.GetTeamModel(goal.ScoringTeamId);

        return string.Equals(scoringTeam.FullName, goalAuthor.CurrentTeam) &&
               !goal.AssistantIds.Any() &&
               ValidatePlayer(goalAuthor, goal);
    }

    // Validates if the technical goal is valid (no author, no assistants)
    private bool ValidateTechnicalGoal(Goal goal) =>
        goal.GoalAuthorId == Guid.Empty && !goal.AssistantIds.Any();
}