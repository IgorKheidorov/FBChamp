using FBChamp.Core.Entities.Soccer;
using FBChamp.Infrastructure;
using IntegrationTests.Helpers;

namespace IntegrationTests.GoalTests;

[TestClass]
public class GoalRepositoryTests
{
    // TODO: Fix after Match CRUD Operations are merged

    [TestMethod]
    public void AddGoalTest()
    {
        var unitOfWork = new UnitOfWork();
        var photoGenerator = new PhotoGenerator();
        var initialCount = unitOfWork.GetAllGoalModels().Count;

        var photo = photoGenerator.Generate(300, 500);
        var positionId = unitOfWork.GetAllPlayerPositions().First().Id;
        var goalAuthor = new Player(Guid.NewGuid(), "Name", new DateTime(2000, 1, 1), 180f, positionId, photo);
        var assistant = new Player(Guid.NewGuid(), "Assistant", new DateTime(2000, 1, 1), 180f, positionId, photo);
        var goal = new Goal(Guid.NewGuid(), Guid.NewGuid(), goalAuthor.Id, [assistant.Id], DateTime.Now);

        unitOfWork.Commit(goalAuthor);
        unitOfWork.Commit(assistant);
        unitOfWork.Commit(goal);

        var result = unitOfWork.GetAllGoalModels().Count;

        Assert.AreEqual(initialCount + 1, result);
    }

    [TestMethod]
    public void GetGoalByIdTest()
    {
        var unitOfWork = new UnitOfWork();
        var photoGenerator = new PhotoGenerator();

        var photo = photoGenerator.Generate(300, 500);
        var positionId = unitOfWork.GetAllPlayerPositions().First().Id;
        var goalAuthor = new Player(Guid.NewGuid(), "Name", new DateTime(2000, 1, 1), 180f, positionId, photo);
        var assistant = new Player(Guid.NewGuid(), "Assistant", new DateTime(2000, 1, 1), 180f, positionId, photo);
        var goal = new Goal(Guid.NewGuid(), Guid.NewGuid(), goalAuthor.Id, [assistant.Id], DateTime.Now);

        unitOfWork.Commit(goalAuthor);
        unitOfWork.Commit(assistant);
        unitOfWork.Commit(goal);

        var result = unitOfWork.GetGoalModel(goal.Id);

        Assert.IsNotNull(result);
        Assert.AreEqual(goal.GoalAuthorId, result.GoalAuthor.Id);
    }

    [TestMethod]
    public void UpdateGoalTest()
    {
        var unitOfWork = new UnitOfWork();
        var photoGenerator = new PhotoGenerator();

        var photo = photoGenerator.Generate(300, 500);
        var positionId = unitOfWork.GetAllPlayerPositions().First().Id;
        var goalAuthor = new Player(Guid.NewGuid(), "Name", new DateTime(2000, 1, 1), 180f, positionId, photo);
        var updateGoalAuthor = new Player(Guid.NewGuid(), "Name", new DateTime(2000, 1, 1), 180f, positionId, photo);

        var assistant = new Player(Guid.NewGuid(), "Assistant", new DateTime(2000, 1, 1), 180f, positionId, photo);
        var goal = new Goal(Guid.NewGuid(), Guid.NewGuid(), goalAuthor.Id, [assistant.Id], DateTime.Now);

        unitOfWork.Commit(goalAuthor);
        unitOfWork.Commit(assistant);
        unitOfWork.Commit(updateGoalAuthor);
        unitOfWork.Commit(goal);

        var retrievedGoal = unitOfWork.GetGoalModel(goal.Id);

        Assert.IsNotNull(retrievedGoal);
        Assert.AreEqual(goalAuthor.Id, retrievedGoal.GoalAuthor.Id);

        retrievedGoal.Goal.GoalAuthorId = updateGoalAuthor.Id;

        unitOfWork.Commit(retrievedGoal.Goal);

        var updatedGoal = unitOfWork.GetGoalModel(goal.Id);

        Assert.IsNotNull(updatedGoal);
        Assert.AreEqual(updateGoalAuthor.Id, updatedGoal.GoalAuthor.Id);
    }

    [TestMethod]
    public void RemoveGoalTest()
    {
        var unitOfWork = new UnitOfWork();
        var photoGenerator = new PhotoGenerator();

        var photo = photoGenerator.Generate(300, 500);
        var positionId = unitOfWork.GetAllPlayerPositions().First().Id;
        var goalAuthor = new Player(Guid.NewGuid(), "Name", new DateTime(2000, 1, 1), 180f, positionId, photo);
        var assistant = new Player(Guid.NewGuid(), "Assistant", new DateTime(2000, 1, 1), 180f, positionId, photo);
        var goal = new Goal(Guid.NewGuid(), Guid.NewGuid(), goalAuthor.Id, [assistant.Id], DateTime.Now);

        unitOfWork.Commit(goalAuthor);
        unitOfWork.Commit(assistant);
        unitOfWork.Commit(goal);

        var initialCount = unitOfWork.GetAllGoalModels().Count;

        unitOfWork.Remove(goal.Id, typeof(Goal));

        var resultCount = unitOfWork.GetAllGoalModels().Count;

        Assert.AreEqual(initialCount - 1, resultCount);
    }
}