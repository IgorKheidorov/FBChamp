using FBChamp.Core.Entities.Soccer;
using FBChamp.Core.UnitOfWork;
using FBChamp.DataGenerators;
using FBChamp.Infrastructure;
using IntegrationTests.Helpers;

namespace IntegrationTests.CoachTests;

[TestClass]
public class CoachRepositoryTests
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
    public void AddCoach()
    {
        var initialCount = _unitOfWork!.GetAllCoachModels().Count;
        _dataGenerator!.GenerateCoach(new Dictionary<string, string> { { "Count", "1" } });

        var realCount = _unitOfWork.GetAllCoachModels().Count;
        Assert.AreEqual(initialCount + 1, realCount);
    }


    [TestMethod]
    public void GetCoachById()
    {
        var coachModel = _unitOfWork!.GetAllCoachModels().First();
        var coachId = coachModel.Coach.Id;
        var retrievedCoach = _unitOfWork.GetCoachModel(coachId);

        Assert.IsNotNull(retrievedCoach);
        Assert.AreEqual(coachId, retrievedCoach.Coach.Id);
        Assert.AreEqual(coachModel.Coach, retrievedCoach.Coach);
    }

    [TestMethod]
    public void UpdateCoach()
    {
        var retrievedCoach = _unitOfWork!.GetAllCoachModels().First();
        var coachId = retrievedCoach.Coach.Id;

        var updateBirthDate = new DateTime(1995, 5, 5);
        retrievedCoach.Coach.FullName = "NameAfterUpdate";
        retrievedCoach.Coach.BirthDate = updateBirthDate;

        _unitOfWork.Commit(retrievedCoach.Coach);
        var updatedCoach = _unitOfWork.GetCoachModel(coachId);

        Assert.IsNotNull(updatedCoach);
        Assert.AreEqual("NameAfterUpdate", updatedCoach.FullName);
        Assert.AreEqual(updateBirthDate, updatedCoach.Coach.BirthDate);
    }

    [TestMethod]
    public void RemoveCoach()
    {
        var initialCount = _unitOfWork!.GetAllCoachModels().Count;
        var coachId = _unitOfWork.GetAllCoachModels().First().Coach.Id;

        _unitOfWork.Remove(coachId, typeof(Coach));
        var resultCount = _unitOfWork.GetAllCoachModels().Count;

        Assert.AreEqual(initialCount - 1, resultCount);
    }
}