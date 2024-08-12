using FBChamp.Core.DALModels;
using FBChamp.Core.UnitOfWork;
using FBChamp.Web.Areas.Admin.Controllers.Models.Players;
using FBChamp.Web.Common.Helpers;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FBChamp.Web.Common.VewModelsBuilders.Shared;

public class PlayerCreateEditModelBuilder : ViewModelBuilder
{
    public PlayerCreateEditModelBuilder(IUnitOfWork unitOfWork) : base(unitOfWork) { }

    public override EntityModel Build(string parameters)
    {
        var player = UnitOfWork.GetPlayerModel(parameters.GetGuidValueFor("PlayerId"));
        var model = player is not null ? new PlayerCreateEditModel(player):  new PlayerCreateEditModel();
        model.Positions = new SelectList(UnitOfWork.GetAllPlayerPositions(), "Id", "Name");
        return model;
    }
    
}
