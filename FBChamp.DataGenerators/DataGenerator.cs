﻿using FBChamp.Core.Entities.Soccer;
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
        _generators.Add(new TeamGenerator(_unitOfWork));
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

    public void GenerateTeam(Dictionary<string, string>? options)
    {
        var entities = _generators.Single(x => x.GetType() == typeof(TeamGenerator)).Generate(options);

        foreach (var entity in entities)
        {
            _unitOfWork.Commit(entity);

            var team = entity as Team;

            GenerateCoach(null);

            var coaches = _unitOfWork.GetAllCoachModels();

            foreach (var coachModel in coaches)
            {
                var coach = coachModel.Coach;
                _unitOfWork.Commit(coach);

                var coachAssignment = new CoachAssignmentInfo(coach.Id, team!.Id, "Role");
                _unitOfWork.Commit(coachAssignment);
            }

            GeneratePlayer(new Dictionary<string, string> { { "Count", "11" } });

            var players = _unitOfWork.GetUnassignedPlayerModels();
            uint playingNumber = 1;

            foreach (var playerModel in players)
            {
                var player = playerModel.Player;

                _unitOfWork.Commit(player);

                var playerAssignment = new PlayerAssignmentInfo(player.Id, team!.Id, playingNumber);
                playingNumber++;
                _unitOfWork.Commit(playerAssignment);
            }
        }
    }
}