using FBChamp.Core.Entities.Socker;

namespace FBChamp.Core.Repositories.Membership;

public interface IPlayerRepository: IRepository<Player, Guid>
{
    //PagedList<Player> GetPagedList(PageInfo pageInfo, string filter, Expression<Func<Player, bool>> predicate = null);        
}
