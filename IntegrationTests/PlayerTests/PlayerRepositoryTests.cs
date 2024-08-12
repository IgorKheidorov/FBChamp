using FBChamp.Core.Entities.Socker;
using FBChamp.Infrastructure;

namespace IntegrationTests.PlayerTests;

[TestClass]
public class PlayerRepositoryTests

{
   //[TestMethod]
    public void AddPlayers()
    {
        UnitOfWork unitOfWork = new UnitOfWork();
        var initialCount = unitOfWork.GetAllPlayerModels().Count;

        var models = unitOfWork.GetAllPlayerModels();
        Guid positionId = Guid.NewGuid();
        Guid playerID = Guid.NewGuid();

        List<Task> tasks = Enumerable.Range(0, 100)
            .Select(x => new Player(playerID, "FulllName" + x, DateTime.Now, 190, positionId, null))
            .Select(x => new Task(() =>
            {
                unitOfWork.Commit(x);
            })).ToList();

        tasks.ForEach(x => x.Start());
        Task.WaitAll(tasks.ToArray());
        var realCount = unitOfWork.GetAllPlayerModels().Count;
        Assert.AreEqual(initialCount + 1, realCount);
    }
}