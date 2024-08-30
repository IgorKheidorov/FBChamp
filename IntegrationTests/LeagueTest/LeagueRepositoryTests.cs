using FBChamp.Core.Entities.Soccer;
using FBChamp.Core.UnitOfWork;
using FBChamp.Infrastructure;
using static IntegrationTests.TestDataFactory;

namespace IntegrationTests.LeagueTest;

[TestClass]
public class LeagueRepositoryTests
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
    public void GetAllLeagueModels_ShouldReturnAllLeagues()
    {
        var leagues = _unitOfWork!.GetAllLeagueModels();

        Assert.IsNotNull(leagues, "The list of leagues should not be null.");
        Assert.AreEqual(2, leagues.Count, "There should be exactly 2 leagues.");

        var league1Model = leagues.FirstOrDefault(l => l.League.FullName == League1!.FullName);
        var league2Model = leagues.FirstOrDefault(l => l.League.FullName == League2!.FullName);

        Assert.IsNotNull(league1Model, "League1 should be included in the list.");
        Assert.IsNotNull(league2Model, "League2 should be included in the list.");

        Assert.AreEqual(League1!.Description, league1Model.League.Description, "Description for League1 should match.");
        Assert.AreEqual(League2!.Description, league2Model.League.Description, "Description for League2 should match.");
    }

    [TestMethod]
    public void GetAllLeagueModels_ShouldReturnEmptyList_WhenNoLeaguesExist()
    {
        _unitOfWork!.Remove(League1!.Id,typeof(League));
        _unitOfWork!.Remove(League2!.Id, typeof(League));

        var leagues = _unitOfWork.GetAllLeagueModels();

        Assert.IsNotNull(leagues, "The list of leagues should not be null.");
        Assert.AreEqual(0, leagues.Count, "The list of leagues should be empty when no leagues exist.");
    }

    [TestMethod]
    public void GetLeagueModel_ShouldReturnCorrectLeague()
    {
        var league = League1!;

        var leagueModel = _unitOfWork!.GetLeagueModel(league.Id);

        Assert.IsNotNull(leagueModel, "The league model should not be null.");
        Assert.AreEqual("League1", leagueModel.League.FullName, "The league name should match.");
        Assert.AreEqual("Description1", leagueModel.League.Description, "The league description should match.");
        Assert.IsNotNull(leagueModel.League.Photo);
    }

    [TestMethod]
    public void GetLeagueModel_ShouldReturnNull_WhenLeagueDoesNotExist()
    {
        var nonExistentLeagueId = Guid.NewGuid();

        var leagueModel = _unitOfWork!.GetLeagueModel(nonExistentLeagueId);

        Assert.IsNull(leagueModel, "The league model should be null when the league does not exist.");
    }
}
