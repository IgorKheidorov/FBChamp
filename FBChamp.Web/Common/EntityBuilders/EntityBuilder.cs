using FBChamp.Core.DALModels;
using FBChamp.Core.Repositories;
using FBChamp.Web.Areas.Admin.Controllers.Models;
using FBChamp.Web.Common.Interfaces;

namespace FBChamp.Web.Common.EntityBuilders;

public abstract class EntityBuilder : IEntityBuilder
{
    protected readonly IUnitOfWork UnitOfWork;

    protected EntityBuilder(IUnitOfWork unitOfWork) => UnitOfWork = unitOfWork;
    
    public abstract bool Update(EntityModel model);

    public virtual bool Delete(Guid id) 
    {
        throw new NotImplementedException();
    }
}
