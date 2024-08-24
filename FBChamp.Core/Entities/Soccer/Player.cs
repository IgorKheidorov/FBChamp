namespace FBChamp.Core.Entities.Soccer;

public class Player(Guid id, string fullName, DateTime birthDate, float? height, Guid positionId, byte[] photo, string description = null) 
    : Person(id, fullName, birthDate, photo, description)
{
    public Guid PositionId { get; set; } = positionId;

    public float? Height { get; set; } = height;
}
