﻿using FBChamp.Core.Entities.Socker;
using FBChamp.Core.Repositories;
using FBChamp.Web.Areas.Admin.Controllers.Models.Players;
using FBChamp.Web.Areas.Admin.Controllers.Models;
using FBChamp.Web.Common.Helpers;
using FBChamp.Core.DALModels;

namespace FBChamp.Web.Common.EntityBuilders.Admin;

public class PlayerBuilder: EntityBuilder
{
    public PlayerBuilder(IUnitOfWork unitOfWork) : base(unitOfWork) { }

    public override bool Update(EntityModel viewModel )
    {
        var model = viewModel as PlayerCreateEditModel;

        if (model is null)
            return false;

        var playerId = model.Id == default ? Guid.NewGuid() : model.Id;

        return UnitOfWork.Commit(
            new Player( playerId, model.Name, model.BirthDate, 
                        model.Heigth, model.PositionId,
                        model.PhotoFile is not null ? model.PhotoFile.GetByteImage() : Convert.FromBase64String(model.PhotoString ?? ""),
                        model.Description));
    }

    public override bool Delete(Guid id) => UnitOfWork.Remove(id, typeof(Player));
    
}
