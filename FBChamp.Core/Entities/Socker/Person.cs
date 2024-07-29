namespace FBChamp.Core.Entities.Socker;

public abstract class Person: Entity<Guid>
{
    public const int FullNameLength = 128;
    
    public string FullName { get; set; }
    public byte[] Photo { get; set; }
    public DateTime BirthDate { get; set; }
    public string Description { get; set; }

    public Person(Guid id, string fullName, DateTime birthDate, byte[] photo, string description = null) 
    {
        Id = id;
        FullName = fullName;
        Photo = photo?? new byte[0];
        BirthDate = birthDate.Date;
        Description = description is not null ? description : "No information";
    }
}
