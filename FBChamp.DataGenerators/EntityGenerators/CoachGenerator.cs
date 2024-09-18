using FBChamp.Core.Entities;
using FBChamp.Core.Entities.Soccer;
using FBChamp.DataGenerators.Helpers;
using FBChamp.DataGenerators.Interfaces;

namespace FBChamp.DataGenerators.EntityGenerators;

public class CoachGenerator : IEntityGenerator
{
    private static int _coachCount;
    private readonly PhotoGenerator _photoGenerator = new();

    public IEnumerable<Entity<Guid>> Generate(Dictionary<string, string>? options)
    {
        if(options is null)
        {
            return new List<Coach> { GenerateCoach() };
        }

        var coaches = new List<Coach>();

        foreach(var option in options)
        {
            switch (option.Key)
            {
                case "Count":
                    if (int.TryParse(option.Value, out var count))
                    {
                        AddCoachesToList(int.Parse(option.Value), coaches);
                    }
                    else
                    {
                        throw new ArgumentException($"Invalid value for 'Count': {option.Value}");
                    }

                    break;

                default:
                    throw new ArgumentException($"Unknown option: {option.Key}");
            }
        }

        return coaches;
    }

    private void AddCoachesToList(int count, List<Coach> coach)
    {
        for (int i = 0; i < count; i++)
        {
            coach.Add(GenerateCoach());
        }
    }

    private Coach GenerateCoach()
    {
        var photo = _photoGenerator.Generate(300, 500);

        var coach = new Coach(
            id: Guid.NewGuid(),
            fullName: $"Coach{_coachCount}",
            birthDate: new DateTime(1999, 12, 12),
            photo: photo);

        _coachCount++;
        return coach;
    }
}
