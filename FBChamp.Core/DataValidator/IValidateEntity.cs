using FBChamp.Core.Entities;
using FBChamp.Core.UnitOfWork;

namespace FBChamp.Core.DataValidator;

public interface IValidateEntity
{
    /// <summary>
    /// Set the Entity type to be validated here
    /// </summary>
    /// <returns></returns>
    Type GetValidatedType();

    CRUDResult Validate(Entity entities);
}
