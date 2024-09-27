using FBChamp.Core.Entities.Soccer;
using FBChamp.Core.UnitOfWork;
using FBChamp.DataGenerators;
using FBChamp.Infrastructure;
using IntegrationTests.Helpers;

namespace IntegrationTests.CoachTests.AssignmentInfoTests;

[TestClass]
public class CoachAssignmentInfoRepositoryTests
{
    private IUnitOfWork? _unitOfWork;
    private DataGenerator? _dataGenerator;

    [TestInitialize]
    public void Initialize()
    {
        Infrastructure.CleanUp();
        _unitOfWork = new UnitOfWork();
        _dataGenerator = new DataGenerator(_unitOfWork);

        _dataGenerator.GenerateTeam(new Dictionary<string, string>
        {
            { "Count", "1" }
        });

        _dataGenerator.GenerateCoach(new Dictionary<string, string>
        {
            { "Count", "1" }
        });
    }

    [TestMethod]
    public void AddCoachAssignmentInfoTest()
    {
        var coach = _unitOfWork!.GetUnassignedCoachModels().First().Coach;
        var team = _unitOfWork.GetAllTeamModels().First().Team;
        var initialCount = _unitOfWork.GetAssignedCoachModels(team.Id).Count;
        var coachAssignmentInfo = new CoachAssignmentInfo(coach.Id, team.Id, "Coach");

        _unitOfWork.Commit(coachAssignmentInfo);

        var resultCount = _unitOfWork.GetAssignedCoachModels(team.Id).Count;

        Assert.AreEqual(initialCount + 1, resultCount);
    }

    [TestMethod]
    public void GetCoachAssignmentInfoTest()
    {
        var coach = _unitOfWork!.GetUnassignedCoachModels().First().Coach;
        var team = _unitOfWork.GetAllTeamModels().First().Team;
        var initialCount = _unitOfWork.GetAssignedCoachModels(team.Id).Count;
        var coachAssignmentInfo = new CoachAssignmentInfo(coach.Id, team.Id, "Coach");

        _unitOfWork.Commit(coachAssignmentInfo);

        var result = _unitOfWork.GetAssignedCoachModels(team.Id).Where(c => c.Role.Equals("Coach")).First();

        Assert.AreEqual(coach.Id, result.Coach.Id);
        Assert.AreEqual(coach.FullName, result.Coach.FullName);
        Assert.AreEqual(coach.BirthDate, result.Coach.BirthDate);
        Assert.AreEqual(coach.Photo, result.Coach.Photo);
        Assert.AreEqual(coach.Description, result.Coach.Description);
    }

    [TestMethod]
    public void RemoveCoachAssignmentInfoTest()
    {
        var coach = _unitOfWork!.GetUnassignedCoachModels().First().Coach;
        var team = _unitOfWork.GetAllTeamModels().First().Team;
        var coachAssignmentInfo = new CoachAssignmentInfo(coach.Id, team.Id, "Coach");

        _unitOfWork.Commit(coachAssignmentInfo);

        var initialCount = _unitOfWork.GetAssignedCoachModels(team.Id).Count;

        _unitOfWork.Remove(coach.Id, typeof(CoachAssignmentInfo));

        var result = _unitOfWork.GetAssignedCoachModels(team.Id).Count;

        Assert.AreEqual(initialCount - 1, result);
    }
}