using FBChamp.Core.UnitOfWork;
using FBChamp.DataGenerators;
using IntegrationTests.Helpers;

namespace IntegrationTests.PlayerTests;

[TestClass]
public class PlayerValidatorTests
{
    private IUnitOfWork? _unitOfWork;
    private DataGenerator? _dataGenerator;
    private PhotoGenerator? _photoGenerator;

    [TestInitialize]
    public void Initialize()
    {
        Infrastructure.CleanUp();

        _dataGenerator = new DataGenerator(_unitOfWork!);
        _photoGenerator = new PhotoGenerator();

        _dataGenerator.GeneratePlayer(new Dictionary<string, string>
        {
            {"Count", "1" }
        });
    }

    [TestMethod]
    [DataRow(257)]
    [DataRow(300)]
    public void AddPlayer_ShouldReturnEntityValidationFailed_WhenNameIsInvalid(int nameLength)
    {
        var players = _unitOfWork!.GetAllPlayerModels();

        var player = players.First();

        player.Player.FullName = new string('a', nameLength);
        var result = _unitOfWork.Commit(player.Player);

        Assert.AreEqual(CRUDResult.EntityValidationFailed, result);
    }

    [TestMethod]
    [DataRow(255)]
    [DataRow(100)]
    [DataRow(20)]
    public void AddPlayer_ShouldReturnSuccess_WhenPlayerNameIsValid(int nameLength)
    {
        var players = _unitOfWork!.GetAllPlayerModels();

        var player = players.First();

        player.Player.FullName = new string('a', nameLength);
        var result = _unitOfWork.Commit(player.Player);

        Assert.AreEqual(CRUDResult.Success, result);
    }

    [TestMethod]
    [DataRow(1950, 2, 1)]
    [DataRow(2015, 2, 1)]
    public void AddPlayer_ShouldReturnEntityValidationFailed_WhenAgeIsInvalid(int year, int month, int day)
    {
        var players = _unitOfWork!.GetAllPlayerModels();

        var player = players.First();

        var birthDate = new DateTime(year, month, day);

        player.Player.BirthDate = birthDate;

        var result = _unitOfWork.Commit(player.Player);

        Assert.AreEqual(CRUDResult.EntityValidationFailed, result);
    }

    [TestMethod]
    [DataRow(2000, 2, 1)]
    [DataRow(1985, 2, 1)]
    [DataRow(2008, 2, 1)]
    public void AddPlayer_ShouldReturnSuccess_WhenAgeIsValid(int year, int month, int day)
    {
        var players = _unitOfWork!.GetAllPlayerModels();

        var player = players.First();

        var birthDate = new DateTime(year, month, day);

        player.Player.BirthDate = birthDate;

        var result = _unitOfWork.Commit(player.Player);

        Assert.AreEqual(CRUDResult.Success, result);
    }

    [TestMethod]
    [DataRow(251)]
    [DataRow(350)]
    [DataRow(115)]
    [DataRow(149)]
    public void AddPlayer_ShouldReturnEntityValidationFailed_WhenHeightIsInvalid(int height)
    {
        var players = _unitOfWork!.GetAllPlayerModels();

        var player = players.First();

        player.Player.Height = height;

        var result = _unitOfWork.Commit(player.Player);

        Assert.AreEqual(CRUDResult.EntityValidationFailed, result);
    }

    [TestMethod]
    [DataRow(170)]
    [DataRow(209)]
    public void AddPlayer_ShouldReturnSuccess_WhenHeightIsValid(int height)
    {
        var players = _unitOfWork!.GetAllPlayerModels();

        var player = players.First();

        player.Player.Height = height;

        var result = _unitOfWork.Commit(player.Player);

        Assert.AreEqual(CRUDResult.Success, result);
    }

    [TestMethod]
    [DataRow(257)]
    [DataRow(400)]
    public void AddPlayer_ShouldReturnEntityValidationFailed_WhenDescriptionLengthIsInvalid(int descriptionLength)
    {
        var players = _unitOfWork!.GetAllPlayerModels();

         var player = players.First();

        player.Player.Description = new string('a',descriptionLength);

        var result = _unitOfWork.Commit(player.Player);

        Assert.AreEqual(CRUDResult.EntityValidationFailed, result);
    }

    [TestMethod]
    [DataRow(255)]
    [DataRow(100)]
    [DataRow(20)]
    public void AddPlayer_ShouldReturnSuccess_WhenDescriptionLengthIsValid(int descriptionLength)
    {
        var players = _unitOfWork!.GetAllPlayerModels();

        var player = players.First();

        player.Player.Description = new string('a', descriptionLength);

        var result = _unitOfWork.Commit(player.Player);

        Assert.AreEqual(CRUDResult.Success, result);
    }

    [TestMethod]
    public void AddPlayer_ShouldReturnEntityValidationFailed_WhenPositionIsInvalid()
    {
        var players = _unitOfWork!.GetAllPlayerModels();

        var player = players.First();

        player.Player.PositionId = Guid.Empty;

        var result = _unitOfWork.Commit(player.Player);

        Assert.AreEqual(CRUDResult.EntityValidationFailed, result);
    }

    [TestMethod]
    [DataRow]
    public void AddPlayer_ShouldReturnSuccess_WhenPositionIdIsValid()
    {
        var players = _unitOfWork!.GetAllPlayerModels();
        var positionId = _unitOfWork.GetAllPlayerPositions().First().Id;

        var player = players.First();

        player.Player.PositionId = positionId;

        var result = _unitOfWork.Commit(player.Player);

        Assert.AreEqual(CRUDResult.Success, result);
    }

    [TestMethod]
    [DataRow(100, 100)]
    [DataRow(100, 500)]
    [DataRow(300, 100)]
    public void AddPlayer_ShouldReturnEntityValidationFailed_WhenPhotoSizeIsInvalid(int width, int height)
    {
        var players = _unitOfWork!.GetAllPlayerModels();
        var photoSize = _photoGenerator!.Generate(width, height);

        var player = players.First();
        player.Player.Photo = photoSize;

        var result = _unitOfWork.Commit(player.Player);

        Assert.AreEqual(CRUDResult.EntityValidationFailed, result);
    }

    [TestMethod]
    public void AddPlayer_ShouldReturnSuccess_WhenPhotoSizeIsValid()
    {
        var players = _unitOfWork!.GetAllPlayerModels();
        var photoSize = _photoGenerator!.Generate(300, 500);

        var player = players.First();
        player.Player.Photo = photoSize;

        var result = _unitOfWork.Commit(player.Player);

        Assert.AreEqual(CRUDResult.Success, result);
    }
}