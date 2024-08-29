using System.ComponentModel.DataAnnotations;

namespace FBChamp.Core.Entities.Soccer;

public class League : Entity<Guid>
{
    [Required]
    public string FullName { get; set; }

    public byte[] Photo {  get; set; }

    [Required]
    public int NumberOfTeams { get; set; }

    [Required]
    public DateTime SeasonStartDate { get; set; }

    [Required]
    public DateTime SeasonFinishDate { get; set; }

    public string Description { get; set; }

    public League()
    {
    }

    public League(Guid id,string fullName, byte[] photo,int numberOfTeams,DateTime seasonStartDate,DateTime seasonFinishDate,string description = null)
    {
        Id = id;
        FullName = fullName;
        Photo = photo;
        NumberOfTeams = numberOfTeams;
        SeasonStartDate = seasonStartDate;
        SeasonFinishDate = seasonFinishDate;
        Description = description is not null ? description : "No information";
    }
}
