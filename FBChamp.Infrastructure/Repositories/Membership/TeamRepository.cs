using FBChamp.Core.Entities.Socker;
using FBChamp.Core.Repositories.Membership;

namespace FBChamp.Infrastructure.Repositories.Membership;

internal class TeamRepository: JSONRepository<Team, Guid>, ITeamRepository
{
    protected override string JSONFileName => "Teams.json";
}
