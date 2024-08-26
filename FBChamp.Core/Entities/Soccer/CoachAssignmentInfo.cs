namespace FBChamp.Core.Entities.Soccer;

public class CoachAssignmentInfo : Entity<Guid>
{
    public Guid TeamId { get; set; }

    public string Role { get; set; }

    public CoachAssignmentInfo()
    {
    }

    public CoachAssignmentInfo(Guid coachId, Guid teamId, string role)
    {
        Id = coachId;
        TeamId = teamId;
        Role = role;
    }
}
