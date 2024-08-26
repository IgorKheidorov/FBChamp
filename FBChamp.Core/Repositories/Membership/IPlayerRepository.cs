using FBChamp.Core.Entities.Soccer;

namespace FBChamp.Core.Repositories.Membership;

public interface IPlayerRepository: IRepository<Player, Guid>
{
    //PagedList<Player> GetPagedList(PageInfo pageInfo, string filter, Expression<Func<Player, bool>> predicate = null);        
}
