using FBChamp.Core.Entities;
using FBChamp.Core.Entities.Soccer;
using FBChamp.Core.Entities.Soccer.Enums;
using FBChamp.DataGenerators.Interfaces;

namespace FBChamp.DataGenerators.EntityGenerators;

public class MatchGenerator : IEntityGenerator
{
    private static int _matchCount;

    public IEnumerable<Entity<Guid>> Generate(Dictionary<string, string>? options)
    {
        if(options is null)
        {
            return new List<Match> { GenerateMatch() };
        }

        var matches = new List<Match>();

        foreach (var option in options)
        {
            switch (option.Key)
            {
                case "Count":
                    if (int.TryParse(option.Value, out var count))
                    {
                        AddMatchesToList(int.Parse(option.Value), matches);
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

        return matches;
    }

    private void AddMatchesToList(int count, List<Match> matches)
    {
        for (int i = 0; i < count; i++)
        {
            matches.Add(GenerateMatch());
        }
    }

    private Match GenerateMatch()
    {
        var match = new Match(
            matchId: Guid.NewGuid(),
            stadiumId: Guid.NewGuid(),
            leagueId: Guid.NewGuid(),
            status: MatchStatus.InProgress,
            startTimeOfMatch: new DateTime(2024, 09, 19, 15, 30, 00),
            finishTimeOfMatch:new DateTime(2024, 09, 19, 17, 00, 00),
            hostTeamId: Guid.NewGuid(),
            guestTeamId: Guid.NewGuid());

        _matchCount++;
        return match;
    }
}
