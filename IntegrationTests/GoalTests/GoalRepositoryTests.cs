using FBChamp.Core.Entities.Soccer;
using FBChamp.Core.UnitOfWork;
using FBChamp.DataGenerators;
using FBChamp.Infrastructure;

namespace IntegrationTests.GoalTests;

[TestClass]
public class GoalRepositoryTests
{
    private IUnitOfWork? _unitOfWork;
    private DataGenerator? _dataGenerator;

    [TestInitialize]
    public void Initialize()
    {
        Infrastructure.CleanUp();

        _unitOfWork = new UnitOfWork();
        _dataGenerator = new DataGenerator(_unitOfWork);
    }

    [TestMethod]
    public void AddGoal_ShouldAddGoalSuccessfully()
    {
        var initialCount = _unitOfWork!.GetAllGoalModels().Count;

        _dataGenerator!.GenerateGoal(new Dictionary<string, string>
        {
            {"Count","1" }
        });

        var result = _unitOfWork.GetAllGoalModels().Count;
        Assert.AreEqual(initialCount + 1, result);
    }

    [TestMethod]
    public void GetGoalById_ShouldReturnCorrectIdOfGoal()
    {
        _dataGenerator!.GenerateGoal(new Dictionary<string, string>
        {
            {"Count","1" }
        });

        var goals = _unitOfWork!.GetAllGoalModels();
        var goal = goals.First();

        var result = _unitOfWork.GetGoalModel(goal.Goal.Id);

        Assert.IsNotNull(result);
        Assert.AreEqual(goal.Goal.GoalAuthorId, result.Goal.GoalAuthorId);
        Assert.AreEqual(goal.Goal.MatchId, result.Goal.MatchId);
        Assert.AreEqual(goal.Goal.ScoringTeamId, result.Goal.ScoringTeamId);
        Assert.AreEqual(goal.Goal.Type, result.Goal.Type);
    }

    [TestMethod]
    public void UpdateGoal_ShouldUpdateGoalSuccessfully()
    {
        _dataGenerator!.GenerateGoal(new Dictionary<string, string>
        {
            {"Count","1" }
        });

        var goals = _unitOfWork!.GetAllGoalModels();
        var goal = goals.First();
        var matches = _unitOfWork.GetAllMatchModels();
        var match = matches.First();

        goal.Goal.ScoringTeamId = match.HostTeam.Team.Id;
        goal.Goal.AssistantIds = new List<Guid> { match.HostTeam.Players.First().Player.Id };
        goal.Goal.GoalAuthorId = match.HostTeam.Players.Last().Player.Id;  

        var result = _unitOfWork.Commit(goal.Goal);

        Assert.AreEqual(CRUDResult.Success, result);
        Assert.AreEqual(goal.Goal.ScoringTeamId, goal.Match.HostTeamId);
    }

    [TestMethod]
    public void RemoveGoalTest()
    {
        _dataGenerator!.GenerateGoal(new Dictionary<string, string>
        {
            {"Count","1" }
        });

        var goals = _unitOfWork!.GetAllGoalModels();
        var goal = goals.First();

        var initialCount = _unitOfWork!.GetAllGoalModels().Count;

        _unitOfWork.Remove(goal.Goal.Id, typeof(Goal));

        var resultCount = _unitOfWork.GetAllGoalModels().Count;
        Assert.AreEqual(initialCount - 1, resultCount);
    }
}