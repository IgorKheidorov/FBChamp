using FBChamp.Core.DataValidator;
using FBChamp.Core.Entities;
using FBChamp.Core.Entities.Socker;
using FBChamp.Core.UnitOfWork;
using FBChamp.Infrastructure.BusinessRules;

namespace FBChamp.Infrastructure.Validators;

public class PlayerValidator : IValidateEntity
{
    public Type GetValidatedType() => typeof(Player);

    public CRUDResult Validate(Entity entity) =>
        entity is Player player ? player.FullName.Length < DataRestrictions.NameLengthMax &&
                player.Height > 0 && player.Height < 250 ? CRUDResult.Success : CRUDResult.EnityValidationFailed : CRUDResult.InvalidOperation; 
}
