namespace FBChamp.Core.Entities.Soccer;

public class Stadium : Entity<Guid>
{
    public string Name { get; set; }

    public Guid LocationId { get; set; }

    public Stadium()
    {
    }

    public Stadium(Guid id, string name, Guid locationId)
    {
        Id = id;
        Name = name;
        LocationId = locationId;
    }
}
