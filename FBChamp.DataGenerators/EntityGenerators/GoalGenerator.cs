using FBChamp.Core.Entities.Soccer.Enums;
using FBChamp.Core.Entities.Soccer;
using FBChamp.Core.Entities;
using FBChamp.DataGenerators.Interfaces;

namespace FBChamp.DataGenerators.EntityGenerators;

public class GoalGenerator : IEntityGenerator
{
    private static int _goalCount;

    public IEnumerable<Entity<Guid>> Generate(Dictionary<string, string>? options)
    {
        if (options is null)
        {
            return new List<Goal> { { GenerateGoal() } };
        }

        var goals = new List<Goal>();

        foreach (var option in options)
        {
            switch (option.Key)
            {
                case "Count":
                    if (int.TryParse(option.Value, out var count))
                    {
                        AddGoalToLists(int.Parse(option.Value), goals);
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

        return goals;
    }

    private void AddGoalToLists(int count, List<Goal> goals)
    {
        for (int i = 0; i < count; i++)
        {
            goals.Add(GenerateGoal());
        }
    }

    private Goal GenerateGoal()
    {
        var goal = new Goal(
            id: Guid.NewGuid(),
            matchId: Guid.NewGuid(),
            goalAuthorId: Guid.NewGuid(),
            assistantIds: new List<Guid>(),
            type: GoalType.Normal,
            scoringTeamId: Guid.NewGuid(),
            time: new DateTime(2024,09,19,17,00,00));

        _goalCount++;
        return goal;
    }
}
