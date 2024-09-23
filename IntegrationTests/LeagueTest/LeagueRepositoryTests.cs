using FBChamp.Core.UnitOfWork;
using FBChamp.DataGenerators;
using FBChamp.Infrastructure;

namespace IntegrationTests.LeagueTest;

[TestClass]
public class LeagueRepositoryTests
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
    public void GetAllLeagueModels_ShouldReturnAllLeagues()
    {
        var leagues = _unitOfWork!.GetAllLeagueModels();
        var leagueModel = leagues.First();

        Assert.IsNotNull(leagues, "The list of leagues should not be null.");
        Assert.AreEqual(1, leagues.Count, "There should be exactly 2 leagues.");

        Assert.IsNotNull(leagueModel, "League0 should be included in the list.");
    }

    [TestMethod]
    public void GetLeagueModel_ShouldReturnCorrectLeague()
    {
        var leagues = _unitOfWork!.GetAllLeagueModels();
        var leagueModel = leagues.First();

        var league = _unitOfWork!.GetLeagueModel(leagueModel.League.Id);

        Assert.IsNotNull(league, "The league model should not be null.");
        Assert.AreEqual("League0", league.League.FullName, "The league name should match.");
        Assert.AreEqual("No information", league.League.Description, "The league description should match.");
        Assert.IsNotNull(league.League.Photo);
    }

    [TestMethod]
    public void GetLeagueModel_ShouldReturnNull_WhenLeagueDoesNotExist()
    {
        var nonExistentLeagueId = Guid.NewGuid();

        var leagueModel = _unitOfWork!.GetLeagueModel(nonExistentLeagueId);

        Assert.IsNull(leagueModel, "The league model should be null when the league does not exist.");
    }
}
