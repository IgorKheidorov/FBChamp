using FBChamp.Core.Entities;
using FBChamp.Core.Entities.Socker;
using FBChamp.Core.Repositories.Membership;
using Microsoft.IdentityModel.Tokens;

namespace FBChamp.Infrastructure.Repositories.Membership;

internal class PlayerAssignmentInfoRepository: JSONRepository<PlayerAssignmentInfo, Guid>, IPlayerAssignmentInfoRepository
{
    protected override string JSONFileName => "PlayerAssignmentInfo.json";

    protected override bool CheckEntityValidity(PlayerAssignmentInfo entity) =>
      EntityList.Where(x => x.Value.PlayingNumber == entity.PlayingNumber && x.Value.TeamId == entity.TeamId).IsNullOrEmpty();
}
