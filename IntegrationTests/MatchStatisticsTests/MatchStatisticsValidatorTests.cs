using FBChamp.Core.Entities.Soccer;
using FBChamp.Core.UnitOfWork;
using FBChamp.Infrastructure;
using static IntegrationTests.TestDataFactory;

namespace IntegrationTests.MatchStatisticsTests;

[TestClass]
public class MatchStatisticsValidatorTests
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
    public void AddMatchStatisticsToMatch_ShouldAddStatisticsSuccessfully()
    {
        var matchStatistics = MatchStatistics1!;

        var statistic = _unitOfWork!.GetMatchStatisticsModel(matchStatistics.MatchId);

        Assert.IsNotNull(statistic);
        Assert.AreEqual(statistic.MatchStatistics.MatchId, matchStatistics.MatchId);
        Assert.AreEqual(statistic.MatchStatistics.Id, matchStatistics.Id);
        Assert.AreEqual(statistic.MatchStatistics.NumberOfWatchers, matchStatistics.NumberOfWatchers);  
    }

    [TestMethod]
    public void AddMatchStatisticsToMatch_ShouldReturnEntityValidationFailed_WhenMatchDoesNotExists()
    {
        var matchStatistics = MatchStatistics1!;
        matchStatistics.MatchId = Guid.NewGuid();

       var result = _unitOfWork!.Commit(matchStatistics);

        Assert.AreEqual(CRUDResult.EntityValidationFailed, result);
    }

    [TestMethod]
    public void AddMatchStatisticsToMatch_ShouldReturnEntityValidationFailed_WhenNumberOfWatchersIsInvalid()
    {
        var matchStatistics = MatchStatistics1!;
        matchStatistics.NumberOfWatchers = -100;

        var result = _unitOfWork!.Commit(matchStatistics);

        Assert.AreEqual(CRUDResult.EntityValidationFailed, result);
    }

    [TestMethod]
    public void AddMatchStatisticsToMatch_ShouldAddStatisticsSuccessfully_WhenNumberOfWatchersIsValid()
    {
        var matchStatistics = MatchStatistics1!;
        matchStatistics.NumberOfWatchers = 5000;

        var result = _unitOfWork!.Commit(matchStatistics);

        Assert.AreEqual(CRUDResult.Success, result);

        var statistic = _unitOfWork.GetMatchStatisticsModel(matchStatistics.MatchId);

        Assert.IsNotNull(statistic);
        Assert.AreEqual(statistic.MatchStatistics.NumberOfWatchers, matchStatistics.NumberOfWatchers);
    }

    [TestMethod]
    public void RemoveMatchStatistics_ShouldRemoveSuccessfully_WhenMatchStatisticsExists()
    {
        var matchStatistics = MatchStatistics1!;

        var result = _unitOfWork!.Remove(matchStatistics.Id, typeof(MatchStatistics));

        Assert.AreEqual(CRUDResult.Success, result);
        Assert.IsNull(_unitOfWork.GetMatchStatisticsModel(matchStatistics.MatchId));
    }

    [TestMethod]
    public void RemoveMatchStatistics_ShouldReturnCRUDResultFailed_WhenMatchStatisticsDoesNotExists()
    {
        var notExistingMatchStatisticsId = Guid.NewGuid();

        var result = _unitOfWork!.Remove(notExistingMatchStatisticsId, typeof(MatchStatistics));

        Assert.AreEqual(CRUDResult.Failed, result);
    }

    [TestMethod]
    public void UpdateMatchStatistics_ShouldUpdateSuccessfully()
    {
        var matchStatistics = MatchStatistics1!;
        matchStatistics.NumberOfWatchers = 1532;
        matchStatistics.MatchId = Match2!.Id;

        var result = _unitOfWork!.Commit(matchStatistics);

        var updatedMatchStatistic = _unitOfWork.GetMatchStatisticsModel(matchStatistics.MatchId);

        Assert.AreEqual(CRUDResult.Success, result);
        Assert.AreEqual(matchStatistics.NumberOfWatchers, 1532);
        Assert.AreEqual(matchStatistics.MatchId, updatedMatchStatistic.MatchStatistics.MatchId);
    }
}

