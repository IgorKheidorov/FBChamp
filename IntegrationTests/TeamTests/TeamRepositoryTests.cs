using FBChamp.Core.Entities.Soccer;
using FBChamp.Infrastructure;
using IntegrationTests.Helpers;

namespace IntegrationTests.TeamTests;

[TestClass]
public class TeamRepositoryTests
{
    [TestMethod]
    public void AddTeams()
    {
        UnitOfWork unitOfWork = new UnitOfWork();
        var initialCount = unitOfWork.GetAllTeamModels().Count;

        Guid teamId = Guid.NewGuid();
        Guid locationId = Guid.NewGuid();
        Guid stadiumId = Guid.NewGuid();
        var photoGenerator = new PhotoGenerator();
        var photo = photoGenerator.Generate(300, 500);

        List<Task> tasks = Enumerable.Range(0, 100)
            .Select(x => new Team(teamId, "Name" + x, locationId, photo, stadiumId))
            .Select(x => new Task(() =>
            {
                unitOfWork.Commit(x);
            })).ToList();

        tasks.ForEach(x => x.Start());
        Task.WaitAll(tasks.ToArray());

        var realCount = unitOfWork.GetAllTeamModels().Count;

        Assert.AreEqual(initialCount + 1, realCount);
    }

    [TestMethod]
    public void GetTeamById()
    {
        UnitOfWork unitOfWork = new UnitOfWork();
        var teamId = Guid.NewGuid();
        Guid locationId = Guid.NewGuid();
        Guid stadiumId = Guid.NewGuid();
        var photoGenerator = new PhotoGenerator();
        var photo = photoGenerator.Generate(300, 500);

        var team = new Team(teamId, "Name", locationId, photo, stadiumId);
        unitOfWork.Commit(team);
        var retrievedTeam = unitOfWork.GetTeamModel(teamId);

        Assert.IsNotNull(retrievedTeam);
        Assert.AreEqual(teamId, retrievedTeam.Team.Id);
        Assert.AreEqual("Name", retrievedTeam.Team.Name);
        Assert.AreEqual(photo, retrievedTeam.Team.Photo);
    }

    [TestMethod]
    public void UpdateTeam()
    {
        UnitOfWork unitOfWork = new UnitOfWork();
        var teamId = Guid.NewGuid();
        Guid locationId = Guid.NewGuid();
        Guid stadiumId = Guid.NewGuid();
        var photoGenerator = new PhotoGenerator();
        var photo = photoGenerator.Generate(300, 500);

        var team = new Team(teamId, "Name", locationId, photo, stadiumId);
        unitOfWork.Commit(team);
        var retrievedTeam = unitOfWork.GetTeamModel(teamId);

        Assert.AreEqual("Name", retrievedTeam.Team.Name);

        retrievedTeam.Team.Name = "NewName";

        unitOfWork.Commit(retrievedTeam.Team);
        var updatedTeam = unitOfWork.GetTeamModel(teamId);

        Assert.IsNotNull(updatedTeam);
        Assert.AreEqual("NewName", updatedTeam.Team.Name);
    }

    [TestMethod]
    public void DeleteTeam()
    {
        UnitOfWork unitOfWork = new UnitOfWork();
        var teamId = Guid.NewGuid();
        Guid locationId = Guid.NewGuid();
        Guid stadiumId = Guid.NewGuid();
        var photoGenerator = new PhotoGenerator();
        var photo = photoGenerator.Generate(300, 500);

        var team = new Team(teamId, "Name", locationId, photo, stadiumId);
        unitOfWork.Commit(team);
        var initialCount = unitOfWork.GetAllTeamModels().Count;

        unitOfWork.Remove(teamId, typeof(Team));
        var realCount = unitOfWork.GetAllTeamModels().Count;

        Assert.AreEqual(initialCount - 1, realCount);
    }
}