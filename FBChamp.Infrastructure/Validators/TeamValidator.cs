using FBChamp.Core.DataValidator;
using FBChamp.Core.Entities;
using FBChamp.Core.Entities.Soccer;
using FBChamp.Core.UnitOfWork;
using FBChamp.Infrastructure.BusinessRules;

namespace FBChamp.Infrastructure.Validators;

internal class TeamValidator: IValidateEntity
{
    public Type GetValidatedType() => typeof(Team);

    public CRUDResult Validate(Entity entity) =>    
        entity is Team team ? team.Name.Length < DataRestrictions.NameLengthMax ? CRUDResult.Success : CRUDResult.EntityValidationFailed :
         CRUDResult.InvalidOperation;    
}
