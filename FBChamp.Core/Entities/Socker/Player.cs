
using System.Runtime.InteropServices;

namespace FBChamp.Core.Entities.Socker;

public class Player: Person
{
    public Guid PositionId { get; set; }
    public float? Height { get; set; }

    public Player(Guid id, string fullName, DateTime birthDate, float? height, Guid positionId, byte[] photo, string description = null) :
        base(id, fullName, birthDate, photo, description)
    {
        PositionId = positionId;
        Height = height;
    }
}
