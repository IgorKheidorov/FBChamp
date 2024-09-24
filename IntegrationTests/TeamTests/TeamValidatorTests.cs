using FBChamp.Core.Entities.Soccer;
using FBChamp.Core.UnitOfWork;
using FBChamp.DataGenerators;
using FBChamp.Infrastructure;
using IntegrationTests.Helpers;
using System.Globalization;

namespace IntegrationTests.TeamTests;

[TestClass]
public class TeamValidatorTests
{
    private IUnitOfWork? _unitOfWork;
    private DataGenerator? _dataGenerator;

    [TestInitialize]
    public void Initialize()
    {
        Infrastructure.CleanUp();
        _unitOfWork = new UnitOfWork();
        _dataGenerator = new DataGenerator(_unitOfWork);

        _dataGenerator.GenerateTeam(new Dictionary<string, string>
        {
            { "Count", "1" }
        });
    }

    [TestMethod]
    [DataRow(257)]
    [DataRow(300)]
    public void TeamInvalidNameLengthTest(int nameLength)
    {
        var team = _unitOfWork!.GetAllTeamModels().First().Team;
        team.Name = new string('n', nameLength);

        var actualResult = _unitOfWork.Commit(team);
        Assert.AreEqual(CRUDResult.EntityValidationFailed, actualResult);
    }

    [TestMethod]
    [DataRow(1)]
    [DataRow(100)]
    [DataRow(255)]
    public void TeamValidNameLengthTest(int nameLength)
    {
        var team = _unitOfWork!.GetAllTeamModels().First().Team;
        team.Name = new string('n', nameLength);

        var actualResult = _unitOfWork.Commit(team);
        Assert.AreEqual(CRUDResult.Success, actualResult);
    }

    [TestMethod]
    [DataRow(257)]
    [DataRow(300)]
    public void TeamInvalidDescriptionLengthTest(int descriptionLength)
    {
        var team = _unitOfWork!.GetAllTeamModels().First().Team;
        team.Description = new string('d', descriptionLength);

        var actualResult = _unitOfWork.Commit(team);
        Assert.AreEqual(CRUDResult.EntityValidationFailed, actualResult);
    }

    [TestMethod]
    [DataRow(1)]
    [DataRow(100)]
    [DataRow(255)]
    public void TeamValidDescriptionLengthTest(int descriptionLength)
    {
        var team = _unitOfWork!.GetAllTeamModels().First().Team;
        team.Description = new string('d', descriptionLength);

        var actualResult = _unitOfWork.Commit(team);
        Assert.AreEqual(CRUDResult.Success, actualResult);
    }

    [TestMethod]
    [DataRow(100, 100)]
    [DataRow(100, 500)]
    [DataRow(300, 100)]
    public void TeamInvalidPhotoSizeTest(int width, int height)
    {
        var team = _unitOfWork!.GetAllTeamModels().First().Team;
        var photo = new PhotoGenerator().Generate(width, height);
        team.Photo = photo;

        var actualResult = _unitOfWork.Commit(team);
        Assert.AreEqual(CRUDResult.EntityValidationFailed, actualResult);
    }

    [TestMethod]
    public void TeamValidPhotoSizeTest()
    {
        var team = _unitOfWork!.GetAllTeamModels().First().Team;
        var photo = new PhotoGenerator().Generate(300, 500);
        team.Photo = photo;

        var actualResult = _unitOfWork.Commit(team);
        Assert.AreEqual(CRUDResult.Success, actualResult);
    }
}