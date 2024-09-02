using FBChamp.Core.Entities.Soccer;
using FBChamp.Core.UnitOfWork;
using FBChamp.Infrastructure;
using static IntegrationTests.TestDataFactory;

namespace IntegrationTests.LeagueTest;

[TestClass]
public class LeagueValidatorTest
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
    public void AddLeagues_ShouldAddLeaguesSuccessfully_WhenAllEntitiesAreValid()
    {
        var league1 = League1;
        var league2 = League2;

        var allLeagues = _unitOfWork!.GetAllLeagueModels();

        Assert.IsNotNull(allLeagues, "The list of leagues should not be null.");
        Assert.AreEqual(2, allLeagues.Count, "There should be exactly 2 leagues.");

        var league1Model = allLeagues.FirstOrDefault(l => l.League.Id == league1!.Id);
        var league2Model = allLeagues.FirstOrDefault(l => l.League.Id == league2!.Id);

        Assert.IsNotNull(league1Model, "League1 should be included in the list.");
        Assert.IsNotNull(league2Model, "League2 should be included in the list.");

        Assert.AreEqual(league1!.FullName, league1Model.League.FullName, "League1 name should match.");
        Assert.AreEqual(league2!.FullName, league2Model.League.FullName, "League2 name should match.");
        Assert.AreEqual(league1.Description, league1Model.League.Description, "League1 description should match.");
        Assert.AreEqual(league2.Description, league2Model.League.Description, "League2 description should match.");
    }

    [TestMethod]
    public void AddLeagues_ShouldReturnEntityValidationFailed_WhenNameIsTooLong()
    {
        var longName = new string('A', 257);
        var league = new League(
            Guid.NewGuid(),
            longName,
            null,
            5,
            DateTime.Now,
            DateTime.Now.AddMonths(6),
            "Description");

        var result = _unitOfWork!.Commit(league);

        Assert.AreEqual(CRUDResult.EntityValidationFailed, result, "Expected validation failure due to the name being too long.");
    }

    [TestMethod]
    public void AddLeagues_ShouldReturnEntityValidationFailed_WhenSeasonStartDateIsAfterFinishDate()
    {
        var league = new League
        {
            Id = Guid.NewGuid(),
            Description = "Description",
            FullName = "FullName",
            SeasonStartDate = DateTime.Now.AddMonths(1),
            SeasonFinishDate = DateTime.Now,
            NumberOfTeams = 1,
        };

        var result = _unitOfWork!.Commit(league);
        Assert.AreEqual(CRUDResult.EntityValidationFailed, result, "Expected validation failure because the season start date is after the finish date");
    }

    [TestMethod]
    public void AddLeagues_ShouldReturnEntityValidationFailed_WhenLeagueNameExists()
    {
        var existingName = "ExistingLeagueName";

        _unitOfWork!.Commit(
            new League(
                Guid.NewGuid(),
                existingName,
                null,
                5,
                DateTime.Now,
                DateTime.Now.AddMonths(6),
                "Description"));

        var newLeague = new League(
            Guid.NewGuid(),
            existingName,
            null,
            5,
            DateTime.Now,
            DateTime.Now.AddMonths(6),
            "Description");

        var result = _unitOfWork.Commit(newLeague);
        Assert.AreEqual(CRUDResult.EntityValidationFailed, result, "Expected validation failure because the league name already exists");
    }

    [TestMethod]
    public void UpdateLeague_ShouldUpdateSuccessfully_WhenAllDataEntitiesAreValid()
    {
        var league = League1;

        Assert.IsNotNull(league);

        league.FullName = "Updated FullName";
        league.Description = "Updated Description";
        league.NumberOfTeams = 10;

        var updateResult = _unitOfWork!.Commit(league);

        var updatedLeague = _unitOfWork.GetLeagueModel(league!.Id);

        Assert.IsNotNull(updateResult);
        Assert.AreEqual("Updated FullName", updatedLeague.FullName);
        Assert.AreEqual("Updated Description", updatedLeague.League.Description);
        Assert.AreEqual(10, updatedLeague.League.NumberOfTeams);
    }

    [TestMethod]
    public void UpdateLeague_ShouldReturnEntityValidationFailed_WhenSeasonStartDateIsAfterFinishDate()
    {
        var league = League1;

        Assert.IsNotNull(league);

        league.SeasonStartDate = new DateTime(2002, 05, 11);
        league.SeasonFinishDate = new DateTime(2001, 05, 12);

        var updateResult = _unitOfWork!.Commit(league);

        Assert.AreEqual(CRUDResult.EntityValidationFailed, updateResult);
    }

    [TestMethod]
    public void UpdateLeague_ShouldReturnEntityValidationFailed_WhenNameIsTooLong()
    {
        var league = League1;

        Assert.IsNotNull(league);

        var longName = new string('A', 257);

        league.FullName = longName;

        var updateResult = _unitOfWork!.Commit(league);

        Assert.AreEqual(CRUDResult.EntityValidationFailed, updateResult);
    }

    [TestMethod]
    public void UpdateLeague_ShouldReturnEntityValidationFailed_WhenUpdatedNameAlreadyExists()
    {
        var leagueToUpdate = new League(
            League1!.Id,
            "League2",
            League1.Photo,
            League1.NumberOfTeams,
            League1.SeasonStartDate,
            League1.SeasonFinishDate,
            League1.Description);

        var updateResult = _unitOfWork!.Commit(leagueToUpdate);

        Assert.AreEqual(CRUDResult.EntityValidationFailed, updateResult, "The update should fail because the league name already exists.");
    }

    [TestMethod]
    public void RemoveLeague_ShouldReturnTrue_WhenLeagueExistsAndHasNoTeams()
    {
        var leagueId = Guid.NewGuid();

        var league = new League(leagueId, "League Without Teams", null, 10, DateTime.Now, DateTime.Now.AddYears(1));
        _unitOfWork!.Commit(league);

        var result = _unitOfWork!.Remove(leagueId, typeof(League));

        Assert.AreEqual(result, CRUDResult.Success, "The league should be removed successfully.");
        Assert.IsNull(_unitOfWork.GetLeagueModel(leagueId), "The league should no longer exist in the repository.");
    }

    [TestMethod]
    public void RemoveLeague_ShouldReturnFalse_WhenLeagueDoesNotExist()
    {
        var nonExistentLeagueId = Guid.NewGuid();

        var result = _unitOfWork!.Remove(nonExistentLeagueId, typeof(League));

        Assert.AreEqual(result, CRUDResult.Failed, "The method should return false when the league does not exist.");
    }

    [TestCleanup]
    public void Dispose()
    {
        _unitOfWork?.Dispose();
    }
}
