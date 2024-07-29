using FBChamp.Core.Entities;

public abstract class Entity<TKey> : Entity
{
    public TKey Id { get; set; }

    public override bool Equals(object obj) =>(obj as Entity<TKey>)?.Id.Equals(Id) ?? false;

    public override int GetHashCode() => Id.GetHashCode();
}