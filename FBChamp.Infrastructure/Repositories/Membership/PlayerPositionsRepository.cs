using FBChamp.Core.Entities.Socker;
using FBChamp.Core.Repositories.Membership;

namespace FBChamp.Infrastructure.Repositories.Membership;

internal class PlayerPositionsRepository: JSONRepository<PlayerPosition, Guid>, IPlayerPositionsRepository
{
    protected override string JSONFileName => "PlayerPositions.json";
}
