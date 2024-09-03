using System.Text.Json;
using System.Linq.Expressions;
using FBChamp.Core.Entities.Soccer;
using FBChamp.Core.Repositories.Membership;

namespace FBChamp.Infrastructure.Repositories.Membership
{
    internal class LocationAssignmentInfoRepository : JSONRepository<LocationAssignmentInfo, Guid>, ILocationAssignmentInfoRepository
    {
        protected override string JSONFileName => "Countries.json";        
    }
}
