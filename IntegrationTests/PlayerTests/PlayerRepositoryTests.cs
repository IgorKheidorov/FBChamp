using System.Globalization;
using FBChamp.Core.Entities.Soccer;
using FBChamp.Core.UnitOfWork;
using FBChamp.DataGenerators;
using FBChamp.Infrastructure;
using IntegrationTests.Helpers;

namespace IntegrationTests.PlayerTests;

[TestClass]
public class PlayerRepositoryTests
{
    private readonly IUnitOfWork _unitOfWork = new UnitOfWork();

    [TestInitialize]
    public void Initialize()
    {
        Infrastructure.CleanUp();

        var dataGenerator = new DataGenerator(_unitOfWork);
        dataGenerator.GeneratePlayer(new Dictionary<string, string>
        {
            { "Count", "2" }
        });
    }

    [TestMethod]
    public void AddPlayers()
    {
        var unitOfWork = new UnitOfWork();
        var photoGenerator = new PhotoGenerator();
        var initialCount = unitOfWork.GetAllPlayerModels().Count;

        var positionId = unitOfWork.GetAllPlayerPositions().First().Id;
        var playerId = Guid.NewGuid();
        var photo = photoGenerator.Generate(300, 500);

        var tasks = Enumerable.Range(0, 100)
            .Select(x =>
                new Player(playerId,
                    "Name" + x,
                    new DateTime(2000, 2, 1, new GregorianCalendar()),
                    180,
                    positionId,
                    photo))
            .Select(x =>
                new Task(() => { unitOfWork.Commit(x); })).ToList();

        tasks.ForEach(x => x.Start());
        Task.WaitAll(tasks.ToArray());

        var realCount = unitOfWork.GetAllPlayerModels().Count;

        Assert.AreEqual(initialCount + 1, realCount);
    }

    [TestMethod]
    public void GetPlayerById()
    {
        var unitOfWork = new UnitOfWork();
        var photoGenerator = new PhotoGenerator();

        var positionId = unitOfWork.GetAllPlayerPositions().First().Id;
        var playerId = Guid.NewGuid();
        var photo = photoGenerator.Generate(300, 500);
        var birthDate = new DateTime(2000, 2, 1);

        var player = new Player(playerId,
            "Name",
            birthDate,
            180,
            positionId,
            photo);

        unitOfWork.Commit(player);

        var retrievedPlayer = unitOfWork.GetPlayerModel(playerId);

        Assert.IsNotNull(retrievedPlayer);
        Assert.AreEqual(playerId, retrievedPlayer.Player.Id);
        Assert.AreEqual("Name", retrievedPlayer.FullName);
        Assert.AreEqual(birthDate, retrievedPlayer.Player.BirthDate);
        Assert.AreEqual(180, retrievedPlayer.Player.Height);
        Assert.AreEqual(positionId, retrievedPlayer.Player.PositionId);
        Assert.AreEqual(photo, retrievedPlayer.Player.Photo);
    }

    [TestMethod]
    public void UpdatePlayer()
    {
        var unitOfWork = new UnitOfWork();
        var photoGenerator = new PhotoGenerator();

        var positionId = unitOfWork.GetAllPlayerPositions().First().Id;
        var playerId = Guid.NewGuid();
        var photo = photoGenerator.Generate(300, 500);
        var birthDate = new DateTime(2000, 2, 1);

        var player = new Player(playerId,
            "NameBeforeUpdate",
            birthDate,
            180,
            positionId,
            photo);

        unitOfWork.Commit(player);
        var retrievedPlayer = unitOfWork.GetPlayerModel(playerId);

        Assert.AreEqual("NameBeforeUpdate", retrievedPlayer.FullName);
        Assert.AreEqual(180, retrievedPlayer.Player.Height);

        retrievedPlayer.Player.FullName = "NameAfterUpdate";
        retrievedPlayer.Player.Height = 205;

        unitOfWork.Commit(retrievedPlayer.Player);
        var updatedPlayer = unitOfWork.GetPlayerModel(playerId);

        Assert.IsNotNull(updatedPlayer);
        Assert.AreEqual("NameAfterUpdate", updatedPlayer.FullName);
        Assert.AreEqual(205, updatedPlayer.Player.Height);
    }

    [TestMethod]
    public void RemovePlayer()
    {
        var unitOfWork = new UnitOfWork();
        var photoGenerator = new PhotoGenerator();

        var positionId = unitOfWork.GetAllPlayerPositions().First().Id;
        var playerId = Guid.NewGuid();
        var photo = photoGenerator.Generate(300, 500);
        var birthDate = new DateTime(2000, 2, 1);

        var player = new Player(playerId,
            "NameBeforeUpdate",
            birthDate,
            180,
            positionId,
            photo);

        unitOfWork.Commit(player);
        var initialCount = unitOfWork.GetAllPlayerModels().Count;

        unitOfWork.Remove(playerId, typeof(Player));
        var resultCount = unitOfWork.GetAllPlayerModels().Count;

        Assert.AreEqual(initialCount - 1, resultCount);
    }
}