using FBChamp.Core.Entities.Socker;
using FBChamp.Core.Repositories.Membership;

namespace FBChamp.Infrastructure.Repositories.Membership;

internal class CoachAssignmentInfoRepository : JSONRepository<CoachAssignmentInfo, Guid>, ICoachAssignmentInfoRepository
{
    protected override string JSONFileName => "CoachAssignmentInfo.json";   
}
