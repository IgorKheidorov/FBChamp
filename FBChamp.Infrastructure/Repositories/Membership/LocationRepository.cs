using FBChamp.Core.Entities.Soccer;
using FBChamp.Core.Repositories.Membership;

namespace FBChamp.Infrastructure.Repositories.Membership
{
    internal class LocationRepository : JSONRepository<Location, Guid>, ILocationRepository
    {
        protected override string JSONFileName => "Countries.json";
    }
}
