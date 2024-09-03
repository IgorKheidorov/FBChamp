namespace FBChamp.Core.Entities.Soccer;

public class Goal : Entity<Guid>
{
    public Guid MatchId { get; set; }

    public Guid GoalAuthorId { get; set; }

    public List<Guid> AssistantIds { get; set; }

    public DateTime Time { get; set; }

    public Goal()
    {
    }

    public Goal(Guid matchId, Guid goalAuthorId, List<Guid> assistantIds, DateTime time)
    {
        MatchId = matchId;
        GoalAuthorId = goalAuthorId;
        AssistantIds = assistantIds;
        Time = time;
    }
}