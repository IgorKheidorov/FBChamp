namespace FBChamp.Core.Entities.Soccer;

public class TeamAssignmentInfo : Entity<Guid>
{
    public Guid LeagueId { get; set; }

    public TeamAssignmentInfo()
    {
    }

    public TeamAssignmentInfo(Guid teamId, Guid leagueId)
    {
        Id = teamId;
        LeagueId = leagueId;
    }
}
