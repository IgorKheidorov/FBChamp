using FBChamp.Core.Entities.Soccer;
using FBChamp.Infrastructure;

namespace IntegrationTests.StadiumTests;

[TestClass]
public class StadiumRepositoryTests
{
    [TestMethod]
    public void AddStadiums()
    {
        UnitOfWork unitOfWork = new UnitOfWork();
        var initialCount = unitOfWork.GetAllStadiumModels().Count;

        Guid stadiumId = Guid.NewGuid();
        Guid locationId = Guid.NewGuid();

        List<Task> tasks = Enumerable.Range(0, 100)
            .Select(x => new Stadium(stadiumId, "Name" + x, locationId))
            .Select(x => new Task(() =>
            {
                unitOfWork.Commit(x);
            })).ToList();

        tasks.ForEach(x => x.Start());
        Task.WaitAll(tasks.ToArray());

        var realCount = unitOfWork.GetAllStadiumModels().Count;

        Assert.AreEqual(initialCount + 1, realCount);
    }

    [TestMethod]
    public void GetStadiumById()
    {
        UnitOfWork unitOfWork = new UnitOfWork();
        var stadiumId = Guid.NewGuid();
        var locationId = Guid.NewGuid();

        var stadium = new Stadium(stadiumId, "Name", locationId);
        unitOfWork.Commit(stadium);
        var retrievedStadium = unitOfWork.GetStadiumModel(stadiumId);

        Assert.IsNotNull(retrievedStadium);
        Assert.AreEqual(stadiumId, retrievedStadium.Stadium.Id);
        Assert.AreEqual("Name", retrievedStadium.Stadium.Name);
        Assert.AreEqual(locationId, retrievedStadium.Stadium.LocationId);
    }

    [TestMethod]
    public void UpdateStadium()
    {
        UnitOfWork unitOfWork = new UnitOfWork();
        var stadiumId = Guid.NewGuid();
        var locationId = Guid.NewGuid();

        var stadium = new Stadium(stadiumId, "Name", locationId);
        unitOfWork.Commit(stadium);
        var retrievedStadium = unitOfWork.GetStadiumModel(stadiumId);

        Assert.AreEqual("Name", retrievedStadium.Stadium.Name);

        retrievedStadium.Stadium.Name = "NewName";

        unitOfWork.Commit(retrievedStadium.Stadium);
        var updatedStadium = unitOfWork.GetStadiumModel(stadiumId);

        Assert.IsNotNull(updatedStadium);
        Assert.AreEqual("NewName", updatedStadium.Stadium.Name);
    }

    [TestMethod]
    public void DeleteStadium()
    {
        UnitOfWork unitOfWork = new UnitOfWork();
        var stadiumId = Guid.NewGuid();
        var locationId = Guid.NewGuid();

        var stadium = new Stadium(stadiumId, "Name", locationId);
        unitOfWork.Commit(stadium);
        var initialCount = unitOfWork.GetAllStadiumModels().Count;

        unitOfWork.Remove(stadiumId, typeof(Stadium));
        var realCount = unitOfWork.GetAllStadiumModels().Count;

        Assert.AreEqual(initialCount - 1, realCount);
    }
}