using FBChamp.Core.Entities.Soccer;
using FBChamp.Core.Repositories.Membership;

namespace FBChamp.Infrastructure.Repositories.Membership;

internal class MatchRepository : JSONRepository<Match, Guid>, IMatchRepository
{
    protected override string JSONFileName => "Matches.json";
}
