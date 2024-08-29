using FBChamp.Core.Entities.Soccer;
using FBChamp.Core.Repositories.Membership;

namespace FBChamp.Infrastructure.Repositories.Membership;

internal class TeamAssignmentInfoRepository : JSONRepository<TeamAssignmentInfo,Guid>, ITeamAssignmentInfoRepository
{
    protected override string JSONFileName => "TeamAssignmentInfo.json";
}
