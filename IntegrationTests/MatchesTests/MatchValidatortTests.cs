using FBChamp.Core.Entities.Soccer;
using FBChamp.Core.Entities.Soccer.Enums;
using FBChamp.Core.UnitOfWork;
using FBChamp.DataGenerators;
using FBChamp.Infrastructure;
using static IntegrationTests.TestDataFactory;

namespace IntegrationTests.MatchesTests;

[TestClass]
public class MatchValidatortTests
{
    private IUnitOfWork? _unitOfWork;
    private DataGenerator? _dataGenerator;

    [TestInitialize]
    public void Initialize()
    {
        _unitOfWork = new UnitOfWork();
        Infrastructure.CleanUp();

        _dataGenerator = new DataGenerator(_unitOfWork);

        _dataGenerator.GenerateMatch(new Dictionary<string, string>
        {
            { "Count", "1" }
        });
    }

    [TestMethod]
    public void AddMatch_ShouldAddMatchSuccessfully_WhenAllEntitiesAreValid()
    {
        var allMatches = _unitOfWork!.GetAllMatchModels();
        var match = allMatches.First();

        Assert.IsNotNull(allMatches, "The list of matches should be not null");
        Assert.AreEqual(allMatches.Count, 1, "There should be exactly one match");

        var matchModel = _unitOfWork.GetMatchModel(match.Match.Id);

        Assert.IsNotNull(matchModel);
        Assert.AreEqual(match.Match.Status, MatchStatus.InProgress, "The match status should be InProgress");
        Assert.AreEqual(match.Match.GuestTeamId, matchModel.GuestTeam.Team.Id, "Guest team ID should match");
        Assert.AreEqual(match.Match.HostTeamId, matchModel.HostTeam.Team.Id, "Host team ID should match");
        Assert.AreEqual(match.Match.StadiumId, matchModel.Stadium.Stadium.Id, "Stadium ID should match");
        Assert.AreEqual(match.Match.LeagueId, matchModel.Match.LeagueId, "League ID should match");
    }

    [TestMethod]
    public void AddMatch_ShouldReturnEntityValidationFailed_WhenGuestTeamDoesNotExists()
    {
        var allMatches = _unitOfWork!.GetAllMatchModels();
        var match = allMatches.First();
        var invalidGuestTeamId = Guid.NewGuid();

        match.Match.GuestTeamId = invalidGuestTeamId;

        var result = _unitOfWork!.Commit(match.Match);

        Assert.AreEqual(CRUDResult.EntityValidationFailed, result, "Expected validation failure because guest team does not exists");
    }

    [TestMethod]
    public void AddMatch_ShouldReturnEntityValidationFailed_WhenHostTeamDoesNotExists()
    {
        var allMatches = _unitOfWork!.GetAllMatchModels();
        var match = allMatches.First();
        var invalidHostTeamId = Guid.NewGuid();

        match.Match.HostTeamId = invalidHostTeamId;

        var result = _unitOfWork!.Commit(match.Match);

        Assert.AreEqual(CRUDResult.EntityValidationFailed, result, "Expected validation failure because host team does not exists");
    }

    [TestMethod]
    public void AddMatch_ShouldReturnEntityValidationFailed_WhenLeagueDoesNotExists()
    {
        var allMatches = _unitOfWork!.GetAllMatchModels();
        var match = allMatches.First();
        var invalidLeagueId = Guid.NewGuid();

        match.Match.LeagueId = invalidLeagueId;

        var result = _unitOfWork!.Commit(match.Match);

        Assert.AreEqual(CRUDResult.EntityValidationFailed, result, "Expected validation failure because league does not exists");
    }

    [TestMethod]
    public void AddMatch_ShouldReturnEntityValidationFailed_WhenStadiumDoesNotExists()
    {
        var allMatches = _unitOfWork!.GetAllMatchModels();
        var match = allMatches.First();
        var invalidStadiumId = Guid.NewGuid();

        match.Match.StadiumId= invalidStadiumId;

        var result = _unitOfWork!.Commit(match.Match);

        Assert.AreEqual(CRUDResult.EntityValidationFailed, result, "Expected validation failure because stadium does not exists");
    }

    [TestMethod]
    public void UpdateMatch_ShouldUpdateMatchSuccessfully_WhenAllEntitiesAreValid()
    {
        var allMatches = _unitOfWork!.GetAllMatchModels();
        var match = allMatches.First();

        var allTeamModels = _unitOfWork!.GetAllTeamModels();
        var allLeagues = _unitOfWork.GetAllLeagueModels();
        var hostTeam = allTeamModels.First();
        var guestTeam = allTeamModels.Last();
        var league = allLeagues.First();

        match.Match.GuestTeamId = guestTeam.Team.Id;
        match.Match.HostTeamId = guestTeam.Team.Id;
        match.Match.LeagueId= league.League.Id;
        match.Match.Status = MatchStatus.Delayed;

        var updateResult = _unitOfWork!.Commit(match.Match);
        var updatedMatch = _unitOfWork.GetMatchModel(match.Match.Id);

        Assert.IsNotNull(updateResult, "Update result should not be null");
        Assert.AreEqual(match.Match.LeagueId, updatedMatch.Match.LeagueId, "League ID should match");
        Assert.AreEqual(match.Match.GuestTeamId, updatedMatch.GuestTeam.Team.Id, "Guest Team ID should match");
        Assert.AreEqual(match.Match.HostTeamId, updatedMatch.HostTeam.Team.Id, "Host Team ID should match");
        Assert.AreEqual(match.Match.Status, updatedMatch.Match.Status, "Match status should match");
    }

    [TestMethod]
    public void RemoveMatch_ShouldRemoveMatchSuccessfully_WhenMatchExists()
    {
        var allMatches = _unitOfWork!.GetAllMatchModels();
        var match = allMatches.First();

        var result = _unitOfWork!.Remove(match.Match.Id, typeof(Match));

        Assert.AreEqual(result, CRUDResult.Success, "The match should be removed successfully.");
        Assert.IsNull(_unitOfWork.GetMatchModel(match.Match.Id), "The match should no longer exist in the repository.");
    }

    [TestMethod]
    public void RemoveMatch_ShouldReturnFalse_WhenMatchDoesNotExists()
    {
        var notExistingMatchId = Guid.NewGuid();

        var result = _unitOfWork!.Remove(notExistingMatchId, typeof(Match));

        Assert.AreEqual(result, CRUDResult.Failed, "The method should return false when the match does not exist.");
    }

    [TestCleanup]
    public void Dispose()
    {
        _unitOfWork?.Dispose();
    }
}
