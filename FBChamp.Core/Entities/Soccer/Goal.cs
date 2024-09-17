using FBChamp.Core.Entities.Soccer.Enums;

namespace FBChamp.Core.Entities.Soccer;

public class Goal : Entity<Guid>
{
    public Guid MatchId { get; set; }

    public Guid GoalAuthorId { get; set; }

    public List<Guid> AssistantIds { get; set; }

    public GoalType Type { get; set; }

    public Guid ScoringTeamId { get; set; }

    public DateTime Time { get; set; }

    public Goal(Guid id, Guid matchId, Guid goalAuthorId, List<Guid> assistantIds, GoalType type, Guid scoringTeamId,
        DateTime time)
    {
        Id = id;
        MatchId = matchId;
        GoalAuthorId = goalAuthorId;
        AssistantIds = assistantIds;
        Type = type;
        ScoringTeamId = scoringTeamId;
        Time = time;
    }
}