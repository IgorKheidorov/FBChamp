namespace FBChamp.Core.Entities.Socker;

public class Coach:Person
{
    public Coach(Guid id, string fullName, DateTime birthDate, byte[] photo) :
        base(id, fullName, birthDate, photo){ }
}
