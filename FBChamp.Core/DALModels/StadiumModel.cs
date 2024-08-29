using FBChamp.Core.Entities.Soccer;

namespace FBChamp.Core.DALModels;

public class StadiumModel : EntityModel
{
    public Stadium Stadium { get; }

    public string Location { get; }

    public StadiumModel(Stadium stadium, string location)
    {
        ArgumentNullException.ThrowIfNull(stadium);
        Stadium = stadium;
        Location = location ?? "No location";
    }

    public override IEnumerable<(string, string)> GetInformation() => new List<(string, string)>
    {
        ("Name", Stadium.Name),
        ("Location", Location),
    };
}