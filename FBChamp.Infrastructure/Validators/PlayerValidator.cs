using FBChamp.Core.DataValidator;
using FBChamp.Core.Entities;
using FBChamp.Core.Entities.Soccer;
using FBChamp.Core.UnitOfWork;
using FBChamp.Infrastructure.BusinessRules;
using FBChamp.Infrastructure.Repositories.Membership;
using SixLabors.ImageSharp;

namespace FBChamp.Infrastructure.Validators;

public class PlayerValidator : IValidateEntity
{
    public Type GetValidatedType() => typeof(Player);

    public CRUDResult Validate(Entity entity) =>
        entity switch
        {
            Player player when ValidateProperties(player) => CRUDResult.Success,
            Player _ => CRUDResult.EntityValidationFailed,
            _ => CRUDResult.InvalidOperation
        };

    private bool ValidateProperties(Player player) =>
        ValidateNameLength(player.FullName) &&
        ValidateAge(player.BirthDate) &&
        ValidateHeight(player.Height) &&
        ValidateDescription(player.Description) &&
        ValidatePosition(player.PositionId) &&
        ValidatePhoto(player.Photo);

    private bool ValidateNameLength(string name) =>
        name.Length < DataRestrictions.NameLengthMax;

    private bool ValidateAge(DateTime birthDate)
    {
        var age = DateTime.Now.Year - birthDate.Year;

        // Adjusts age if birthday not yet occurred this year 
        if (birthDate > DateTime.Now.AddYears(-age))
        {
            age--;
        }

        return age is >= 16 and <= 50;
    }

    private bool ValidateHeight(float height) =>
        height is > 150f and < 210f;

    private bool ValidateDescription(string description) =>
        description.Length < 256;

    private bool ValidatePosition(Guid positionId)
    {
        if (positionId == Guid.Empty)
        {
            return false;
        }

        var positionRepository = new PlayerPositionsRepository();

        var position = positionRepository.Find(positionId);

        return position is not null;
    }

    private bool ValidatePhoto(byte[] photo)
    {
        if (photo is null || photo.Length == 0)
        {
            return false;
        }

        try
        {
            using var image = Image.Load(photo);

            return image.Width == 300 && image.Height == 500;
        }
        catch (Exception)
        {
            return false;
        }
    }
}