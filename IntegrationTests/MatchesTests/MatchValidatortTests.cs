using FBChamp.Core.Entities.Soccer;
using FBChamp.Core.Entities.Soccer.Enums;
using FBChamp.Core.UnitOfWork;
using FBChamp.Infrastructure;
using static IntegrationTests.TestDataFactory;

namespace IntegrationTests.MatchesTests;

[TestClass]
public class MatchValidatortTests
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
    public void AddMatch_ShouldAddMatchSuccessfully_WhenAllEntitiesAreValid()
    {
        var match = Match1!;

        var allMatches = _unitOfWork!.GetAllMatchModels();

        Assert.IsNotNull(allMatches, "The list of matches should be not null");
        Assert.AreEqual(allMatches.Count, 1, "There should be exactly one match");

        var matchModel = _unitOfWork.GetMatchModel(match.Id);

        Assert.IsNotNull(matchModel);
        Assert.AreEqual(match.Status, MatchStatus.InProgress, "The match status should be InProgress");
        Assert.AreEqual(match.GuestTeamId, matchModel.GuestTeam.Team.Id, "Guest team ID should match");
        Assert.AreEqual(match.HostTeamId, matchModel.HostTeam.Team.Id, "Host team ID should match");
        Assert.AreEqual(match.StadiumId, matchModel.Stadium.Stadium.Id, "Stadium ID should match");
        Assert.AreEqual(match.LeagueId, matchModel.Match.LeagueId, "League ID should match");
    }

    [TestMethod]
    public void AddMatch_ShouldReturnEntityValidationFailed_WhenGuestTeamDoesNotExists()
    {
        var match = Match1!;
        var invalidGuestTeamId = Guid.NewGuid();

        match.GuestTeamId = invalidGuestTeamId;

        var result = _unitOfWork!.Commit(match);

        Assert.AreEqual(CRUDResult.EntityValidationFailed, result, "Expected validation failure because guest team does not exists");
    }

    [TestMethod]
    public void AddMatch_ShouldReturnEntityValidationFailed_WhenHostTeamDoesNotExists()
    {
        var match = Match1!;
        var invalidHostTeamId = Guid.NewGuid();

        match.HostTeamId = invalidHostTeamId;

        var result = _unitOfWork!.Commit(match);

        Assert.AreEqual(CRUDResult.EntityValidationFailed, result, "Expected validation failure because host team does not exists");
    }

    [TestMethod]
    public void AddMatch_ShouldReturnEntityValidationFailed_WhenLeagueDoesNotExists()
    {
        var match = Match1!;
        var invalidLeagueId = Guid.NewGuid();

        match.LeagueId = invalidLeagueId;

        var result = _unitOfWork!.Commit(match);

        Assert.AreEqual(CRUDResult.EntityValidationFailed, result, "Expected validation failure because league does not exists");
    }

    [TestMethod]
    public void AddMatch_ShouldReturnEntityValidationFailed_WhenStadiumDoesNotExists()
    {
        var match = Match1!;
        var invalidStadiumId = Guid.NewGuid();

        match.StadiumId= invalidStadiumId;

        var result = _unitOfWork!.Commit(match);

        Assert.AreEqual(CRUDResult.EntityValidationFailed, result, "Expected validation failure because stadium does not exists");
    }

    [TestMethod]
    public void UpdateMatch_ShouldUpdateMatchSuccessfully_WhenAllEntitiesAreValid()
    {
        var match = Match1!;

        match.GuestTeamId = Team1!.Id;
        match.HostTeamId = Team2!.Id;
        match.LeagueId= League2!.Id;
        match.Status = MatchStatus.Delayed;

        var updateResult = _unitOfWork!.Commit(match);
        var updatedMatch = _unitOfWork.GetMatchModel(match.Id);

        Assert.IsNotNull(updateResult, "Update result should not be null");
        Assert.AreEqual(match.LeagueId, updatedMatch.Match.LeagueId, "League ID should match");
        Assert.AreEqual(match.GuestTeamId, updatedMatch.GuestTeam.Team.Id, "Guest Team ID should match");
        Assert.AreEqual(match.HostTeamId, updatedMatch.HostTeam.Team.Id, "Host Team ID should match");
        Assert.AreEqual(match.Status, updatedMatch.Match.Status, "Match status should match");
    }

    [TestMethod]
    public void RemoveMatch_ShouldRemoveMatchSuccessfully_WhenMatchExists()
    {
        var match = Match1!;

        var result = _unitOfWork!.Remove(match.Id, typeof(Match));

        Assert.AreEqual(result, CRUDResult.Success, "The match should be removed successfully.");
        Assert.IsNull(_unitOfWork.GetMatchModel(match.Id), "The match should no longer exist in the repository.");
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
