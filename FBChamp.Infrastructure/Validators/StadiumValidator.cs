using FBChamp.Core.DataValidator;
using FBChamp.Core.Entities;
using FBChamp.Core.Entities.Soccer;
using FBChamp.Core.UnitOfWork;

namespace FBChamp.Infrastructure.Validators;

internal class StadiumValidator : IValidateEntity
{
    private IUnitOfWork _unitOfWork;

    public StadiumValidator(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public Type GetValidatedType() => typeof(Stadium);

    public CRUDResult Validate(Entity entity) =>
        entity switch
        {
            Stadium stadium 
            when ValidateLocation(stadium.LocationId) => CRUDResult.Success,
            Stadium _ => CRUDResult.EntityValidationFailed,
            _ => CRUDResult.InvalidOperation,
        };

    private bool ValidateLocation(Guid locationId) =>
        _unitOfWork.Exists(locationId, typeof(Location));
}