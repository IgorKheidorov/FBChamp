using FBChamp.Core.Entities;
using FBChamp.Core.Entities.Socker;
using FBChamp.Infrastructure;

namespace IntegrationTests;

[TestClass]
public class PlayerRepositoryTests

{
    [TestMethod]
    public void AddPlayers()
    {
      /*  UnitOfWork unitOfWork = new UnitOfWork();
        var initialCount = unitOfWork.PlayerRepository.All().Count();
        Guid positionId = Guid.NewGuid();
        Guid playerID = Guid.NewGuid();

        List<Task> tasks = new();
        for (int i = 0; i < 10; i++) 
        {
            tasks.Add(Task.Run(() =>
            {
                Player player = new Player(playerID, "FulllName" + i++, DateTime.Now, 190, positionId, null);
                unitOfWork.Commit(new List<Entity>(){ player });
            }));
        }
        Task.WaitAll(tasks.ToArray());
        var realCount = unitOfWork.PlayerRepository.All().Count();
        Assert.AreEqual(initialCount + 1, realCount);*/
    }
}