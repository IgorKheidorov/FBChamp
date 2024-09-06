using FBChamp.Core.Entities.Soccer;
using FBChamp.Core.UnitOfWork;
using FBChamp.Infrastructure;
using IntegrationTests.Helpers;

namespace IntegrationTests.CoachTests;

[TestClass]
public class CoachValidatorTests
{
    [TestInitialize]
    public void Initialize()
    {
        Infrastructure.CleanUp();
    }

    [TestMethod]
    [DataRow(257)]
    [DataRow(300)]
    public void CoachInvalidNameLengthTest(int nameLength)
    {
        var unitOfWork = new UnitOfWork();
        var photoGenerator = new PhotoGenerator();

        var photo = photoGenerator.Generate(300, 500);

        var coach = new Coach(Guid.NewGuid(), new string('a', nameLength), new DateTime(2000, 1, 1), photo);

        var result = unitOfWork.Commit(coach);

        Assert.AreEqual(CRUDResult.EntityValidationFailed, result);
    }

    [TestMethod]
    [DataRow(255)]
    [DataRow(100)]
    [DataRow(20)]
    public void CoachValidNameLengthTest(int nameLength)
    {
        var unitOfWork = new UnitOfWork();
        var photoGenerator = new PhotoGenerator();

        var photo = photoGenerator.Generate(300, 500);

        var coach = new Coach(Guid.NewGuid(), new string('a', nameLength), new DateTime(2000, 1, 1), photo);

        var result = unitOfWork.Commit(coach);

        Assert.AreEqual(CRUDResult.Success, result);
    }

    [TestMethod]
    [DataRow(1923, 1, 1)]
    [DataRow(1888, 1, 1)]
    public void CoachInvalidAgeTest(int year, int month, int day)
    {
        var unitOfWork = new UnitOfWork();
        var photoGenerator = new PhotoGenerator();

        var photo = photoGenerator.Generate(300, 500);

        var coach = new Coach(Guid.NewGuid(), "Name", new DateTime(year, month, day), photo);

        var result = unitOfWork.Commit(coach);

        Assert.AreEqual(CRUDResult.EntityValidationFailed, result);
    }

    [TestMethod]
    [DataRow(2006, 1, 1)]
    [DataRow(1995, 1, 1)]
    public void CoachValidAgeTest(int year, int month, int day)
    {
        var unitOfWork = new UnitOfWork();
        var photoGenerator = new PhotoGenerator();

        var photo = photoGenerator.Generate(300, 500);

        var coach = new Coach(Guid.NewGuid(), "Name", new DateTime(year, month, day), photo);

        var result = unitOfWork.Commit(coach);

        Assert.AreEqual(CRUDResult.Success, result);
    }

    [TestMethod]
    [DataRow(100, 100)]
    [DataRow(100, 500)]
    [DataRow(300, 100)]
    public void CoachInvalidPhotoTest(int width, int height)
    {
        var unitOfWork = new UnitOfWork();
        var photoGenerator = new PhotoGenerator();

        var photo = photoGenerator.Generate(width, height);

        var coach = new Coach(Guid.NewGuid(), "Name", new DateTime(2000, 1, 1), photo);

        var result = unitOfWork.Commit(coach);

        Assert.AreEqual(CRUDResult.EntityValidationFailed, result);
    }

    [TestMethod]
    public void CoachValidPhotoTest()
    {
        var unitOfWork = new UnitOfWork();
        var photoGenerator = new PhotoGenerator();

        var photo = photoGenerator.Generate(300, 500);

        var coach = new Coach(Guid.NewGuid(), "Name", new DateTime(2000, 1, 1), photo);

        var result = unitOfWork.Commit(coach);

        Assert.AreEqual(CRUDResult.Success, result);
    }

    [TestMethod]
    [DataRow(257)]
    [DataRow(400)]
    public void CoachInvalidDescriptionLength(int descriptionLength)
    {
        var unitOfWork = new UnitOfWork();
        var photoGenerator = new PhotoGenerator();

        var photo = photoGenerator.Generate(300, 500);

        var coach = new Coach(Guid.NewGuid(), "Name", new DateTime(2000, 1, 1), photo,
            new string('a', descriptionLength));

        var result = unitOfWork.Commit(coach);

        Assert.AreEqual(CRUDResult.EntityValidationFailed, result);
    }

    [TestMethod]
    [DataRow(255)]
    [DataRow(100)]
    [DataRow(20)]
    public void CoachValidDescriptionLength(int descriptionLength)
    {
        var unitOfWork = new UnitOfWork();
        var photoGenerator = new PhotoGenerator();

        var photo = photoGenerator.Generate(300, 500);

        var coach = new Coach(Guid.NewGuid(), "Name", new DateTime(2000, 1, 1), photo,
            new string('a', descriptionLength));

        var result = unitOfWork.Commit(coach);

        Assert.AreEqual(CRUDResult.Success, result);
    }
}