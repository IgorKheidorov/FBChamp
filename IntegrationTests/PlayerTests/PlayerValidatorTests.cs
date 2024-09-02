using System.Globalization;
using FBChamp.Core.Entities.Soccer;
using FBChamp.Core.UnitOfWork;
using FBChamp.Infrastructure;
using IntegrationTests.Helpers;

namespace IntegrationTests.PlayerTests;

[TestClass]
public class PlayerValidatorTests
{
    [TestInitialize]
    public void Initialize()
    {
        Infrastructure.CleanUp();
    }

    [TestMethod]
    [DataRow(257)]
    [DataRow(300)]
    public void PlayerInvalidNameLengthTest(int nameLength)
    {
        var unitOfWork = new UnitOfWork();
        var photoGenerator = new PhotoGenerator();

        var positionId = unitOfWork.GetAllPlayerPositions().First().Id;
        var photo = photoGenerator.Generate(300, 500);

        var player = new Player(Guid.NewGuid(),
            new string('a', nameLength),
            new DateTime(2000, 2, 1),
            180,
            positionId,
            photo);

        var result = unitOfWork.Commit(player);

        Assert.AreEqual(CRUDResult.EntityValidationFailed, result);
    }

    [TestMethod]
    [DataRow(255)]
    [DataRow(100)]
    [DataRow(20)]
    public void PlayerValidNameLengthTest(int nameLength)
    {
        var unitOfWork = new UnitOfWork();
        var photoGenerator = new PhotoGenerator();

        var positionId = unitOfWork.GetAllPlayerPositions().First().Id;
        var photo = photoGenerator.Generate(300, 500);

        var player = new Player(Guid.NewGuid(),
            new string('a', nameLength),
            new DateTime(2000, 2, 1),
            180,
            positionId,
            photo);

        var result = unitOfWork.Commit(player);

        Assert.AreEqual(CRUDResult.Success, result);
    }

    [TestMethod]
    [DataRow(1950, 2, 1)]
    [DataRow(2015, 2, 1)]
    public void PlayerInvalidAgeTest(int year, int month, int day)
    {
        var unitOfWork = new UnitOfWork();
        var photoGenerator = new PhotoGenerator();

        var positionId = unitOfWork.GetAllPlayerPositions().First().Id;
        var photo = photoGenerator.Generate(300, 500);
        var birthDate = new DateTime(year, month, day);

        var player = new Player(Guid.NewGuid(),
            "Name",
            birthDate,
            180,
            positionId,
            photo);

        var result = unitOfWork.Commit(player);

        Assert.AreEqual(CRUDResult.EntityValidationFailed, result);
    }

    [TestMethod]
    [DataRow(2000, 2, 1)]
    [DataRow(1985, 2, 1)]
    [DataRow(2008, 2, 1)]
    public void PlayerValidAgeTest(int year, int month, int day)
    {
        var unitOfWork = new UnitOfWork();
        var photoGenerator = new PhotoGenerator();

        var positionId = unitOfWork.GetAllPlayerPositions().First().Id;
        var photo = photoGenerator.Generate(300, 500);
        var birthDay = new DateTime(year, month, day);

        var player = new Player(Guid.NewGuid(),
            "Name",
            birthDay,
            180,
            positionId,
            photo);

        var result = unitOfWork.Commit(player);

        Assert.AreEqual(CRUDResult.Success, result);
    }

    [TestMethod]
    [DataRow(251)]
    [DataRow(350)]
    [DataRow(115)]
    [DataRow(149)]
    public void PlayerInvalidHeightTest(int height)
    {
        var unitOfWork = new UnitOfWork();
        var photoGenerator = new PhotoGenerator();

        var positionId = unitOfWork.GetAllPlayerPositions().First().Id;
        var photo = photoGenerator.Generate(300, 500);

        var player = new Player(Guid.NewGuid(),
            "Name",
            new DateTime(2000, 2, 1),
            height,
            positionId,
            photo);

        var result = unitOfWork.Commit(player);

        Assert.AreEqual(CRUDResult.EntityValidationFailed, result);
    }

    [TestMethod]
    [DataRow(170)]
    [DataRow(209)]
    public void PlayerValidHeightTest(int height)
    {
        var unitOfWork = new UnitOfWork();
        var photoGenerator = new PhotoGenerator();

        var positionId = unitOfWork.GetAllPlayerPositions().First().Id;
        var photo = photoGenerator.Generate(300, 500);

        var player = new Player(Guid.NewGuid(),
            "Name",
            new DateTime(2000, 2, 1),
            height,
            positionId,
            photo);

        var result = unitOfWork.Commit(player);

        Assert.AreEqual(CRUDResult.Success, result);
    }

    [TestMethod]
    [DataRow(257)]
    [DataRow(400)]
    public void PlayerInvalidDescriptionLengthTest(int descriptionLength)
    {
        var unitOfWork = new UnitOfWork();
        var photoGenerator = new PhotoGenerator();

        var positionId = unitOfWork.GetAllPlayerPositions().First().Id;
        var photo = photoGenerator.Generate(300, 500);

        var player = new Player(Guid.NewGuid(),
            "Name",
            new DateTime(2000, 2, 1),
            180,
            positionId,
            photo,
            new string('a', descriptionLength));

        var result = unitOfWork.Commit(player);

        Assert.AreEqual(CRUDResult.EntityValidationFailed, result);
    }

    [TestMethod]
    [DataRow(255)]
    [DataRow(100)]
    [DataRow(20)]
    public void PlayerValidDescriptionLengthTest(int descriptionLength)
    {
        var unitOfWork = new UnitOfWork();
        var photoGenerator = new PhotoGenerator();

        var positionId = unitOfWork.GetAllPlayerPositions().First().Id;
        var photo = photoGenerator.Generate(300, 500);

        var player = new Player(Guid.NewGuid(),
            "Name",
            new DateTime(2000, 2, 1),
            180,
            positionId,
            photo,
            new string('a', descriptionLength));

        var result = unitOfWork.Commit(player);

        Assert.AreEqual(CRUDResult.Success, result);
    }

    [TestMethod]
    public void PlayerInvalidPositionTest()
    {
        var unitOfWork = new UnitOfWork();
        var photoGenerator = new PhotoGenerator();

        var positionId = Guid.Empty;
        var photo = photoGenerator.Generate(300, 500);

        var player = new Player(Guid.NewGuid(),
            "Name",
            new DateTime(2000, 2, 1),
            180,
            positionId,
            photo);

        var result = unitOfWork.Commit(player);

        Assert.AreEqual(CRUDResult.EntityValidationFailed, result);
    }

    [TestMethod]
    [DataRow]
    public void PlayerValidPositionTest()
    {
        var unitOfWork = new UnitOfWork();
        var photoGenerator = new PhotoGenerator();

        var positionId = unitOfWork.GetAllPlayerPositions().First().Id;
        var photo = photoGenerator.Generate(300, 500);

        var player = new Player(Guid.NewGuid(),
            "Name",
            new DateTime(2000, 2, 1),
            180,
            positionId,
            photo);

        var result = unitOfWork.Commit(player);

        Assert.AreEqual(CRUDResult.Success, result);
    }

    [TestMethod]
    [DataRow(100, 100)]
    [DataRow(100, 500)]
    [DataRow(300, 100)]
    public void PlayerInvalidPhotoSizeTest(int width, int height)
    {
        var unitOfWork = new UnitOfWork();
        var photoGenerator = new PhotoGenerator();

        var positionId = unitOfWork.GetAllPlayerPositions().First().Id;
        var photo = photoGenerator.Generate(width, height);

        var player = new Player(Guid.NewGuid(),
            "Name",
            new DateTime(2000, 2, 1),
            180,
            positionId,
            photo);

        var result = unitOfWork.Commit(player);

        Assert.AreEqual(CRUDResult.EntityValidationFailed, result);
    }

    [TestMethod]
    public void PlayerValidPhotoSizeTest()
    {
        var unitOfWork = new UnitOfWork();
        var photoGenerator = new PhotoGenerator();

        var positionId = unitOfWork.GetAllPlayerPositions().First().Id;
        var photo = photoGenerator.Generate(300, 500);

        var player = new Player(Guid.NewGuid(),
            "Name",
            new DateTime(2000, 2, 1, new GregorianCalendar()),
            180,
            positionId,
            photo);

        var result = unitOfWork.Commit(player);

        Assert.AreEqual(CRUDResult.Success, result);
    }
}