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
    [DataRow]
    public void PlayerInvalidNameLengthTest()
    {
        var unitOfWork = new UnitOfWork();
        var photoGenerator = new PhotoGenerator();

        var positionId = unitOfWork.GetAllPlayerPositions().First().Id;
        var photo = photoGenerator.Generate(300, 500);

        var player = new Player(Guid.NewGuid(),
            new string('a', 257),
            new DateTime(2000, 2, 1, new GregorianCalendar()),
            180,
            positionId,
            photo);

        var result = unitOfWork.Commit(player);

        Assert.AreEqual(CRUDResult.EntityValidationFailed, result);
    }

    [TestMethod]
    [DataRow]
    public void PlayerValidNameLengthTest()
    {
        var unitOfWork = new UnitOfWork();
        var photoGenerator = new PhotoGenerator();

        var positionId = unitOfWork.GetAllPlayerPositions().First().Id;
        var photo = photoGenerator.Generate(300, 500);

        var player = new Player(Guid.NewGuid(),
            new string('a', 255),
            new DateTime(2000, 2, 1, new GregorianCalendar()),
            180,
            positionId,
            photo);

        var result = unitOfWork.Commit(player);

        Assert.AreEqual(CRUDResult.Success, result);
    }

    [TestMethod]
    [DataRow]
    public void PlayerInvalidHigherAgeTest()
    {
        var unitOfWork = new UnitOfWork();
        var photoGenerator = new PhotoGenerator();

        var positionId = unitOfWork.GetAllPlayerPositions().First().Id;
        var photo = photoGenerator.Generate(300, 500);

        var player = new Player(Guid.NewGuid(),
            "Name",
            new DateTime(1950, 2, 1, new GregorianCalendar()),
            180,
            positionId,
            photo);

        var result = unitOfWork.Commit(player);

        Assert.AreEqual(CRUDResult.EntityValidationFailed, result);
    }

    [TestMethod]
    [DataRow]
    public void PlayerInvalidLowerAgeTest()
    {
        var unitOfWork = new UnitOfWork();
        var photoGenerator = new PhotoGenerator();

        var positionId = unitOfWork.GetAllPlayerPositions().First().Id;
        var photo = photoGenerator.Generate(300, 500);

        var player = new Player(Guid.NewGuid(),
            "Name",
            new DateTime(2015, 2, 1, new GregorianCalendar()),
            180,
            positionId,
            photo);

        var result = unitOfWork.Commit(player);

        Assert.AreEqual(CRUDResult.EntityValidationFailed, result);
    }

    [TestMethod]
    [DataRow]
    public void PlayerValidAgeTest()
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

    [TestMethod]
    [DataRow]
    public void PlayerInvalidHeightTest()
    {
        var unitOfWork = new UnitOfWork();
        var photoGenerator = new PhotoGenerator();

        var positionId = unitOfWork.GetAllPlayerPositions().First().Id;
        var photo = photoGenerator.Generate(300, 500);

        var player = new Player(Guid.NewGuid(),
            "Name",
            new DateTime(2000, 2, 1, new GregorianCalendar()),
            251,
            positionId,
            photo);

        var result = unitOfWork.Commit(player);

        Assert.AreEqual(CRUDResult.EntityValidationFailed, result);
    }

    [TestMethod]
    [DataRow]
    public void PlayerValidHeightTest()
    {
        var unitOfWork = new UnitOfWork();
        var photoGenerator = new PhotoGenerator();

        var positionId = unitOfWork.GetAllPlayerPositions().First().Id;
        var photo = photoGenerator.Generate(300, 500);

        var player = new Player(Guid.NewGuid(),
            "Name",
            new DateTime(2000, 2, 1, new GregorianCalendar()),
            209,
            positionId,
            photo);

        var result = unitOfWork.Commit(player);

        Assert.AreEqual(CRUDResult.Success, result);
    }

    [TestMethod]
    [DataRow]
    public void PlayerInvalidDescriptionLengthTest()
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
            photo,
            new string('a', 257));

        var result = unitOfWork.Commit(player);

        Assert.AreEqual(CRUDResult.EntityValidationFailed, result);
    }

    [TestMethod]
    [DataRow]
    public void PlayerValidDescriptionLengthTest()
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
            photo,
            new string('a', 255));

        var result = unitOfWork.Commit(player);

        Assert.AreEqual(CRUDResult.Success, result);
    }

    [TestMethod]
    [DataRow]
    public void PlayerInvalidPositionTest()
    {
        var unitOfWork = new UnitOfWork();
        var photoGenerator = new PhotoGenerator();

        var positionId = Guid.Empty;
        var photo = photoGenerator.Generate(300, 500);

        var player = new Player(Guid.NewGuid(),
            "Name",
            new DateTime(2000, 2, 1, new GregorianCalendar()),
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
            new DateTime(2000, 2, 1, new GregorianCalendar()),
            180,
            positionId,
            photo);

        var result = unitOfWork.Commit(player);

        Assert.AreEqual(CRUDResult.Success, result);
    }

    [TestMethod]
    [DataRow]
    public void PlayerInvalidPhotoSizeTest()
    {
        var unitOfWork = new UnitOfWork();
        var photoGenerator = new PhotoGenerator();

        var positionId = unitOfWork.GetAllPlayerPositions().First().Id;
        var photo = photoGenerator.Generate(100, 100);

        var player = new Player(Guid.NewGuid(),
            "Name",
            new DateTime(2000, 2, 1, new GregorianCalendar()),
            180,
            positionId,
            photo);

        var result = unitOfWork.Commit(player);

        Assert.AreEqual(CRUDResult.EntityValidationFailed, result);
    }

    [TestMethod]
    [DataRow]
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