using FBChamp.Core.Entities.Soccer;
using FBChamp.Infrastructure;
using IntegrationTests.Helpers;

namespace IntegrationTests.CoachTests;

[TestClass]
public class CoachRepositoryTests
{
    [TestMethod]
    public void AddCoach()
    {
        var unitOfWork = new UnitOfWork();
        var photoGenerator = new PhotoGenerator();
        var initialCount = unitOfWork.GetAllCoachModels().Count;

        var coachId = Guid.NewGuid();
        var photo = photoGenerator.Generate(300, 500);
        var birthDate = new DateTime(2000, 1, 1);

        var coach = new Coach(coachId, "Name", birthDate, photo);

        unitOfWork.Commit(coach);

        var realCount = unitOfWork.GetAllCoachModels().Count;

        Assert.AreEqual(initialCount + 1, realCount);
    }


    [TestMethod]
    public void GetCoachById()
    {
        var unitOfWork = new UnitOfWork();
        var photoGenerator = new PhotoGenerator();

        var coachId = Guid.NewGuid();
        var photo = photoGenerator.Generate(300, 500);
        var birthDate = new DateTime(2000, 1, 1);

        var coach = new Coach(coachId, "Name", birthDate, photo);

        unitOfWork.Commit(coach);

        var retrievedCoach = unitOfWork.GetCoachModel(coachId);

        Assert.IsNotNull(retrievedCoach);
        Assert.AreEqual(coachId, retrievedCoach.Coach.Id);
        Assert.AreEqual("Name", retrievedCoach.FullName);
        Assert.AreEqual(birthDate, retrievedCoach.Coach.BirthDate);
        Assert.AreEqual(photo, retrievedCoach.Coach.Photo);
    }

    [TestMethod]
    public void UpdateCoach()
    {
        var unitOfWork = new UnitOfWork();
        var photoGenerator = new PhotoGenerator();

        var coachId = Guid.NewGuid();
        var photo = photoGenerator.Generate(300, 500);
        var birthDate = new DateTime(2000, 1, 1);

        var coach = new Coach(coachId, "NameBeforeUpdate", birthDate, photo);

        unitOfWork.Commit(coach);
        var retrievedCoach = unitOfWork.GetCoachModel(coachId);

        Assert.AreEqual("NameBeforeUpdate", retrievedCoach.FullName);
        Assert.AreEqual(birthDate, retrievedCoach.Coach.BirthDate);

        var updateBirthDate = new DateTime(1995, 5, 5);
        retrievedCoach.Coach.FullName = "NameAfterUpdate";
        retrievedCoach.Coach.BirthDate = updateBirthDate;

        unitOfWork.Commit(retrievedCoach.Coach);
        var updatedCoach = unitOfWork.GetCoachModel(coachId);

        Assert.IsNotNull(updatedCoach);
        Assert.AreEqual("NameAfterUpdate", updatedCoach.FullName);
        Assert.AreEqual(updateBirthDate, updatedCoach.Coach.BirthDate);
    }

    [TestMethod]
    public void RemoveCoach()
    {
        var unitOfWork = new UnitOfWork();
        var photoGenerator = new PhotoGenerator();

        var coachId = Guid.NewGuid();
        var photo = photoGenerator.Generate(300, 500);
        var birthDate = new DateTime(2000, 1, 1);

        var coach = new Coach(coachId, "Name", birthDate, photo);

        unitOfWork.Commit(coach);
        var initialCount = unitOfWork.GetAllCoachModels().Count;

        unitOfWork.Remove(coachId, typeof(Coach));
        var resultCount = unitOfWork.GetAllCoachModels().Count;

        Assert.AreEqual(initialCount - 1, resultCount);
    }
}