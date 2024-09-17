using FBChamp.Core.Entities.Soccer;
using FBChamp.Core.Entities.Soccer.Enums;
using FBChamp.Infrastructure;
using IntegrationTests.Helpers;

namespace IntegrationTests.GoalTests;

[TestClass]
public class GoalRepositoryTests
{
    // TODO: Proceed with unit tests after player assignment to a match is ready
    [TestInitialize]
    public void Initialize()
    {
        Infrastructure.CleanUp();
    }

    [TestMethod]
    public void AddGoalTest()
    {
       // var unitOfWork = new UnitOfWork();
       // var photoGenerator = new PhotoGenerator();
       // var initialCount = unitOfWork.GetAllGoalModels().Count;
       //
       // var photo = photoGenerator.Generate(300, 500);
       // var stadium = new Stadium(Guid.NewGuid(), "Name", Guid.NewGuid());
       // var team = new Team(Guid.NewGuid(), "Team", Guid.NewGuid(), photo, stadium.Id);
       // var team2 = new Team(Guid.NewGuid(), "Team2", Guid.NewGuid(), photo, stadium.Id);
       // var league = new League(Guid.NewGuid(), "Leagure", photo, 2, new DateTime(2024, 9, 1),
       //     new DateTime(2024, 9, 25));
       // var match = new Match(Guid.NewGuid(), stadium.Id, league.Id, MatchStatus.Scheduled,
       //     new DateTime(2024, 9, 15), team.Id, team2.Id);
       //
       //
       // var positionId = unitOfWork.GetAllPlayerPositions().First().Id;
       // var goalAuthor = new Player(Guid.NewGuid(), "Name", new DateTime(2000, 1, 1), 180f, positionId, photo);
       // var assistant = new Player(Guid.NewGuid(), "Assistant", new DateTime(2000, 1, 1), 180f, positionId, photo);
       // var goal = new Goal(Guid.NewGuid(), match.Id, goalAuthor.Id, [assistant.Id], GoalType.Normal, team2.Id,
       //     new DateTime(2024, 9, 15));
       //
       // var goalAuthorAssignment = new PlayerAssignmentInfo(goalAuthor.Id, team.Id, 2);
       // var assistantAssignment = new PlayerAssignmentInfo(assistant.Id, team.Id, 3);
       // var role = unitOfWork.GetAllPlayerPositions().First().Name;
       //
       // var goalAuthorAssignmentToMatch = new PlayerMatchAssignment(goalAuthor.Id, match.Id, new DateTime(2024, 9, 15),
       //     new DateTime(2024, 9, 15), role);
       // var assistantAssignmentToMatch = new PlayerMatchAssignment(goalAuthor.Id, match.Id, new DateTime(2024, 9, 15),
       //     new DateTime(2024, 9, 15), role);
       //
       // unitOfWork.Commit(team);
       // unitOfWork.Commit(team2);
       // unitOfWork.Commit(league);
       // unitOfWork.Commit(stadium);
       // unitOfWork.Commit(match);
       // unitOfWork.Commit(goalAuthor);
       // unitOfWork.Commit(assistant);
       // unitOfWork.Commit(goalAuthorAssignment);
       // unitOfWork.Commit(assistantAssignment);
       // unitOfWork.Commit(goalAuthorAssignmentToMatch);
       // unitOfWork.Commit(assistantAssignmentToMatch);
       // unitOfWork.Commit(goal);
       //
       // var result = unitOfWork.GetAllGoalModels().Count;
       //
       // Assert.AreEqual(initialCount + 1, result);
    }

    [TestMethod]
    public void GetGoalByIdTest()
    {
        // var unitOfWork = new UnitOfWork();
        // var photoGenerator = new PhotoGenerator();
        //
        // var photo = photoGenerator.Generate(300, 500);
        // var positionId = unitOfWork.GetAllPlayerPositions().First().Id;
        // var goalAuthor = new Player(Guid.NewGuid(), "Name", new DateTime(2000, 1, 1), 180f, positionId, photo);
        // var assistant = new Player(Guid.NewGuid(), "Assistant", new DateTime(2000, 1, 1), 180f, positionId, photo);
        // var goal = new Goal(Guid.NewGuid(), Guid.NewGuid(), goalAuthor.Id, [assistant.Id], DateTime.Now);
        //
        // unitOfWork.Commit(goalAuthor);
        // unitOfWork.Commit(assistant);
        // unitOfWork.Commit(goal);
        //
        // var result = unitOfWork.GetGoalModel(goal.Id);
        //
        // Assert.IsNotNull(result);
        // Assert.AreEqual(goal.GoalAuthorId, result.GoalAuthor.Id);
    }

    [TestMethod]
    public void UpdateGoalTest()
    {
        //  var unitOfWork = new UnitOfWork();
        //  var photoGenerator = new PhotoGenerator();
        //
        //  var photo = photoGenerator.Generate(300, 500);
        //  var positionId = unitOfWork.GetAllPlayerPositions().First().Id;
        //  var goalAuthor = new Player(Guid.NewGuid(), "Name", new DateTime(2000, 1, 1), 180f, positionId, photo);
        //  var updateGoalAuthor = new Player(Guid.NewGuid(), "Name", new DateTime(2000, 1, 1), 180f, positionId, photo);
        //
        //  var assistant = new Player(Guid.NewGuid(), "Assistant", new DateTime(2000, 1, 1), 180f, positionId, photo);
        //  var goal = new Goal(Guid.NewGuid(), Guid.NewGuid(), goalAuthor.Id, [assistant.Id], DateTime.Now);
        //
        //  unitOfWork.Commit(goalAuthor);
        //  unitOfWork.Commit(assistant);
        //  unitOfWork.Commit(updateGoalAuthor);
        //  unitOfWork.Commit(goal);
        //
        //  var retrievedGoal = unitOfWork.GetGoalModel(goal.Id);
        //
        //  Assert.IsNotNull(retrievedGoal);
        //  Assert.AreEqual(goalAuthor.Id, retrievedGoal.GoalAuthor.Id);
        //
        //  retrievedGoal.Goal.GoalAuthorId = updateGoalAuthor.Id;
        //
        //  unitOfWork.Commit(retrievedGoal.Goal);
        //
        //  var updatedGoal = unitOfWork.GetGoalModel(goal.Id);
        //
        //  Assert.IsNotNull(updatedGoal);
        //  Assert.AreEqual(updateGoalAuthor.Id, updatedGoal.GoalAuthor.Id);
    }

    [TestMethod]
    public void RemoveGoalTest()
    {
        //   var unitOfWork = new UnitOfWork();
        //   var photoGenerator = new PhotoGenerator();
        //
        //   var photo = photoGenerator.Generate(300, 500);
        //   var positionId = unitOfWork.GetAllPlayerPositions().First().Id;
        //   var goalAuthor = new Player(Guid.NewGuid(), "Name", new DateTime(2000, 1, 1), 180f, positionId, photo);
        //   var assistant = new Player(Guid.NewGuid(), "Assistant", new DateTime(2000, 1, 1), 180f, positionId, photo);
        //   var goal = new Goal(Guid.NewGuid(), Guid.NewGuid(), goalAuthor.Id, [assistant.Id], DateTime.Now);
        //
        //   unitOfWork.Commit(goalAuthor);
        //   unitOfWork.Commit(assistant);
        //   unitOfWork.Commit(goal);
        //
        //   var initialCount = unitOfWork.GetAllGoalModels().Count;
        //
        //   unitOfWork.Remove(goal.Id, typeof(Goal));
        //
        //   var resultCount = unitOfWork.GetAllGoalModels().Count;
        //
        //   Assert.AreEqual(initialCount - 1, resultCount);
    }
}