namespace FBChamp.Core.Entities.Socker;

public class Team : Entity<Guid>
{
    public string Name { get; set; }
    public byte[] Photo { get; set; }
    public string Description { get; set; }

    public Team() { }

    public Team(Guid guid, string name, byte[] photo)
    {
        Id = guid;
        Name = name;
        Photo = photo;
    }    
}
