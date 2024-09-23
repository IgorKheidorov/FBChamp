using FBChamp.Core.Entities;
using FBChamp.Core.Entities.Soccer;
using FBChamp.Core.UnitOfWork;
using FBChamp.DataGenerators.Helpers;
using FBChamp.DataGenerators.Interfaces;

namespace FBChamp.DataGenerators.EntityGenerators;

internal class TeamGenerator(IUnitOfWork unitOfWork) : IEntityGenerator
{
    private static int _teamCount;

    private readonly PhotoGenerator _photoGenerator = new();

    public IEnumerable<Entity<Guid>> Generate(Dictionary<string, string>? options)
    {
        if (options is null)
        {
            return new List<Team> { GenerateTeam() };
        }

        var teams = new List<Team>();

        foreach (var option in options)
        {
            switch (option.Key)
            {
                case "Count":
                    if (int.TryParse(option.Value, out var count))
                    {
                        AddTeamToList(int.Parse(option.Value), teams);
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

        return teams;
    }

    private Team GenerateTeam()
    {
        var locationId = Guid.NewGuid();
        var stadiumId = Guid.NewGuid();
        var photo = _photoGenerator.Generate(300, 500);

        var team = new Team(Guid.NewGuid(),
            $"Team {_teamCount}",
            locationId,
            photo,
            stadiumId
            );

        _teamCount++;

        return team;
    }

    private void AddTeamToList(int count, List<Team> teams)
    {
        for (int i = 0; i < count; i++)
        {
            teams.Add(GenerateTeam());
        }
    }
}