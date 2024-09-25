using FBChamp.Core.Entities.Soccer.Enums;
using FBChamp.Core.UnitOfWork;
using FBChamp.DataGenerators;
using FBChamp.Infrastructure;

namespace IntegrationTests.GoalTests;

[TestClass]
public class GoalValidatorTests
{
    private IUnitOfWork? _unitOfWork;
    private DataGenerator? _dataGenerator;

    [TestInitialize]
    public void Initialize()
    {
        Infrastructure.CleanUp();

        _unitOfWork = new UnitOfWork();
        _dataGenerator = new DataGenerator(_unitOfWork);

        _dataGenerator.GenerateGoal(new Dictionary<string, string>
        {
            { "Count", "1" }
        });
    }

    [TestMethod]
    public void AddGoal_ShouldReturnEntityValidationFailed_WhenMatchDoesNotExists()
    {
        var goals = _unitOfWork!.GetAllGoalModels();
        var goal = goals.First();

        goal.Match.Id = Guid.NewGuid();

        var result = _unitOfWork.Commit(goal.Goal);

        Assert.AreEqual(CRUDResult.EntityValidationFailed, result);
    }

    [TestMethod]
    public void AddGoal_ShouldReturnEntityValidationFailed_WhenAssistantsAreFromDifferentTeam()
    {
        var goal = _unitOfWork!.GetAllGoalModels().First();
        var goalAuthor = _unitOfWork.GetPlayerModel(goal.Goal.GoalAuthorId);

        goal.Goal.AssistantIds = _unitOfWork.GetAllPlayerModels()
            .Where(p => !p.CurrentTeam.Equals(goalAuthor.CurrentTeam))
            .Select(p => p.Player.Id)
            .ToList();

        var result = _unitOfWork.Commit(goal.Goal);

        Assert.AreEqual(CRUDResult.EntityValidationFailed, result);
    }

    [TestMethod]
    public void AddGoal_ShouldReturnEntityValidationFailed_WhenAnyPlayerIsNotParticipatingAtGoalTime()
    {
        var goal = _unitOfWork!.GetAllGoalModels().First();
        var playerAssignment = _unitOfWork.GetPlayerMatchAssignmentModel(goal.Goal.GoalAuthorId);

        goal.Goal.Time = playerAssignment.PlayerMatchAssignment.StartTime.AddHours(-1);

        var result = _unitOfWork.Commit(goal.Goal);

        Assert.AreEqual(CRUDResult.EntityValidationFailed, result);
    }

    [TestMethod]
    public void AddPenaltyGoal_ShouldReturnEntityValidationFailed_WhenGoalHasAssistants()
    {
        var goal = _unitOfWork!.GetAllGoalModels().First();

        goal.Goal.AssistantIds = new List<Guid> { Guid.NewGuid() };
        goal.Goal.Type = GoalType.Penalty;

        var result = _unitOfWork.Commit(goal.Goal);

        Assert.AreEqual(CRUDResult.EntityValidationFailed, result);
    }

    [TestMethod]
    public void AddPenaltyGoal_ShouldSucceed_WhenGoalHasNoAssistantsAndScoredByRightTeam()
    {
        var goal = _unitOfWork!.GetAllGoalModels().First();

        goal.Goal.AssistantIds.Clear();
        goal.Goal.Type = GoalType.Penalty;

        var result = _unitOfWork.Commit(goal.Goal);

        Assert.AreEqual(CRUDResult.Success, result);
    }

    [TestMethod]
    public void AddAutoGoal_ShouldReturnEntityValidationFailed_WhenGoalIsScoredBySameTeamAndGoalTypeIsNormal()
    {
        var goal = _unitOfWork!.GetAllGoalModels().First();
        var goalAuthor = _unitOfWork.GetPlayerModel(goal.Goal.GoalAuthorId);
        var match = _unitOfWork.GetAllMatchModels().First();

        goal.Goal.ScoringTeamId = match.HostTeam.Team.Id;
        goal.GoalAuthor.Id = goalAuthor.Player.Id;

        var result = _unitOfWork.Commit(goal.Goal);

        Assert.AreEqual(CRUDResult.EntityValidationFailed, result);
    }
}
