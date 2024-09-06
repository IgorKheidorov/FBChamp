using FBChamp.Core.Entities.Soccer;

namespace FBChamp.Core.DALModels;

public class MatchModel : EntityModel
{
    public Match Match { get; }

    public TeamModel HostTeam { get; }

    public TeamModel GuestTeam { get; }

    public StadiumModel Stadium { get; }

    public IEnumerable<PlayerMatchAssignmentModel> PlayerMatchAssignments { get; }

    public MatchStatisticsModel MatchStatistics { get; }

    public MatchModel(Match match, TeamModel hostTeam, TeamModel guestTeam, StadiumModel stadium,
                      IEnumerable<PlayerMatchAssignmentModel> playerMatchAssignments, MatchStatisticsModel matchStatistics)
    {
        Match = match;
        HostTeam = hostTeam;
        GuestTeam = guestTeam;
        Stadium = stadium;
        PlayerMatchAssignments = playerMatchAssignments;
        MatchStatistics = matchStatistics;
    }

    public override IEnumerable<(string, string)> GetInformation() => new List<(string, string)>()
    {
        ("Match Start Time", Match.StartTimeOfMatch.ToShortDateString()),
        ("Host Team", HostTeam.FullName),
        ("Guest Team", GuestTeam.FullName),
        ("Stadium", Stadium.Stadium.Name),
        ("Watchers", MatchStatistics.MatchStatistics.NumberOfWatchers.ToString()),
    };
}
