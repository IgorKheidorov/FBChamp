using FBChamp.Core.Entities.Soccer;
using FBChamp.Core.UnitOfWork;
using FBChamp.Infrastructure;
using static IntegrationTests.TestDataFactory;

namespace IntegrationTests.MatchStatisticsTests;

[TestClass]
public class MatchStatisticsRepositoryTests
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
    public void GetMatchStatisticsModel_ShouldReturnCorrectMatchStatisticsModel()
    {
        var matchStatistics = MatchStatistics1!;

        var statistic = _unitOfWork!.GetMatchStatisticsModel(matchStatistics.MatchId);

        Assert.IsNotNull(statistic);
        Assert.AreEqual(statistic.MatchStatistics.MatchId, matchStatistics.MatchId);
        Assert.AreEqual(statistic.MatchStatistics.Id, matchStatistics.Id);
        Assert.AreEqual(statistic.MatchStatistics.NumberOfWatchers, matchStatistics.NumberOfWatchers);
    }

    [TestMethod]
    public void GetMatchStatisticsModel_ShouldReturncNull_WhenMatchStatisticsModelIsNull()
    {
        var matchStatistics = MatchStatistics1!;

        _unitOfWork!.Remove(matchStatistics.Id, typeof(MatchStatistics));

        var statistics = _unitOfWork.GetMatchStatisticsModel(matchStatistics.MatchId);

        Assert.IsNull(statistics);
    }
}
