using FBChamp.Core.UnitOfWork;
using FBChamp.Infrastructure;
using static IntegrationTests.TestDataFactory;

namespace IntegrationTests.TeamAssignmentInfoTest;

[TestClass] 
public class TeamAssignmentInfoRepositoryTests
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
    public void GetUnassignedTeamModel_ShouldReturnOnlyUnassignedTeams()
    {
        var unassignedTeams = _unitOfWork!.GetUnassignedTeamModel();

        Assert.IsNotNull(unassignedTeams, "The list of unassigned teams should not be null.");
        Assert.AreEqual(2, unassignedTeams.Count, "There should be exactly 2 unassigned team.");
        Assert.IsTrue(unassignedTeams.Any(t => t.Team.Name == "TeamOne"), "TeamOne should be in the list.");
        Assert.IsTrue(unassignedTeams.Any(t => t.Team.Name == "TeamTwo"), "TeamTwo should be in the list");
    }

    [TestMethod]
    public void GetAssignedTeamsModels_ShouldReturnAssignedTeamsModels_WhenLeagueExists()
    {
        var teamOne = Team1!;
        var teamTwo = Team2!;   
        var unassignedTeamOne = TeamAssignmentInfoOne!;
        var unassignedTeamTwo = TeamAssignmentInfoTwo!;
        var league = League1!;

        league.NumberOfTeams = 3;

        unassignedTeamOne.LeagueId = league.Id;
        unassignedTeamTwo.LeagueId = league.Id;

        _unitOfWork!.Commit(unassignedTeamOne);
        _unitOfWork!.Commit(unassignedTeamTwo);

        var assignedTeamModels = _unitOfWork.GetAssignedTeamsModels(league.Id);

        Assert.IsNotNull(assignedTeamModels, "The list of assigned team models should not be null.");
        Assert.AreEqual(2, assignedTeamModels.Count);
    }

    [TestMethod]
    public void GetAssignedTeamsModels_ShouldReturnEmptyList_WhenNoTeamsAssigned()
    {
        var leagueId = League1!.Id; 

        var assignedTeamModels = _unitOfWork!.GetAssignedTeamsModels(leagueId);

        Assert.IsNotNull(assignedTeamModels, "The list of assigned team models should not be null.");
        Assert.AreEqual(0, assignedTeamModels.Count, "There should be no assigned team models.");
    }

    [TestCleanup]
    public void Dispose()
    {
        _unitOfWork?.Dispose();
    }
}
