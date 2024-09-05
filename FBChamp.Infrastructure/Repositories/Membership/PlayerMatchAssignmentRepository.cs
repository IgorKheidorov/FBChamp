using FBChamp.Core.Entities.Soccer;
using FBChamp.Core.Repositories.Membership;

namespace FBChamp.Infrastructure.Repositories.Membership;

internal class PlayerMatchAssignmentRepository : JSONRepository<PlayerMatchAssignment, Guid>, IPlayerMatchAssignmentRepository
{
    protected override string JSONFileName => "PlayerMatchAssignments.json";
}
