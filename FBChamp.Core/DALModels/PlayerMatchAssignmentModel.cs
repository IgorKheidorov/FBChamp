using FBChamp.Core.Entities.Soccer;

namespace FBChamp.Core.DALModels;

public class PlayerMatchAssignmentModel : EntityModel
{
    public PlayerMatchAssignment PlayerMatchAssignment { get; }

    public string Role { get; }

    public DateTime Duration => DateTime.MinValue.Add(PlayerMatchAssignment.FinishTime - PlayerMatchAssignment.StartTime);

    public PlayerMatchAssignmentModel()
    {
    }

    public PlayerMatchAssignmentModel(PlayerMatchAssignment playerMatchAssignment)
    {
        ArgumentNullException.ThrowIfNull(playerMatchAssignment);
        Role = playerMatchAssignment.Role;
    }

    public override IEnumerable<(string, string)> GetInformation() => new List<(string, string)>()
    {
        ("Role", Role),
        ("Start Time", PlayerMatchAssignment.StartTime.ToShortDateString()),
        ("Finish Time", PlayerMatchAssignment.FinishTime.ToShortDateString()),
        ("Duration", $"{Duration.Hour}h {Duration.Minute}m")
    };
}
