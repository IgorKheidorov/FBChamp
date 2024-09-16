using FBChamp.Core.Entities.Soccer;

namespace FBChamp.Core.DALModels;

public class CoachModel: EntityModel
{
    public Coach Coach { get; }

    public string CurrentTeam { get; }

    public string PhotoString { get; }

    public string Role { get; }

    public override string FullName => Coach.FullName;

    public CoachModel(Coach coach, string currentTeam, string role)
    {
        Coach = coach;
        Role = role ?? "No role";
        CurrentTeam = currentTeam ?? "No assignment";
        PhotoString = coach.Photo is null ? string.Empty : Convert.ToBase64String(coach.Photo);
    }

    public override IEnumerable<(string, string)> GetInformation() => new List<(string, string)>()
    {
        ("BirthDate", Coach.BirthDate.ToShortDateString()),
        ("Club", CurrentTeam),
        ("Role", Role),
        ("Description", Coach.Description)
    };

    public override string GetPhoto() => PhotoString;
}
