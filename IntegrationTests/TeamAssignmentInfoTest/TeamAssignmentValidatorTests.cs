using FBChamp.Core.Entities.Soccer;
using FBChamp.Core.UnitOfWork;
using FBChamp.Infrastructure;
using static IntegrationTests.TestDataFactory;

namespace IntegrationTests.TeamAssignmentInfoTest;

[TestClass]
public class TeamAssignmentValidatorTests
{
    private IUnitOfWork? _unitOfWork;

    [TestInitialize]
    public void Initialize()
    {
        _unitOfWork = new UnitOfWork();

        Infrastructure.CleanUp();

        SeedEntity(_unitOfWork);
    }

    [TestMethod]
    public void AssignTeamToLeague_ShouldAssignTeamCorrectly()
    {
        var unassignedTeamAssignment = TeamAssignmentInfoOne;
        var leagueToAssign = League1;

        unassignedTeamAssignment!.LeagueId = leagueToAssign!.Id;

        _unitOfWork!.Commit(unassignedTeamAssignment);

        var assignedTeamsModels = _unitOfWork.GetAssignedTeamsModels(leagueToAssign.Id);

        Assert.IsNotNull(assignedTeamsModels, "The list of assigned team models should not be null.");
        Assert.AreEqual(1, assignedTeamsModels.Count, "There should be exactly 1 assigned teams in the league.");
        Assert.IsTrue(assignedTeamsModels.Any(t => t.Team.Name == "TeamOne"), "The newly assigned team's name should be in the list.");
    }

    [TestMethod]
    public void AssignTeamToLeague_ShouldReturnCRUDResultFailed_WhenTeamCountExceedsLeagueLimit()
    {
        var unassignedTeamAssignmentOne = TeamAssignmentInfoOne;
        var unassignedTeamAssignmentTwo = TeamAssignmentInfoTwo;
        var leagueToAssign = League1;

        unassignedTeamAssignmentOne!.LeagueId = leagueToAssign!.Id;
        unassignedTeamAssignmentTwo!.LeagueId = leagueToAssign!.Id;

        _unitOfWork!.Commit(unassignedTeamAssignmentOne);

       var resultTwo = _unitOfWork!.Commit(unassignedTeamAssignmentTwo);

        Assert.AreEqual(CRUDResult.Failed, resultTwo, "The result should be CRUDResult.Failed when the team count exceeds the league limit.");
    }

    [TestMethod]
    public void AssignTeamToLeague_ShouldReturnCRUDResultInvalidOperation_WhenLeagueOrTeamDoesNotExist()
    {
        var team = Team1!;

        var invalidTeamAssignment = new TeamAssignmentInfo
        {
            Id = team.Id, 
            LeagueId = Guid.NewGuid() 
        };

        var result = _unitOfWork!.Commit(invalidTeamAssignment);

        Assert.AreEqual(CRUDResult.InvalidOperation, result, "The result should be CRUDResult.InvalidOperation when the league or team does not exist.");
    }

    [TestCleanup]
    public void Dispose()
    {
        _unitOfWork?.Dispose();
    }
}
