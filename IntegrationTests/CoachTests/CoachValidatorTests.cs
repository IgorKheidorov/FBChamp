using FBChamp.Core.Entities.Soccer;
using FBChamp.Core.UnitOfWork;
using FBChamp.DataGenerators;
using FBChamp.Infrastructure;
using IntegrationTests.Helpers;

namespace IntegrationTests.CoachTests;

[TestClass]
public class CoachValidatorTests
{
    private IUnitOfWork? _unitOfWork;
    private DataGenerator? _dataGenerator;

    [TestInitialize]
    public void Initialize()
    {
        Infrastructure.CleanUp();
        _unitOfWork = new UnitOfWork();
        _dataGenerator = new DataGenerator(_unitOfWork);

        _dataGenerator.GenerateCoach(new Dictionary<string, string>
        {
            { "Count", "1" }
        });
    }

    [TestMethod]
    [DataRow(257)]
    [DataRow(300)]
    public void CoachInvalidNameLengthTest(int nameLength)
    {
        var coach = _unitOfWork!.GetAllCoachModels().First().Coach;
        coach.FullName = new string('a', nameLength);

        var result = _unitOfWork.Commit(coach);
        Assert.AreEqual(CRUDResult.EntityValidationFailed, result);
    }

    [TestMethod]
    [DataRow(255)]
    [DataRow(100)]
    [DataRow(20)]
    public void CoachValidNameLengthTest(int nameLength)
    {
        var coach = _unitOfWork!.GetAllCoachModels().First().Coach;
        coach.FullName = new string('a', nameLength);

        var result = _unitOfWork.Commit(coach);
        Assert.AreEqual(CRUDResult.Success, result);
    }

    [TestMethod]
    [DataRow(1923, 1, 1)]
    [DataRow(1888, 1, 1)]
    public void CoachInvalidAgeTest(int year, int month, int day)
    {
        var coach = _unitOfWork!.GetAllCoachModels().First().Coach;
        coach.BirthDate = new DateTime(year, month, day);

        var result = _unitOfWork.Commit(coach);
        Assert.AreEqual(CRUDResult.EntityValidationFailed, result);
    }

    [TestMethod]
    [DataRow(2006, 1, 1)]
    [DataRow(1995, 1, 1)]
    public void CoachValidAgeTest(int year, int month, int day)
    {
        var coach = _unitOfWork!.GetAllCoachModels().First().Coach;
        coach.BirthDate = new DateTime(year, month, day);

        var result = _unitOfWork.Commit(coach);
        Assert.AreEqual(CRUDResult.Success, result);
    }

    [TestMethod]
    [DataRow(100, 100)]
    [DataRow(100, 500)]
    [DataRow(300, 100)]
    public void CoachInvalidPhotoTest(int width, int height)
    {
        var coach = _unitOfWork!.GetAllCoachModels().First().Coach;
        var photo = new PhotoGenerator().Generate(width, height);
        coach.Photo = photo;

        var result = _unitOfWork.Commit(coach);
        Assert.AreEqual(CRUDResult.EntityValidationFailed, result);
    }

    [TestMethod]
    public void CoachValidPhotoTest()
    {
        var coach = _unitOfWork!.GetAllCoachModels().First().Coach;
        var photo = new PhotoGenerator().Generate(300, 500);
        coach.Photo = photo;

        var result = _unitOfWork.Commit(coach);
        Assert.AreEqual(CRUDResult.Success, result);
    }

    [TestMethod]
    [DataRow(257)]
    [DataRow(400)]
    public void CoachInvalidDescriptionLength(int descriptionLength)
    {
        var coach = _unitOfWork!.GetAllCoachModels().First().Coach;
        coach.Description = new string('a', descriptionLength);

        var result = _unitOfWork.Commit(coach);
        Assert.AreEqual(CRUDResult.EntityValidationFailed, result);
    }

    [TestMethod]
    [DataRow(255)]
    [DataRow(100)]
    [DataRow(20)]
    public void CoachValidDescriptionLength(int descriptionLength)
    {
        var coach = _unitOfWork!.GetAllCoachModels().First().Coach;
        coach.Description = new string('a', descriptionLength);

        var result = _unitOfWork.Commit(coach);
        Assert.AreEqual(CRUDResult.Success, result);
    }
}