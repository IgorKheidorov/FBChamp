using FBChamp.Core.Entities.Socker;
using FBChamp.Core.Repositories.Membership;

namespace FBChamp.Infrastructure.Repositories.Membership;

internal class PlayerAssignmentInfoRepository: JSONRepository<PlayerAssignmentInfo, Guid>, IPlayerAssignmentInfoRepository
{
    protected override string JSONFileName => "PlayerAssignmentInfo.json";   
}
