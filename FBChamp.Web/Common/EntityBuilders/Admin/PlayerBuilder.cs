using FBChamp.Core.Entities.Socker;
using FBChamp.Web.Areas.Admin.Controllers.Models.Players;
using FBChamp.Web.Areas.Admin.Controllers.Models;
using FBChamp.Web.Common.Helpers;
using FBChamp.Core.DALModels;
using FBChamp.Core.UnitOfWork;

namespace FBChamp.Web.Common.EntityBuilders.Admin;

public class PlayerBuilder: EntityBuilder
{
    public PlayerBuilder(IUnitOfWork unitOfWork) : base(unitOfWork) { }

    public override CRUDResult CreateUpdate(EntityModel viewModel )
    {
        var model = viewModel as PlayerCreateEditModel;

        if (model is null)
            return CRUDResult.Failed
;

    var playerId = model.Id == default ? Guid.NewGuid() : model.Id;

        return UnitOfWork.Commit(
            new Player( playerId, model.Name, model.BirthDate, 
                        model.Heigth, model.PositionId,
                        model.PhotoFile is not null ? model.PhotoFile.GetByteImage() : Convert.FromBase64String(model.PhotoString ?? ""),
                        model.Description));
    }

    public override CRUDResult Delete(Guid id) => UnitOfWork.Remove(id, typeof(Player));
    
}
