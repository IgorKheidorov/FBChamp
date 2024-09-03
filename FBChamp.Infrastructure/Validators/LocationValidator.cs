using FBChamp.Core.DataValidator;
using FBChamp.Core.Entities;
using FBChamp.Core.Entities.Soccer;
using FBChamp.Core.UnitOfWork;

namespace FBChamp.Infrastructure.Validators
{
    public class LocationValidator : IValidateEntity
    {
        public Type GetValidatedType() => typeof(LocationValidator);

        public CRUDResult Validate(Entity entity) =>
        entity is Location location ? location.City is not null ? CRUDResult.Success : CRUDResult.EntityValidationFailed : CRUDResult.InvalidOperation;
    }
}
