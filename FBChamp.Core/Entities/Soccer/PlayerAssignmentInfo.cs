namespace FBChamp.Core.Entities.Soccer;

public class PlayerAssignmentInfo : Entity<Guid>
{
    public Guid TeamId { get; set; }

    public uint PlayingNumber { get; set; }

    public PlayerAssignmentInfo(Guid id, Guid teamId, uint playingNumber)
    {
        Id = id;
        TeamId = teamId;
        PlayingNumber = playingNumber;
    }
}