using FBChamp.Core.Entities.Socker;
using FBChamp.Core.Entities;
using FBChamp.Core.UnitOfWork;
using FBChamp.Infrastructure.BusinessRules;

using FBChamp.Core.DataValidator;

namespace FBChamp.Infrastructure.Validators;

internal class TeamValidator: IValidateEntity
{
    public Type GetValidatedType() => typeof(Team);

    public CRUDResult Validate(Entity entity) =>    
        entity is Team team ? team.Name.Length < DataRestrictions.NameLengthMax ? CRUDResult.Success : CRUDResult.EnityValidationFailed :
         CRUDResult.InvalidOperation;    
}
