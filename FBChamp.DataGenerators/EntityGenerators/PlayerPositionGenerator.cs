using FBChamp.Core.Entities;
using FBChamp.Core.Entities.Soccer;
using FBChamp.DataGenerators.Interfaces;

namespace FBChamp.DataGenerators.EntityGenerators;

internal class PlayerPositionGenerator : IEntityGenerator
{
    private static int _playerPositionCount;

    public IEnumerable<Entity<Guid>> Generate(Dictionary<string, string>? options)
    {
        if (options is null)
        {
            return new List<PlayerPosition> { GenerateEntity() };
        }

        var players = new List<PlayerPosition>();

        foreach (var option in options)
        {
            if (string.Equals(option.Key, "Count"))
            {
                for (var i = 0; i < int.Parse(option.Value); i++)
                {
                    players.Add(GenerateEntity());

                    _playerPositionCount++;
                }
            }

            // Other options for generation can be handled here
        }

        return players;
    }

    private PlayerPosition GenerateEntity() =>
        new($"Position{_playerPositionCount}", _playerPositionCount, "Description");
}