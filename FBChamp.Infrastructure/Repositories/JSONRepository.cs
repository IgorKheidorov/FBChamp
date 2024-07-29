using FBChamp.Core.Exceptions;
using FBChamp.Core.Repositories;
using FBChamp.Infrastructure.Helpers;
using System.Collections.Concurrent;
using System.Linq.Expressions;
using System.Text.Json;

namespace FBChamp.Infrastructure.Repositories;

internal abstract class JSONRepository<TEntity,TKey> : IRepository<TEntity>
    where TEntity : Entity<TKey>
{      
    protected readonly ConcurrentDictionary<TKey,TEntity> EntityList = new ConcurrentDictionary<TKey, TEntity>();
    protected virtual string JSONFileName => "NoName.json";
    protected string FullFileName => Path.Combine("..\\", Directory.GetCurrentDirectory(), "Data", JSONFileName);

    object locker = new object();
    protected JSONRepository()
    {
        Load();
    }

    public void Load()    
    {
        EntityList.Clear();
       
        if (!File.Exists(FullFileName))
        {
            Commit();
        }
        var context = ThreadSafeFileWriter.ReadFile(FullFileName);

        var items = JsonSerializer.Deserialize<ConcurrentDictionary<TKey, TEntity>>(context)!;
        foreach (var item in items)
        {
            EntityList.TryAdd(item.Key, item.Value);
        }        
    }

    public void Commit()
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        string context = JsonSerializer.Serialize(EntityList, options);
        ThreadSafeFileWriter.WriteFile(JsonSerializer.Serialize(EntityList, options), FullFileName);     
    }

    public TEntity Find(Expression<Func<TEntity, bool>> predicate)
    {
        ArgumentNullException.ThrowIfNull(predicate);

        try
        {
            return InternalFind(predicate);
        }
        catch (Exception ex)
        {
            throw new RepositoryException(ex.Message, ex);
        }
    }

    public IEnumerable<TEntity> All() => Filter(x => true);

    public IEnumerable<TEntity> Filter(Expression<Func<TEntity, bool>> predicate)
    {
        ArgumentNullException.ThrowIfNull(predicate);

        try
        {
            return InternalFilter(predicate);
        }
        catch (Exception ex)
        {
            throw new RepositoryException(ex.Message, ex);
        }
    }

    protected virtual bool CheckEntityValidity(TEntity entity) => true;

    public bool AddOrUpdate(TEntity entity)
    {
        if (entity is null || ! CheckEntityValidity(entity))
        {
            return false;
        }

        if(!EntityList.TryAdd(entity.Id, entity))
        {
            return EntityList.TryUpdate(entity.Id, entity, EntityList[entity.Id]);
        }

        return true;
    }

    public bool Remove(TEntity entity) => 
        entity is null ? false : EntityList.TryRemove(entity.Id, out entity);

    public bool Remove(TKey key) =>
      key is null ? false : Remove(Find(key));

    protected virtual TEntity InternalFind(Expression<Func<TEntity, bool>> predicate)
    {
        return MakeInclusions()?.SingleOrDefault(predicate);
    }

    protected virtual IEnumerable<TEntity> InternalFilter(Expression<Func<TEntity, bool>> predicate)
    {
        var res = MakeInclusions();
        return MakeInclusions().Where(predicate).ToList();
    }

    protected virtual IQueryable<TEntity> MakeInclusions() => EntityList.Values.AsQueryable<TEntity>();

    public virtual TEntity Find(TKey id) => Find(x => x.Id.Equals(id));
}