using FBChamp.Core.Entities.Socker;
using FBChamp.Core.Repositories.Membership;

namespace FBChamp.Infrastructure.Repositories.Membership;

internal class PlayerRepository : JSONRepository<Player,Guid>, IPlayerRepository
{
    protected override string JSONFileName => "Players.json";
}
  