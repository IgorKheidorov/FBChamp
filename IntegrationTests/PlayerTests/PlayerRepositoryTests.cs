using FBChamp.Core.Entities.Soccer;
using FBChamp.Core.UnitOfWork;
using FBChamp.DataGenerators;
using FBChamp.Infrastructure;

namespace IntegrationTests.PlayerTests;

[TestClass]
public class PlayerRepositoryTests
{
    private IUnitOfWork? _unitOfWork;
    private DataGenerator? _dataGenerator;


    [TestInitialize]
    public void Initialize()
    {
        Infrastructure.CleanUp();

        _unitOfWork = new UnitOfWork();
        _dataGenerator = new DataGenerator(_unitOfWork!);
    }

    [TestMethod]
    public void AddPlayers_ShouldAddPlayerSuccessfully()
    {
        var initialCount = _unitOfWork!.GetAllPlayerModels().Count;

        _dataGenerator!.GeneratePlayer(new Dictionary<string, string>
        {
            { "Count", "1" }
        });

        var realCount = _unitOfWork.GetAllPlayerModels().Count;

        Assert.AreEqual(initialCount + 1, realCount);
    }

    [TestMethod]
    public void GetPlayerById_ShouldReturnCorrectPlayerId()
    {
        _dataGenerator!.GeneratePlayer(new Dictionary<string, string>
        {
            { "Count", "1" }
        });

        var players = _unitOfWork!.GetAllPlayerModels();
        var player = players.First();

        var retrievedPlayer = _unitOfWork.GetPlayerModel(player.Player.Id);

        Assert.IsNotNull(retrievedPlayer);
        Assert.AreEqual(retrievedPlayer.Player.Id, player.Player.Id);
        Assert.AreEqual(retrievedPlayer.Player.FullName, player.Player.FullName);
        Assert.AreEqual(retrievedPlayer.Player.BirthDate,player.Player.BirthDate);
        Assert.AreEqual(retrievedPlayer.Player.PositionId,player.Player.PositionId);
        Assert.AreEqual(retrievedPlayer.Player.Photo,player.Player.Photo);
    }

    [TestMethod]
    public void UpdatePlayer_ShouldUpdatePlayerSuccessfully()
    {
        _dataGenerator!.GeneratePlayer(new Dictionary<string, string>
        {
            { "Count", "1" }
        });

        var players = _unitOfWork!.GetAllPlayerModels();
        var player = players.First();

        Assert.IsNotNull(player);
        Assert.AreEqual(player.Player.FullName, "Player0");
        Assert.AreEqual(player.Player.Height, 180);
        Assert.AreEqual(player.Player.Description, "No information");

        player.Player.FullName = "NameAfterUpdate";
        player.Player.Height = 200;
        player.Player.Description = "Description after update";

        _unitOfWork.Commit(player.Player);
        var updatedPlayer = _unitOfWork.GetPlayerModel(player.Player.Id);

        Assert.IsNotNull(updatedPlayer);
        Assert.AreEqual(updatedPlayer.Player.FullName, "NameAfterUpdate");
        Assert.AreEqual(updatedPlayer.Player.Height, 200);
        Assert.AreEqual(updatedPlayer.Player.Description, "Description after update");
    }

    [TestMethod]
    public void RemovePlayer_ShouldRemovePlayerSuccessfully()
    {
        var initialCount = _unitOfWork!.GetAllPlayerModels().Count;

        _dataGenerator!.GeneratePlayer(new Dictionary<string, string>
        {
            { "Count", "1" }
        });

        var players = _unitOfWork!.GetAllPlayerModels();
        var player = players.First();

        _unitOfWork.Remove(player.Player.Id,typeof(Player));

        var realCount = _unitOfWork.GetAllPlayerModels().Count;

        Assert.AreEqual(initialCount , realCount);
    }
}