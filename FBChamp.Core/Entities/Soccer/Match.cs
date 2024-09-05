using FBChamp.Core.Entities.Soccer.Enums;

namespace FBChamp.Core.Entities.Soccer;

public class Match : Entity<Guid>
{
    public Guid StadiumId { get; set; }

    public Guid LeagueId { get; set; }

    public MatchStatus Status { get; set; }

    public DateTime StartTimeOfMatch { get; set; }

    public Guid HostTeamId { get; set; }

    public Guid GuestTeamId { get; set; }

    public Match()
    {
    }

    public Match(Guid matchId, Guid stadiumId, Guid leagueId, MatchStatus status,
                DateTime startTimeOfMatch, Guid hostTeamId, Guid guestTeamId)
    {
        Id = matchId;
        StadiumId = stadiumId;
        LeagueId = leagueId;
        Status = status;
        StartTimeOfMatch = startTimeOfMatch;
        HostTeamId = hostTeamId;
        GuestTeamId = guestTeamId;
    }
}
