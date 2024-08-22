using FBChamp.Core.DALModels;
using FBChamp.Core.Entities.Socker;
using FBChamp.Core.UnitOfWork;
using FBChamp.Web.Areas.Admin.Controllers.Models.Teams;

namespace FBChamp.Web.Common.EntityBuilders.Admin;

internal class TeamBuilder(IUnitOfWork unitOfWork)
    : EntityBuilder(unitOfWork)
{
    public override CRUDResult CreateUpdate(EntityModel viewModel)
    {
        if (viewModel is not TeamCreateEditModel model)
        {
            return CRUDResult.Failed;
        }

        var photo = Convert.FromBase64String(model.PhotoString ?? "");

        if (model?.PhotoFile != null && model?.PhotoFile.Length > 0)
        {
            var stream = new MemoryStream();
            model.PhotoFile.CopyToAsync(stream).Wait();
            photo = stream.ToArray();
        }

        return UnitOfWork.Commit(new Team(model.Id, model.FullName, photo));
    }

    public override CRUDResult Delete(Guid id) =>
        UnitOfWork.Remove(id, typeof(Team));
}