using System.Linq.Expressions;
using FBChamp.Core.Entities;

namespace FBChamp.Core.Repositories;

public interface IRepository<TEntity> : IRepository where TEntity : Entity
{
    TEntity Find(Expression<Func<TEntity, bool>> predicate);
    IEnumerable<TEntity> All();
    IEnumerable<TEntity> Filter(Expression<Func<TEntity, bool>> predicate);

    bool AddOrUpdate(TEntity entity);
    bool Remove(TEntity entity);    
}