using FBChamp.Common.Paging;
using FBChamp.Core.Entities.Socker;
using System.Linq.Expressions;

namespace FBChamp.Core.Repositories.Membership
{
    public interface IPlayerRepository: IRepository<Player, Guid>
    {
        //PagedList<Player> GetPagedList(PageInfo pageInfo, string filter, Expression<Func<Player, bool>> predicate = null);        
    }
}
