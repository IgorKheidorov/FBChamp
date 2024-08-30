using FBChamp.Core.Entities.Soccer;

namespace FBChamp.Core.DALModels;

public class StadiumModel : EntityModel
{
    public Stadium Stadium { get; }

    public string Description { get; }

    public StadiumModel(Stadium stadium, string description = null)
    {
        ArgumentNullException.ThrowIfNull(stadium);
        Stadium = stadium;
        Description = description;
    }

    public override IEnumerable<(string, string)> GetInformation() => new List<(string, string)>
    {
        ("Name", Stadium.Name),
        ("Description", Description),
    };
}