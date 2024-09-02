using FBChamp.Core.DataValidator;
using FBChamp.Core.Entities;
using FBChamp.Core.Entities.Soccer;
using FBChamp.Core.UnitOfWork;

namespace FBChamp.Infrastructure.Validators;

internal class StadiumValidator : IValidateEntity
{
    public Type GetValidatedType() => typeof(Stadium);

    public CRUDResult Validate(Entity entity) =>
        entity is Stadium stadium ? stadium.Name is not null ? CRUDResult.Success : CRUDResult.EntityValidationFailed : CRUDResult.InvalidOperation;
}