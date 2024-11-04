using FBChamp.Core.Entities.Soccer;
using FBChamp.Core.Entities.Soccer.Enums;
using FBChamp.Core.UnitOfWork;
using FBChamp.Infrastructure;
using static IntegrationTests.TestDataFactory;

namespace IntegrationTests.MatchesTests;

[TestClass]
public class MatchRepositoryTests
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
    public void GetAllMatchModel_ShouldReturnAllMatches()
    {
        var matches = _unitOfWork!.GetAllMatchModels();

        Assert.IsNotNull(matches, "The list of matches should not be null.");
        Assert.AreEqual(2, matches.Count, "There should be exactly 2 matches");

        var matchModel = matches.FirstOrDefault(m => m.Match.Id == Match1!.Id);

        Assert.IsNotNull(matchModel, "Match1 should be included in the list.");
    }

    [TestMethod]
    public void GetAllMatchModel_ShouldReturnEmptyList_WhenNoMatchesExists()
    {
        _unitOfWork!.Remove(Match1!.Id, typeof(Match));

        var matches = _unitOfWork!.GetAllMatchModels();

        Assert.IsNotNull(matches, "The list of matches should not be null.");
        Assert.AreEqual(1, matches.Count, "The list of matches should contain only 1 match.");
    }

    [TestMethod]
    public void GetMatchModel_ShouldReturnCorrectMatch()
    {
        var match = Match1!;

        var matchModel = _unitOfWork!.GetMatchModel(match.Id);

        Assert.IsNotNull(matchModel);
        Assert.AreEqual(match.Status, MatchStatus.InProgress, "The match status should be InProgress");
        Assert.AreEqual(match.GuestTeamId, matchModel.GuestTeam.Team.Id, "Guest team ID should match");
        Assert.AreEqual(match.HostTeamId, matchModel.HostTeam.Team.Id, "Host team ID should match");
        Assert.AreEqual(match.StadiumId, matchModel.Stadium.Stadium.Id, "Stadium ID should match");
        Assert.AreEqual(match.LeagueId, matchModel.Match.LeagueId, "League ID should match");
    }

    [TestMethod]
    public void GetMatchModel_ShouldReturnNull_WhenMatchDoesNotExists()
    {
        var notExistingMatchId = Guid.NewGuid();

        var matchModel = _unitOfWork!.GetMatchModel(notExistingMatchId);

        Assert.IsNull(matchModel, "The match model should be null when the match does not exist.");
    }

    [TestCleanup]
    public void Dispose()
    {
        _unitOfWork!.Dispose();
    }
}
