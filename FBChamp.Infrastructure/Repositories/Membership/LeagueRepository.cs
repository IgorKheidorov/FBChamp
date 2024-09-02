using FBChamp.Core.Entities.Soccer;
using FBChamp.Core.Repositories.Membership;

namespace FBChamp.Infrastructure.Repositories.Membership;

internal class LeagueRepository : JSONRepository<League, Guid>, ILeagueRepository
{
    protected override string JSONFileName => "Leagues.json";
}
