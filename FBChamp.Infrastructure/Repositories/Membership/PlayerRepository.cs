using FBChamp.Common.Paging;
using FBChamp.Core.Entities.Socker;
using FBChamp.Core.Exceptions;
using FBChamp.Core.Repositories;
using FBChamp.Core.Repositories.Membership;
using System.Linq.Expressions;

namespace FBChamp.Infrastructure.Repositories.Membership;

internal class PlayerRepository : JSONRepository<Player,Guid>, IPlayerRepository
{
    protected override string JSONFileName => "Players.json";
}
  