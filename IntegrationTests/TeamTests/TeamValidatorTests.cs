using FBChamp.Core.Entities.Soccer;
using FBChamp.Core.UnitOfWork;
using FBChamp.Infrastructure;
using IntegrationTests.Helpers;
using System.Globalization;

namespace IntegrationTests.TeamTests;

[TestClass]
public class TeamValidatorTests
{
    [TestInitialize]
    public void Initialize()
    {
        Infrastructure.CleanUp();
    }

    [TestMethod]
    [DataRow(257)]
    [DataRow(300)]
    public void TeamInvalidNameLengthTest(int nameLength)
    {
        UnitOfWork unitOfWork = new UnitOfWork();
        var photoGenerator = new PhotoGenerator();
        var photo = photoGenerator.Generate(300, 500);
        Team team = new Team(Guid.NewGuid(), new string('n', nameLength), Guid.NewGuid(), photo, Guid.NewGuid());

        var actualResult = unitOfWork.Commit(team);
        Assert.AreEqual(CRUDResult.EntityValidationFailed, actualResult);
    }

    [TestMethod]
    [DataRow(1)]
    [DataRow(100)]
    [DataRow(255)]
    public void TeamValidNameLengthTest(int nameLength)
    {
        UnitOfWork unitOfWork = new UnitOfWork();
        var photoGenerator = new PhotoGenerator();
        var photo = photoGenerator.Generate(300, 500);
        Team team = new Team(Guid.NewGuid(), new string('n', nameLength), Guid.NewGuid(), photo, Guid.NewGuid());

        var actualResult = unitOfWork.Commit(team);
        Assert.AreEqual(CRUDResult.Success, actualResult);
    }

    [TestMethod]
    [DataRow(257)]
    [DataRow(300)]
    public void TeamInvalidDescriptionLengthTest(int descriptionLength)
    {
        UnitOfWork unitOfWork = new UnitOfWork();
        var photoGenerator = new PhotoGenerator();
        var photo = photoGenerator.Generate(300, 500);
        Team team = new Team(Guid.NewGuid(), new string('n', 100), Guid.NewGuid(), photo, Guid.NewGuid(), new string('d', descriptionLength));

        var actualResult = unitOfWork.Commit(team);
        Assert.AreEqual(CRUDResult.EntityValidationFailed, actualResult);
    }

    [TestMethod]
    [DataRow(1)]
    [DataRow(100)]
    [DataRow(255)]
    public void TeamValidDescriptionLengthTest(int descriptionLength)
    {
        UnitOfWork unitOfWork = new UnitOfWork();
        var photoGenerator = new PhotoGenerator();
        var photo = photoGenerator.Generate(300, 500);
        Team team = new Team(Guid.NewGuid(), new string('n', 100), Guid.NewGuid(), photo, Guid.NewGuid(), new string('d', descriptionLength));

        var actualResult = unitOfWork.Commit(team);
        Assert.AreEqual(CRUDResult.Success, actualResult);
    }

    [TestMethod]
    [DataRow(100, 100)]
    [DataRow(100, 500)]
    [DataRow(300, 100)]
    public void TeamInvalidPhotoSizeTest(int width, int height)
    {
        var unitOfWork = new UnitOfWork();
        var photoGenerator = new PhotoGenerator();
        var photo = photoGenerator.Generate(width, height);

        var team = new Team(Guid.NewGuid(), new string('n', 100), Guid.NewGuid(), photo, Guid.NewGuid());

        var actualResult = unitOfWork.Commit(team);

        Assert.AreEqual(CRUDResult.EntityValidationFailed, actualResult);
    }

    [TestMethod]
    public void TeamValidPhotoSizeTest()
    {
        var unitOfWork = new UnitOfWork();
        var photoGenerator = new PhotoGenerator();
        var photo = photoGenerator.Generate(300, 500);

        var team = new Team(Guid.NewGuid(), new string('n', 100), Guid.NewGuid(), photo, Guid.NewGuid());

        var actualResult = unitOfWork.Commit(team);

        Assert.AreEqual(CRUDResult.Success, actualResult);
    }
}