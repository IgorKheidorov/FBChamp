namespace FBChamp.Core.Entities.Soccer;

public class MatchStatistics : Entity<Guid>
{
    public Guid MatchId { get; set; }
    public int NumberOfWatchers { get; set; }

    public MatchStatistics()
    {
    }

    public MatchStatistics(Guid matchStatisticId, Guid matchId, int numberOfWatchers)
    {
        Id = matchStatisticId;
        MatchId = matchId;
        NumberOfWatchers = numberOfWatchers;
    }
}
