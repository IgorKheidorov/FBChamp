using FBChamp.Core.Entities.Soccer;
using FBChamp.Infrastructure;
using IntegrationTests.Helpers;

namespace IntegrationTests.CoachTests.AssignmentInfoTests;

[TestClass]
public class CoachAssignmentInfoRepositoryTests
{
    [TestInitialize]
    public void Initialize()
    {
        Infrastructure.CleanUp();
    }

    [TestMethod]
    public void AddCoachAssignmentInfoTest()
    {
        var unitOfWork = new UnitOfWork();
        var photoGenerator = new PhotoGenerator();
        var photo = photoGenerator.Generate(300, 500);

        var coach = new Coach(Guid.NewGuid(), "Coach", new DateTime(2000, 1, 1), photo);
        var team = new Team(Guid.NewGuid(), "Team", Guid.NewGuid(), photo, Guid.NewGuid(), "description");
        var coachAssignmentInfo = new CoachAssignmentInfo(coach.Id, team.Id, "Coach");

        var initialCount = unitOfWork.GetAssignedCoachModels(team.Id).Count;

        unitOfWork.Commit(coach);
        unitOfWork.Commit(team);
        unitOfWork.Commit(coachAssignmentInfo);

        var resultCount = unitOfWork.GetAssignedCoachModels(team.Id).Count;

        Assert.AreEqual(initialCount + 1, resultCount);
    }

    [TestMethod]
    public void GetCoachAssignmentInfoTest()
    {
        var unitOfWork = new UnitOfWork();
        var photoGenerator = new PhotoGenerator();
        var photo = photoGenerator.Generate(300, 500);

        var coach = new Coach(Guid.NewGuid(), "Coach", new DateTime(2000, 1, 1), photo);
        var team = new Team(Guid.NewGuid(), "Team", Guid.NewGuid(), photo, Guid.NewGuid(), "description");
        var coachAssignmentInfo = new CoachAssignmentInfo(coach.Id, team.Id, "Coach");

        unitOfWork.Commit(coach);
        unitOfWork.Commit(team);
        unitOfWork.Commit(coachAssignmentInfo);

        var result = unitOfWork.GetAssignedCoachModel(team.Id);

        Assert.AreEqual(coach.Id, result.Coach.Id);
        Assert.AreEqual(coach.FullName, result.Coach.FullName);
        Assert.AreEqual(coach.BirthDate, result.Coach.BirthDate);
        Assert.AreEqual(coach.Photo, result.Coach.Photo);
        Assert.AreEqual(coach.Description, result.Coach.Description);
    }

    [TestMethod]
    public void RemoveCoachAssignmentInfoTest()
    {
        var unitOfWork = new UnitOfWork();
        var photoGenerator = new PhotoGenerator();
        var photo = photoGenerator.Generate(300, 500);

        var coach = new Coach(Guid.NewGuid(), "Coach", new DateTime(2000, 1, 1), photo);
        var team = new Team(Guid.NewGuid(), "Team", Guid.NewGuid(), photo, Guid.NewGuid(), "description");
        var coachAssignmentInfo = new CoachAssignmentInfo(coach.Id, team.Id, "Coach");

        unitOfWork.Commit(coach);
        unitOfWork.Commit(team);
        unitOfWork.Commit(coachAssignmentInfo);

        var initialCount = unitOfWork.GetAssignedCoachModels(team.Id).Count;

        unitOfWork.Remove(coach.Id, typeof(CoachAssignmentInfo));

        var result = unitOfWork.GetAssignedCoachModels(team.Id).Count;

        Assert.AreEqual(initialCount - 1, result);
    }
}