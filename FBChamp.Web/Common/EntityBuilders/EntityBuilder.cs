using FBChamp.Core.DALModels;
using FBChamp.Core.UnitOfWork;
using FBChamp.Web.Common.Interfaces;

namespace FBChamp.Web.Common.EntityBuilders;



public abstract class EntityBuilder : IEntityBuilder
{
    protected readonly IUnitOfWork UnitOfWork;

    protected EntityBuilder(IUnitOfWork unitOfWork) => UnitOfWork = unitOfWork;
    
    public abstract CRUDResult CreateUpdate(EntityModel model);

    public abstract CRUDResult Delete(Guid id); 
}
