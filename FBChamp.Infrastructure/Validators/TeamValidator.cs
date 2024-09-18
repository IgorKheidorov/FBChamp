using FBChamp.Core.DataValidator;
using FBChamp.Core.Entities;
using FBChamp.Core.Entities.Soccer;
using FBChamp.Core.UnitOfWork;
using FBChamp.Infrastructure.BusinessRules;

namespace FBChamp.Infrastructure.Validators;

internal class TeamValidator : IValidateEntity
{
    public Type GetValidatedType() => typeof(Team);

    public CRUDResult Validate(Entity entity) =>
        entity switch
        {
            Team team when ValidateProperties(team) => CRUDResult.Success,
            Team _ => CRUDResult.EntityValidationFailed,
            _ => CRUDResult.InvalidOperation
        };


    private bool ValidateProperties(Team team) =>
        ValidateNameLength(team.Name) &&
        ValidatePhoto(team.Photo) &&
        ValidateDescriptionLength(team.Description) &&
        ValidateLocation(team.LocationId);

    private bool ValidateNameLength(string name) =>
    name.Length < DataRestrictions.NameLengthMax;

    private bool ValidateDescriptionLength(string description) =>
        description.Length < DataRestrictions.MaxDescriptionLength;

    private bool ValidatePhoto(byte[] photo)
    {
        var photoValidator = new PhotoValidator();

        return photoValidator.Validate(photo,
            DataRestrictions.PersonPhotoWidth,
            DataRestrictions.PersonPhotoHeight);
    }

    private bool ValidateLocation(Guid teamId) =>
        teamId != Guid.Empty;
}