namespace FBChamp.Core.Entities.Soccer;

public class PlayerAssignmentInfo : Entity<Guid>
{
    public Guid TeamId { get; set; }

    public uint PlayingNumber { get; set; }

    public PlayerAssignmentInfo(Guid playerId, Guid teamId, uint playingNumber)
    {
        Id = playerId;
        TeamId = teamId;
        PlayingNumber = playingNumber;
    }
}