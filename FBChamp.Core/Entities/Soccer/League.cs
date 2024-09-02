using System.ComponentModel.DataAnnotations;

namespace FBChamp.Core.Entities.Soccer;

public class League : Entity<Guid>
{
    public string FullName { get; set; }

    public byte[] Photo {  get; set; }

    public int NumberOfTeams { get; set; }

    public DateTime SeasonStartDate { get; set; }

    public DateTime SeasonFinishDate { get; set; }

    public string Description { get; set; }

    public League()
    {
    }

    public League(Guid id,string fullName, byte[] photo,int numberOfTeams,DateTime seasonStartDate,DateTime seasonFinishDate,string description = null)
    {
        Id = id;
        FullName = fullName;
        Photo = photo ?? Array.Empty<byte>();
        NumberOfTeams = numberOfTeams;
        SeasonStartDate = seasonStartDate;
        SeasonFinishDate = seasonFinishDate;
        Description = description is not null ? description : "No information";
    }
}
