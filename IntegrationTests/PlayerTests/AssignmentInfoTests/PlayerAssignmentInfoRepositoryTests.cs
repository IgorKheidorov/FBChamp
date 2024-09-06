using FBChamp.Core.Entities.Soccer;
using FBChamp.Infrastructure;
using IntegrationTests.Helpers;

namespace IntegrationTests.PlayerTests.AssignmentInfoTests;

[TestClass]
public class PlayerAssignmentInfoRepositoryTests
{
    [TestInitialize]
    public void Initialize()
    {
        Infrastructure.CleanUp();
    }

    [TestMethod]
    public void AddPlayerAssignmentInfoTest()
    {
        var unitOfWork = new UnitOfWork();
        var photoGenerator = new PhotoGenerator();
        var photo = photoGenerator.Generate(300, 500);

        var positionId = unitOfWork.GetAllPlayerPositions().First().Id;
        var player = new Player(Guid.NewGuid(), "Player", new DateTime(2000, 1, 1), 180, positionId, photo);
        var team = new Team(Guid.NewGuid(), "Team", Guid.NewGuid(), photo, Guid.NewGuid(), "description");
        var playerAssignmentInfo = new PlayerAssignmentInfo(player.Id, team.Id, 10);

        var initialCount = unitOfWork.GetAssignedPlayerModels(team.Id).Count;

        unitOfWork.Commit(player);
        unitOfWork.Commit(team);
        unitOfWork.Commit(playerAssignmentInfo);

        var resultCount = unitOfWork.GetAssignedPlayerModels(team.Id).Count;

        Assert.AreEqual(initialCount + 1, resultCount);
    }

    [TestMethod]
    public void GetPlayerAssignmentInfoModelsTest()
    {
        var unitOfWork = new UnitOfWork();
        var photoGenerator = new PhotoGenerator();
        var photo = photoGenerator.Generate(300, 500);

        var positionId = unitOfWork.GetAllPlayerPositions().First().Id;
        var player = new Player(Guid.NewGuid(), "Player", new DateTime(2000, 1, 1), 180, positionId, photo);
        var player2 = new Player(Guid.NewGuid(), "Player2", new DateTime(2000, 1, 1), 180, positionId, photo);
        var team = new Team(Guid.NewGuid(), "Team", Guid.NewGuid(), photo, Guid.NewGuid(), "description");
        var playerAssignmentInfo = new PlayerAssignmentInfo(player.Id, team.Id, 10);
        var playerAssignmentInfo2 = new PlayerAssignmentInfo(player2.Id, team.Id, 11);

        unitOfWork.Commit(player);
        unitOfWork.Commit(player2);
        unitOfWork.Commit(team);
        unitOfWork.Commit(playerAssignmentInfo);
        unitOfWork.Commit(playerAssignmentInfo2);

        var result = unitOfWork.GetAssignedPlayerModels(team.Id);

        Assert.AreEqual(2, result.Count);
    }

    [TestMethod]
    public void UpdatePlayerAssignmentInfoTest()
    {
        var unitOfWork = new UnitOfWork();
        var photoGenerator = new PhotoGenerator();
        var photo = photoGenerator.Generate(300, 500);

        var positionId = unitOfWork.GetAllPlayerPositions().First().Id;
        var player = new Player(Guid.NewGuid(), "Player", new DateTime(2000, 1, 1), 180, positionId, photo);
        var team = new Team(Guid.NewGuid(), "Team", Guid.NewGuid(), photo, Guid.NewGuid(), "description");
        var playerAssignmentInfo = new PlayerAssignmentInfo(player.Id, team.Id, 10);

        unitOfWork.Commit(player);
        unitOfWork.Commit(team);
        unitOfWork.Commit(playerAssignmentInfo);

        var beforeUpdate = unitOfWork.GetAssignedPlayerModels(team.Id).First();

        playerAssignmentInfo.PlayingNumber = 5;
        unitOfWork.Commit(playerAssignmentInfo);

        var afterUpdate = unitOfWork.GetAssignedPlayerModels(team.Id).First();

        Assert.AreEqual("10", beforeUpdate.PlayingNumber);
        Assert.AreEqual("5", afterUpdate.PlayingNumber);
    }

    [TestMethod]
    public void RemovePlayerAssignmentInfoTest()
    {
        var unitOfWork = new UnitOfWork();
        var photoGenerator = new PhotoGenerator();
        var photo = photoGenerator.Generate(300, 500);

        var positionId = unitOfWork.GetAllPlayerPositions().First().Id;
        var player = new Player(Guid.NewGuid(), "Player", new DateTime(2000, 1, 1), 180, positionId, photo);
        var team = new Team(Guid.NewGuid(), "Team", Guid.NewGuid(), photo, Guid.NewGuid(), "description");
        var playerAssignmentInfo = new PlayerAssignmentInfo(player.Id, team.Id, 10);

        unitOfWork.Commit(player);
        unitOfWork.Commit(team);
        unitOfWork.Commit(playerAssignmentInfo);

        var initialCount = unitOfWork.GetAssignedPlayerModels(team.Id).Count;

        unitOfWork.Remove(player.Id, typeof(PlayerAssignmentInfo));

        var result = unitOfWork.GetAssignedPlayerModels(team.Id).Count;
        Assert.AreEqual(initialCount - 1, result);
    }
}