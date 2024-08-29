using FBChamp.Core.Entities.Soccer;
using FBChamp.Core.Repositories.Membership;

namespace FBChamp.Infrastructure.Repositories.Membership;

internal class StadiumRepository : JSONRepository<Stadium, Guid>, IStadiumRepository
{
    protected override string JSONFileName => "Stadiums.json";
}