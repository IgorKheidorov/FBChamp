namespace FBChamp.Core.Entities.Soccer;

public class Coach(
    Guid id,
    string fullName,
    DateTime birthDate,
    byte[] photo,
    string description = null)
    : Person(id, fullName, birthDate, photo, description)
{
}