namespace FBChamp.Core.Entities.Socker;

public class PlayerAssignmentInfo : Entity<Guid> // guid is PlayerId
{
    public Guid TeamId { get; set; }
    public uint PlayingNumber { get; set; }

    public PlayerAssignmentInfo()
    {
    }

    public PlayerAssignmentInfo(Guid playerId, Guid teamId, uint playingNumber)
    {
        Id = playerId;
        TeamId = teamId;
        PlayingNumber = playingNumber;
    }
}
