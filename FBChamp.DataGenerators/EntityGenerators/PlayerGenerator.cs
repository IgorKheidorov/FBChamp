using FBChamp.Core.Entities;
using FBChamp.Core.Entities.Soccer;
using FBChamp.Core.UnitOfWork;
using FBChamp.DataGenerators.Helpers;
using FBChamp.DataGenerators.Interfaces;

namespace FBChamp.DataGenerators.EntityGenerators;

internal class PlayerGenerator(IUnitOfWork unitOfWork) : IEntityGenerator
{
    private static int _playerCount;

    private readonly PhotoGenerator _photoGenerator = new();

    /// <summary>
    ///     Generates a list of <see cref="Player" /> entities based on the provided options.
    /// </summary>
    /// <remarks>
    ///     If no options are provided, a single <see cref="Player" /> entity will be generated with default values.
    ///     Options can be used to specify the number of players or other customizable parameters.
    /// </remarks>
    /// <param name="options">
    ///     Dictionary containing key-value pairs for generation options.
    ///     For example, "Count" can be used to specify the number of players to generate.
    /// </param>
    /// <returns>A collection of generated <see cref="Player" /> entities.</returns>
    public IEnumerable<Entity<Guid>> Generate(Dictionary<string, string>? options)
    {
        if (options is null)
        {
            return new List<Player> { GeneratePlayer() };
        }

        var players = new List<Player>();

        foreach (var option in options)
        {
            switch (option.Key)
            {
                case "Count":
                    if (int.TryParse(option.Value, out var count))
                    {
                        AddPlayersToList(int.Parse(option.Value), players);
                    }
                    else
                    {
                        throw new ArgumentException($"Invalid value for 'Count': {option.Value}");
                    }

                    break;

                // Other options for generation can be handled here

                default:
                    throw new ArgumentException($"Unknown option: {option.Key}");
            }
        }

        return players;
    }

    /// <summary>
    ///     Generates a single <see cref="Player" /> entity with randomly generated attributes.
    /// </summary>
    /// <remarks>
    ///     Ensures the uniqueness of player names by appending the current player count
    ///     to the name. This helps avoid validation issues caused by duplicate names.
    /// </remarks>
    /// <returns>A newly created <see cref="Player" /> entity with random values.</returns>
    private Player GeneratePlayer()
    {
        var positionId = unitOfWork.GetAllPlayerPositions().First().Id;
        var photo = _photoGenerator.Generate(300, 500);
        var player = new Player(Guid.NewGuid(),
            $"Player{_playerCount}",
            new DateTime(2000, 1, 1),
            180,
            positionId,
            photo);

        _playerCount++;

        return player;
    }

    private void AddPlayersToList(int count, List<Player> players)
    {
        for (var i = 0; i < count; i++)
        {
            players.Add(GeneratePlayer());
        }
    }
}