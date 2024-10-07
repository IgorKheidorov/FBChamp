using FBChamp.Core.Entities.Soccer.Enums;
using FBChamp.Core.UnitOfWork;
using FBChamp.DataGenerators;
using FBChamp.Infrastructure;

namespace IntegrationTests.MatchesTests;

[TestClass]
public class MatchRepositoryTests
{
    private IUnitOfWork? _unitOfWork;
    private DataGenerator? _dataGenerator;

    [TestInitialize]
    public void Initialize()
    {
        _unitOfWork = new UnitOfWork();
        Infrastructure.CleanUp();
        _dataGenerator = new DataGenerator(_unitOfWork);
    }

    [TestMethod]
    public void GetAllMatchModel_ShouldReturnAllMatches()
    {
        var initialCount = _unitOfWork!.GetAllMatchModels().Count();

        _dataGenerator!.GenerateMatch(new Dictionary<string, string>
        {
            { "Count" , "1" }
        });

        var matches = _unitOfWork!.GetAllMatchModels();

        Assert.AreEqual(initialCount + 1, matches.Count, "There should be exactly one match");
    }

    [TestMethod]
    public void GetAllMatchModel_ShouldReturnEmptyList_WhenNoMatchesExists()
    {
        var matches = _unitOfWork!.GetAllMatchModels();

        Assert.IsNotNull(matches, "The list of matches should not be null.");
        Assert.AreEqual(0, matches.Count, "The list of matches should be empty when no matches exist.");
    }

    [TestMethod]
    public void GetMatchModel_ShouldReturnCorrectMatch()
    {
        _dataGenerator!.GenerateMatch(new Dictionary<string, string>
        {
            { "Count" , "1" }
        });

        var matches = _unitOfWork!.GetAllMatchModels();
        var match = matches.First();

        var matchModel = _unitOfWork!.GetMatchModel(match.Match.Id);

        Assert.IsNotNull(matchModel);
        Assert.AreEqual(match.Match.Status, MatchStatus.InProgress, "The match status should be InProgress");
        Assert.AreEqual(match.Match.GuestTeamId, matchModel.GuestTeam.Team.Id, "Guest team ID should match");
        Assert.AreEqual(match.Match.HostTeamId, matchModel.HostTeam.Team.Id, "Host team ID should match");
        Assert.AreEqual(match.Match.StadiumId, matchModel.Stadium.Stadium.Id, "Stadium ID should match");
        Assert.AreEqual(match.Match.LeagueId, matchModel.Match.LeagueId, "League ID should match");
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
