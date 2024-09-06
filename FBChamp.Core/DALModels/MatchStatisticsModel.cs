using FBChamp.Core.Entities.Soccer;

namespace FBChamp.Core.DALModels;

public class MatchStatisticsModel : EntityModel
{
    public MatchStatistics MatchStatistics { get; set; }

    public MatchStatisticsModel()
    {
    }

    public MatchStatisticsModel(MatchStatistics matchStatistics)
    {
        MatchStatistics = matchStatistics;
    }
}
