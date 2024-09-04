namespace FBChamp.Core.Entities.Soccer;

public class PlayerMatchAssignment : Entity<Guid>
{
    public Guid MatchId { get; set; }

    public DateTime StartTime { get; set; }

    public DateTime FinishTime { get; set; }

    public string Role { get; set; }

    public DateTime TimeOfGoal { get; set; }

    public PlayerMatchAssignment()
    {
    }

    public PlayerMatchAssignment(Guid playerId, Guid matchId, DateTime startTime,
                                DateTime finishTime, string role, DateTime timeOfGoal)
    {
        Id = playerId;
        MatchId = matchId;
        StartTime = startTime;
        FinishTime = finishTime;
        Role = role;
        TimeOfGoal = timeOfGoal;
    }
}
