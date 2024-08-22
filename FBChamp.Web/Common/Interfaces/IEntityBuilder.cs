using FBChamp.Core.DALModels;
using FBChamp.Core.UnitOfWork;

namespace FBChamp.Web.Common.Interfaces;

public interface IEntityBuilder
{
    CRUDResult CreateUpdate(EntityModel model);

    CRUDResult Delete(Guid id);
}