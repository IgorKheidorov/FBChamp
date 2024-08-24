using FBChamp.Core.Entities;

namespace FBChamp.Core.Repositories;

public interface IRepository<TEntity, in TKey> : IRepository<TEntity> where TEntity : Entity<TKey>
{
    TEntity Find(TKey id);

    void Commit() 
    {
    }

    bool Remove(TKey id);
}