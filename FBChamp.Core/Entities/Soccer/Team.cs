namespace FBChamp.Core.Entities.Soccer;

public class Team : Entity<Guid>
{
    public string Name { get; set; }

    public byte[] Photo { get; set; }

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
}
