using FBChamp.Core.Entities;

namespace FBChamp.Web.Models;

public class BaseModel
{
    public Guid Id { get; init; }

    public string Name { get; init; }

    public int VisualOrder { get; init; }

    public string Description { get; init; }

    protected BaseModel()
    {
    }

    public BaseModel(VisibleEntity entity)
    {
        ArgumentNullException.ThrowIfNull(entity);

        Id = entity.Id;
        Name = entity.Name;
        VisualOrder = entity.VisualOrder;
        Description = entity.Description;
    }
}