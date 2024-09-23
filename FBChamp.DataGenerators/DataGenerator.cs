using FBChamp.Core.Entities.Soccer;
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
        _generators.Add(new LeagueGenerator());
        _generators.Add(new MatchGenerator());
        _generators.Add(new GoalGenerator());
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
    /// Generates goals for matches using the provided options. 
    /// The method first generates a match by calling the <see cref="GenerateMatch"/> method, 
    /// then retrieves the match models. For each match, it assigns a goal to the first player as the author, 
    /// the last player as the assistant, and assigns the scoring team based on the host team.
    /// The generated goal is then committed to the database.
    /// </summary>
    /// <param name="options">
    /// A dictionary of options used to configure the goal generation. Typically includes the count of goals to generate.
    /// </param>
    /// <remarks>
    /// This method expects that matches, players, and player assignments are already generated.
    /// </remarks>

    public void GenerateGoal(Dictionary<string, string>? options)
    {
        var entities = _generators.Single(x => x.GetType() == typeof(GoalGenerator)).Generate(options);

        foreach (var entity in entities)
        {
            var goal = entity as Goal;

            // Note: The GenerateMatch method should be uncommented once the GenerateMatch implementation is added to the Development branch
            // GenerateMatch(new Dictionary<string, string> { { "Count", "1" } });

            var matches = _unitOfWork.GetAllMatchModels();

            foreach (var matchModel in matches)
            {
                var match = matchModel.Match;

                goal!.MatchId = match.Id;
                goal.GoalAuthorId = matchModel.PlayerMatchAssignments.First().PlayerMatchAssignment.Id;
                goal.AssistantIds = new List<Guid> { matchModel.PlayerMatchAssignments.Last().PlayerMatchAssignment.Id };
                goal.ScoringTeamId = matchModel.HostTeam.Team.Id;

                if (goal is not null)
                {
                    _unitOfWork.Commit(goal);
                }
            }
        }
    }

    /// <summary>
    /// Generates leagues based on the provided options, commits them,
    /// and assigns unassigned teams to the newly created leagues.
    /// 
    /// The number of teams assigned is based on the 'NumberOfTeams' property of the league.
    /// Unassigned teams are retrieved and committed, followed by the creation of 
    /// TeamAssignmentInfo entries to link teams to their respective leagues.
    /// 
    /// Note: The team generation logic is currently commented out and should be
    ///       uncommented and integrated when the TeamGenerator is implemented.
    /// </summary>
    /// <param name="options">
    ///    A dictionary of options that determine the number of leagues to generate (e.g., {"Count", "3"}).
    /// </param>

    public void GenerateLeague(Dictionary<string, string>? options)
    {
        var entities = _generators.Single(x => x.GetType() == typeof(LeagueGenerator)).Generate(options);

        foreach(var entity in entities)
        {
            var league = entity as League;

            if(league is not null)
            {
                _unitOfWork.Commit(league);

                // NOTE: Uncomment and integrate when TeamGenerator is implemented
                //   GenerateTeam(new Dictionary<string, string> { { "Count", league.NumberOfTeams.ToString() } });

                var teams = _unitOfWork.GetUnassignedTeamModel();

                foreach (var teamModel in teams)
                {
                    var team = teamModel.Team;

                    _unitOfWork.Commit(team);

                    var teamAssignment = new TeamAssignmentInfo(teamId: teamModel.Team.Id, leagueId: league.Id);
                    _unitOfWork.Commit(teamAssignment);
                }
            }
        }
    }

    /// <summary>
    /// Generates a new match or set of matches based on the provided options. 
    /// The method first generates leagues using the <see cref="GenerateLeague"/> method 
    /// and retrieves the league models. For each league, it assigns the first and last 
    /// teams as the host and guest teams for the match, and commits the generated match. 
    /// 
    /// After the match generation, the method generates a set of players using the <see cref="GeneratePlayer"/> method 
    /// and assigns 12 players to the match, each with a specific role (e.g., Goalkeeper, Defender, Midfielder, Striker). 
    /// Player roles are assigned in a round-robin fashion from a predefined list of roles, 
    /// and the player assignments are then committed.
    /// </summary>
    /// <param name="options">
    /// A dictionary of options to configure the match generation. Typically includes the count of matches to generate.
    /// </param>
    /// <remarks>
    /// This method expects that leagues and teams are generated first, and that there are enough teams in each league 
    /// </remarks>

    public void GenerateMatch(Dictionary<string, string>? options)
    {
        var entities = _generators.Single(x => x.GetType() == typeof(MatchGenerator)).Generate(options);

        foreach(var entity in entities)
        {
            var match = entity as Match;
            var numberOfLeagues = 1;

            GenerateLeague(new Dictionary<string, string> { { "Count", numberOfLeagues.ToString() } });

            var leagues = _unitOfWork.GetAllLeagueModels();

            foreach(var leagueModel  in leagues)
            {
                var league = leagueModel.League;

                match!.LeagueId = league.Id;
                match.HostTeamId = leagueModel.Teams.First().Team.Id;
                match.GuestTeamId = leagueModel.Teams.Last().Team.Id;

                if(match is not null)
                {
                    _unitOfWork.Commit(match);
                }

                GeneratePlayer(new Dictionary<string, string> { { "Count", "11" } });

                var players = _unitOfWork.GetUnassignedPlayerModels().Take(11);

                var roles = new List<string>
                {
                    "Goalkeeper", "Defender", "Defender", "Defender",
                    "Defender", "Midfielder", "Midfielder",
                    "Midfielder", "Striker", "Striker", "Striker"
                };

                int index = 0;

                foreach(var playerModel in players)
                {
                    var player = playerModel.Player;

                    var role = roles[index % roles.Count];

                    var playerAssignment = new PlayerMatchAssignment(
                        playerId: player.Id,
                        matchId: match!.Id,
                        startTime: new DateTime(2024, 09, 19, 16, 30, 00),
                        finishTime: new DateTime(2024, 9, 19, 18, 00, 00),
                        role: role);

                    _unitOfWork.Commit(playerAssignment);

                    index++;
                }
            }
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