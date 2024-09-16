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
            return new List<Player> { GenerateEntity() };
        }

        var players = new List<Player>();

        foreach (var option in options)
        {
            if (string.Equals(option.Key, "Count"))
            {
                for (var i = 0; i < int.Parse(option.Value); i++)
                {
                    players.Add(GenerateEntity());
                    _playerCount++;
                }
            }

            // Other options for generation can be handled here
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
    private Player GenerateEntity()
    {
        var positionId = unitOfWork.GetAllPlayerPositions().First().Id;
        var photo = _photoGenerator.Generate(300, 500);

        return new Player(Guid.NewGuid(),
            $"Player{_playerCount}",
            new DateTime(2000, 1, 1),
            180,
            positionId,
            photo);
    }
}