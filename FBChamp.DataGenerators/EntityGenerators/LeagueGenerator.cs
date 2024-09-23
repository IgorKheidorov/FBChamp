using FBChamp.Core.Entities;
using FBChamp.Core.Entities.Soccer;
using FBChamp.DataGenerators.Helpers;
using FBChamp.DataGenerators.Interfaces;

namespace FBChamp.DataGenerators.EntityGenerators;

public class LeagueGenerator : IEntityGenerator
{
    private static int _leagueCount;

    private readonly PhotoGenerator _photoGenerator = new();

    public IEnumerable<Entity<Guid>> Generate(Dictionary<string, string>? options)
    {
        if(options is null)
        {
            return new List<League> { GenerateLeague() };
        }

        var leagues = new List<League>();

        foreach(var option in options)
        {
            switch(option.Key)
            {
                case "Count":
                    if(int.TryParse(option.Value, out var count))
                    {
                        AddLeaguesToLists(int.Parse(option.Value), leagues);
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

        return leagues;
    }

    private void AddLeaguesToLists(int count, List<League> leagues)
    {
        for (int i = 0; i < count; i++)
        {
            leagues.Add(GenerateLeague());
        }
    }

    private League GenerateLeague()
    {
        var photo = _photoGenerator.Generate(300, 500);

        var league = new League(
            id: Guid.NewGuid(),
            fullName: $"League{_leagueCount}",
            photo: photo,
            numberOfTeams: 3,
            seasonStartDate: new DateTime(2024, 07, 25),
            seasonFinishDate: new DateTime(2025, 05, 15));

        _leagueCount++;

        return league;
    }
}
