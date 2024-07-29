using FBChamp.Core.DALModels;
using FBChamp.Core.Entities;
using FBChamp.Web.Areas.Admin.Controllers.Models;

namespace FBChamp.Web.Common.Interfaces
{
    public interface IEntityBuilder
    {
        bool Update(EntityModel model);
        bool Delete(Guid id);
    }
}
