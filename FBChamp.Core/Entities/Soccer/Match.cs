using FBChamp.Core.Entities.Soccer.Enums;

namespace FBChamp.Core.Entities.Soccer;

public class Match : Entity<Guid>
{
    public Guid StadiumId { get; set; }

    public Guid LeagueId { get; set; }

    public MatchStatus Status { get; set; }

    public DateTime StartTimeOfMatch { get; set; }
    
    public DateTime FinishTimeOfMatch { get; set; }

    public Guid HostTeamId { get; set; }

    public Guid GuestTeamId { get; set; }

    public Match()
    {
    }

    public Match(Guid matchId, Guid stadiumId, Guid leagueId, MatchStatus status, 
        Guid hostTeamId, Guid guestTeamId, DateTime startTimeOfMatch = default, DateTime finishTimeOfMatch = default)
    {
        Id = matchId;
        StadiumId = stadiumId;
        LeagueId = leagueId;
        Status = status;
        StartTimeOfMatch = startTimeOfMatch == default ? DateTime.Now : startTimeOfMatch;
        FinishTimeOfMatch = finishTimeOfMatch;
        HostTeamId = hostTeamId;
        GuestTeamId = guestTeamId;
    }
}
