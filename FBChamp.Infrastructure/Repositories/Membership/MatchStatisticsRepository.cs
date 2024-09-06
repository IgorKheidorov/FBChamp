using FBChamp.Core.Entities.Soccer;
using FBChamp.Core.Repositories.Membership;

namespace FBChamp.Infrastructure.Repositories.Membership;

internal class MatchStatisticsRepository : JSONRepository<MatchStatistics, Guid>, IMatchStatisticsRepository
{
    protected override string JSONFileName => "MatchStatistics.json";
}
