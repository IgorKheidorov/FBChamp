using FBChamp.Core.Entities.Soccer;

namespace FBChamp.Core.DALModels;

public class PlayerMatchAssignmentModel : EntityModel
{
    public PlayerMatchAssignment PlayerMatchAssignment { get; }

    public string PlayerName { get; }

    public string Role { get; }

    public string Duration { get; }

    public override string FullName => PlayerName;

    public PlayerMatchAssignmentModel()
    {
    }

    public PlayerMatchAssignmentModel(PlayerMatchAssignment playerMatchAssignment, string playerName)
    {
        ArgumentNullException.ThrowIfNull(playerMatchAssignment);
        PlayerName = playerName ?? "Unknown Player";
        Role = playerMatchAssignment.Role ?? "Unknown Role";
        Duration = CalculateDuration(playerMatchAssignment.StartTime, playerMatchAssignment.FinishTime);
    }

    private string CalculateDuration(DateTime startTime, DateTime finishTime)
    {
        TimeSpan duration = finishTime - startTime;
        return $"{duration.Hours}h {duration.Minutes}m";
    }

    public override IEnumerable<(string, string)> GetInformation() => new List<(string, string)>()
    {
        ("Player Name",PlayerName),
        ("Role", Role),
        ("Start Time", PlayerMatchAssignment.StartTime.ToShortDateString()),
        ("Finish Time", PlayerMatchAssignment.FinishTime.ToShortDateString()),
        ("Time of Goal", PlayerMatchAssignment.TimeOfGoal.ToShortDateString()),
        ("Duration", Duration)
    };
}
