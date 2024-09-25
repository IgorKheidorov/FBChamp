using FBChamp.Core.Entities.Soccer;
using FBChamp.Core.UnitOfWork;
using FBChamp.DataGenerators;
using FBChamp.Infrastructure;

namespace IntegrationTests.LeagueTest;

[TestClass]
public class LeagueValidatorTest
{
    private IUnitOfWork? _unitOfWork;
    private DataGenerator? _dataGenerator;

    [TestInitialize]
    public void Initialize()
    {
        Infrastructure.CleanUp();

        _unitOfWork = new UnitOfWork();
        _dataGenerator = new DataGenerator(_unitOfWork);

        _dataGenerator.GenerateLeague(new Dictionary<string, string>
        {
            {"Count", "1" }
        });
    }

    [TestMethod]
    public void AddLeagues_ShouldAddLeaguesSuccessfully_WhenAllEntitiesAreValid()
    {
        var allLeagues = _unitOfWork!.GetAllLeagueModels();

        Assert.IsNotNull(allLeagues);

        var leagueModel = allLeagues.First();

        Assert.AreEqual(3,leagueModel.League.NumberOfTeams);
        Assert.AreEqual("No information", leagueModel.League.Description);
    }

    [TestMethod]
    public void AddLeagues_ShouldReturnEntityValidationFailed_WhenNameIsTooLong()
    {
        var allLeagues = _unitOfWork!.GetAllLeagueModels();
        var leagueModel = allLeagues.First();

        var longName = new string('A', 257);

        leagueModel.League.FullName = longName;

   
        var result = _unitOfWork!.Commit(leagueModel.League);

        Assert.AreEqual(CRUDResult.EntityValidationFailed, result, "Expected validation failure due to the name being too long.");
    }

    [TestMethod]
    public void AddLeagues_ShouldReturnEntityValidationFailed_WhenSeasonStartDateIsAfterFinishDate()
    {
        var allLeagues = _unitOfWork!.GetAllLeagueModels();
        var leagueModel = allLeagues.First();

        leagueModel.League.SeasonStartDate = DateTime.Now.AddMonths(1);
        leagueModel.League.SeasonFinishDate = DateTime.Now;

        var result = _unitOfWork!.Commit(leagueModel.League);
        Assert.AreEqual(CRUDResult.EntityValidationFailed, result, "Expected validation failure because the season start date is after the finish date");
    }

    [TestMethod]
    public void AddLeagues_ShouldReturnEntityValidationFailed_WhenLeagueNameExists()
    {
        _dataGenerator!.GenerateLeague(new Dictionary<string, string>
        {
            {"Count", "1" }
        });

        var allLeagues = _unitOfWork!.GetAllLeagueModels();
        var leagueModel = allLeagues.Last();

        leagueModel.League.FullName = "League0";

        var result = _unitOfWork.Commit(leagueModel.League);

        Assert.AreEqual(CRUDResult.EntityValidationFailed, result, "Expected validation failure because the league name already exists");
    }

    [TestMethod]
    public void UpdateLeague_ShouldUpdateSuccessfully_WhenAllDataEntitiesAreValid()
    {
        var allLeagues = _unitOfWork!.GetAllLeagueModels();
        var leagueModel = allLeagues.First();

        leagueModel.League.FullName = "Updated FullName";
        leagueModel.League.Description = "Updated Description";
        leagueModel.League.NumberOfTeams = 10;

        var updateResult = _unitOfWork!.Commit(leagueModel.League);

        var updatedLeague = _unitOfWork.GetLeagueModel(leagueModel.League.Id);

        Assert.IsNotNull(updateResult);
        Assert.AreEqual("Updated FullName", updatedLeague.FullName);
        Assert.AreEqual("Updated Description", updatedLeague.League.Description);
        Assert.AreEqual(10, updatedLeague.League.NumberOfTeams);
    }

    [TestMethod]
    public void UpdateLeague_ShouldReturnEntityValidationFailed_WhenSeasonStartDateIsAfterFinishDate()
    {
        var allLeagues = _unitOfWork!.GetAllLeagueModels();
        var leagueModel = allLeagues.First();

        leagueModel.League.SeasonStartDate = new DateTime(2002, 05, 11);
        leagueModel.League.SeasonFinishDate = new DateTime(2001, 05, 12);

        var updateResult = _unitOfWork!.Commit(leagueModel.League);

        Assert.AreEqual(CRUDResult.EntityValidationFailed, updateResult);
    }

    [TestMethod]
    public void UpdateLeague_ShouldReturnEntityValidationFailed_WhenNameIsTooLong()
    {
        var allLeagues = _unitOfWork!.GetAllLeagueModels();
        var leagueModel = allLeagues.First();

        var longName = new string('A', 257);

        leagueModel.League.FullName = longName;

        var updateResult = _unitOfWork!.Commit(leagueModel.League);

        Assert.AreEqual(CRUDResult.EntityValidationFailed, updateResult);
    }

    [TestMethod]
    public void UpdateLeague_ShouldReturnEntityValidationFailed_WhenUpdatedNameAlreadyExists()
    {
        _dataGenerator!.GenerateLeague(new Dictionary<string, string>
        {
            {"Count", "1" }
        });

        var allLeagues = _unitOfWork!.GetAllLeagueModels();
        var leagueModel = allLeagues.Last();

        leagueModel.League.FullName = "League0";

        var updateResult = _unitOfWork!.Commit(leagueModel.League);

        Assert.AreEqual(CRUDResult.EntityValidationFailed, updateResult, "The update should fail because the league name already exists.");
    }

    [TestMethod]
    public void RemoveLeague_ShouldReturnTrue_WhenLeagueExistsAndHasNoTeams()
    {
        var allLeagues = _unitOfWork!.GetAllLeagueModels();
        var leagueModel = allLeagues.Last();

        leagueModel.League.NumberOfTeams = 0;

        _unitOfWork.Commit(leagueModel.League);

        var result = _unitOfWork!.Remove(leagueModel.League.Id, typeof(League));

        Assert.AreEqual(result, CRUDResult.Success, "The league should be removed successfully.");
        Assert.IsNull(_unitOfWork.GetLeagueModel(leagueModel.League.Id), "The league should no longer exist in the repository.");
    }

    [TestMethod]
    public void RemoveLeague_ShouldReturnFalse_WhenLeagueDoesNotExist()
    {
        var nonExistentLeagueId = Guid.NewGuid();

        var result = _unitOfWork!.Remove(nonExistentLeagueId, typeof(League));

        Assert.AreEqual(result, CRUDResult.Failed, "The method should return false when the league does not exist.");
    }

    [TestMethod]
    public void RemoveLeague_ShouldRemoveLeagueSuccessfully_AndDeassignAllTeamsInLeague()
    {
        var allLeagues = _unitOfWork!.GetAllLeagueModels();
        var leagueModel = allLeagues.Last();

        var assignedTeamsBeforeRemoval = _unitOfWork.GetAssignedTeamsModels(leagueModel.League.Id);
        Assert.AreEqual(3, assignedTeamsBeforeRemoval.Count, "There should be 2 assigned teams in the league before removal.");

        var result = _unitOfWork!.Remove(leagueModel.League.Id, typeof(League));

        var assignedTeamsAfterRemoval = _unitOfWork.GetAssignedTeamsModels(leagueModel.League.Id);

        Assert.AreEqual(0, assignedTeamsAfterRemoval.Count, "There should be no assigned teams in the league after removal.");

        var allTeams = _unitOfWork.GetAllTeamModels();
        var teamOne = allTeams.First();
        var teamTwo = allTeams.Last();
        var allTeamIds = allTeams.Select(t => t.Team.Id).ToList();
        var assignedTeamsIds = assignedTeamsAfterRemoval.Select(t => t.Team.Id).ToList();

        Assert.IsTrue(allTeamIds.Contains(teamOne.Team.Id) && allTeamIds.Contains(teamTwo.Team.Id), "Teams should still exist in the system.");
        Assert.IsFalse(assignedTeamsIds.Contains(teamOne.Team.Id) || assignedTeamsIds.Contains(teamTwo.Team.Id), "Teams should not be assigned to any league after removal.");
    }

    [TestCleanup]
    public void Dispose()
    {
        _unitOfWork?.Dispose();
    }
}
