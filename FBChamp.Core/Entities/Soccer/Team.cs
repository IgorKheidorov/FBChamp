namespace FBChamp.Core.Entities.Soccer;

public class Team : Entity<Guid>
{
    public string Name { get; set; }

    public Guid LocationId { get; set; }

    public byte[] Photo { get; set; }

    public Guid StadiumId { get; set; }

    public string Description { get; set; }

    public Team()
    {
    }

    public Team(Guid id, string name, byte[] photo)
    {
        Id = id;
        Name = name;
        Photo = photo;
    }

    public Team(Guid id, string name, Guid locationId, byte[] photo, Guid stadiumId, string description = null)
    {
        Id = id;
        Name = name;
        LocationId = locationId;
        Photo = photo ?? Array.Empty<byte>();
        StadiumId = stadiumId;
        Description = description ?? "No description";
    }
}
