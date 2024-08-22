using FBChamp.Core.DALModels;
using FBChamp.Core.UnitOfWork;
using FBChamp.Web.Common.Interfaces;

namespace FBChamp.Web.Common.EntityBuilders;

public abstract class EntityBuilder(IUnitOfWork unitOfWork)
    : IEntityBuilder
{
    protected readonly IUnitOfWork UnitOfWork = unitOfWork;

    public abstract CRUDResult CreateUpdate(EntityModel model);

    public abstract CRUDResult Delete(Guid id);
}