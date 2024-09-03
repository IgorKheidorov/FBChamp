using FBChamp.Core.DataValidator;
using FBChamp.Core.Entities;
using FBChamp.Core.Entities.Soccer;
using FBChamp.Core.UnitOfWork;

namespace FBChamp.Infrastructure.Validators
{
    public class CountryValidator : IValidateEntity
    {
        public Type GetValidatedType() => typeof(Country);

        public CRUDResult Validate(Entity entity) =>
        entity is Country country ? country.Name is not null ? CRUDResult.Success : CRUDResult.EntityValidationFailed : CRUDResult.InvalidOperation;
    }
}
