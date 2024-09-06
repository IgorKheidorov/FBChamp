using FBChamp.Core.DataValidator;
using FBChamp.Core.Entities;
using FBChamp.Core.Entities.Soccer;
using FBChamp.Core.UnitOfWork;
using FBChamp.Infrastructure.BusinessRules;

namespace FBChamp.Infrastructure.Validators;

public class CoachValidator : IValidateEntity
{
    public Type GetValidatedType() => typeof(Coach);

    public CRUDResult Validate(Entity entity) =>
        entity switch
        {
            Coach coach when ValidateProperties(coach) => CRUDResult.Success,
            Coach _ => CRUDResult.EntityValidationFailed,
            _ => CRUDResult.InvalidOperation
        };

    private bool ValidateProperties(Coach coach) =>
        ValidateNameLength(coach.FullName) &&
        ValidateAge(coach.BirthDate) &&
        (coach.Photo is null || ValidatePhoto(coach.Photo)) &&
        (coach.Description is null || ValidateDescription(coach.Description));

    private bool ValidateNameLength(string name) =>
        name is not null && name.Length < DataRestrictions.NameLengthMax;

    private bool ValidateAge(DateTime birthDate)
    {
        var age = DateTime.Now.Year - birthDate.Year;

        if (birthDate > DateTime.Now.AddYears(-age))
        {
            age--;
        }

        return age is >= DataRestrictions.MinCoachAge and <= DataRestrictions.MaxCoachAge;
    }

    private bool ValidatePhoto(byte[] photo)
    {
        var photoValidator = new PhotoValidator();

        return photoValidator.Validate(photo,
            DataRestrictions.PersonPhotoWidth,
            DataRestrictions.PersonPhotoHeight);
    }

    private bool ValidateDescription(string description) =>
        description.Length < DataRestrictions.MaxDescriptionLength;
}