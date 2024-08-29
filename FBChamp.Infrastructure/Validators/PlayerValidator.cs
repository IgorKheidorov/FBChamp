using FBChamp.Core.DataValidator;
using FBChamp.Core.Entities;
using FBChamp.Core.Entities.Soccer;
using FBChamp.Core.UnitOfWork;
using FBChamp.Infrastructure.BusinessRules;

namespace FBChamp.Infrastructure.Validators;

public class PlayerValidator(IUnitOfWork unitOfWork) : IValidateEntity
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

        return age is >= DataRestrictions.MinPlayerAge and <= DataRestrictions.MaxPlayerAge;
    }

    private bool ValidateHeight(float height) =>
        height is > DataRestrictions.MinPlayerHeight and < DataRestrictions.MaxPlayerHeight;

    private bool ValidateDescription(string description) =>
        description.Length < DataRestrictions.MaxDescriptionLength;

    private bool ValidatePosition(Guid positionId) =>
        unitOfWork.Exists(positionId, typeof(PlayerPosition));

    private bool ValidatePhoto(byte[] photo)
    {
        var photoValidator = new PhotoValidator();

        return photoValidator.Validate(photo,
            DataRestrictions.PlayerPhotoWidth,
            DataRestrictions.PlayerPhotoHeight);
    }
}