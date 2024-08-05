using FBChamp.Core.DALModels;
using FBChamp.Core.Entities.Socker;
using FBChamp.Core.UnitOfWork;
using FBChamp.Web.Areas.Admin.Controllers.Models;
using FBChamp.Web.Areas.Admin.Controllers.Models.Teams;
using FBChamp.Web.Common.Helpers;
using System.Text.Json;

namespace FBChamp.Web.Common.EntityBuilders.Admin;

internal class TeamBuilder : EntityBuilder
{
    public TeamBuilder(IUnitOfWork unitOfWork) : base(unitOfWork) { }

    public override bool Update(EntityModel viewModel)
    {
        var model = viewModel as TeamCreateEditModel;

        if (model is null)
            return false;

        byte[] photo = Convert.FromBase64String(model.PhotoString ?? "");
        if (model?.PhotoFile != null && model?.PhotoFile.Length > 0)
        {
            var stream = new MemoryStream();
            Task task = model.PhotoFile.CopyToAsync(stream);
            task.Wait();
            var bytes = stream.ToArray();
            photo = bytes;
        }

        return UnitOfWork.Commit(new Team(model.Id, model.FullName, photo));
    }
}
