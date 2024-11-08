﻿using FBChamp.Core.DALModels;
using FBChamp.Core.Entities.Soccer;
using FBChamp.Core.UnitOfWork;
using FBChamp.Web.Areas.Admin.Controllers.Models.Players;

namespace FBChamp.Web.Common.EntityBuilders.Admin;

internal class PlayerAssignmentInfoBuilder(IUnitOfWork unitOfWork)
    : EntityBuilder(unitOfWork)
{
    public override CRUDResult CreateUpdate(EntityModel viewModel) =>
        viewModel is PlayerAssignModel model
            ? UnitOfWork.Commit(new PlayerAssignmentInfo(model.Id, model.TeamId, model.PlayingNumber))
            : CRUDResult.Failed;

    public override CRUDResult Delete(Guid id) =>
        UnitOfWork.Remove(id, typeof(PlayerAssignmentInfo));
}