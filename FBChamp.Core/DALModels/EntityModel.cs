namespace FBChamp.Core.DALModels;

public abstract class EntityModel
{
    public virtual string FullName { get; } = String.Empty;  
    public virtual IEnumerable<(string, string)> GetInformation() => new List<(string, string)>();
    public virtual string GetPhoto() => String.Empty;
}
