using FBChamp.Core.UnitOfWork;
using FBChamp.DataGenerators.EntityGenerators;
using FBChamp.DataGenerators.Interfaces;

namespace FBChamp.DataGenerators;

public class DataGenerator : IDataGenerator
{
    private readonly HashSet<IEntityGenerator> _generators = [];
    private readonly IUnitOfWork _unitOfWork;

    public DataGenerator(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;

        // Add entity generators to the HashSet
        _generators.Add(new PlayerGenerator(_unitOfWork));
        _generators.Add(new CoachGenerator());
    }

    public void GenerateCoach(Dictionary<string, string>? options)
    {
        var entities = _generators.Single(x => x.GetType() == typeof(CoachGenerator)).Generate(options);

        foreach (var entity in entities)
        {
            _unitOfWork.Commit(entity);
        }
    }

    /// <summary>
    ///     Generates players and their positions.
    /// </summary>
    /// <remarks>
    ///     Generates player positions first within this method, ensuring that positions exist before players are created.
    ///     This avoids the need to generate positions during integration tests.
    /// </remarks>
    /// <param name="options">
    ///     Dictionary with options (e.g., "Count" to specify the number of players).
    /// </param>
    public void GeneratePlayer(Dictionary<string, string>? options)
    {
        // GeneratePlayerPosition(null);

        var entities = _generators.Single(x => x.GetType() == typeof(PlayerGenerator)).Generate(options);

        foreach (var entity in entities)
        {
            _unitOfWork.Commit(entity);
        }
    }
}