﻿using FBChamp.Core.Entities.Socker;
using FBChamp.Web.Areas.Admin.Controllers.Models.Coaches;
using FBChamp.Web.Common.Helpers;
using FBChamp.Core.DALModels;
using FBChamp.Core.UnitOfWork;

namespace FBChamp.Web.Common.EntityBuilders.Admin;

public class CoachBuilder : EntityBuilder
{
    public CoachBuilder(IUnitOfWork unitOfWork) : base(unitOfWork) { }
    
    public override CRUDResult CreateUpdate(EntityModel viewModel) =>
        viewModel is CoachCreateEditModel model ?
            UnitOfWork.Commit(
                new Coach(model?.Id == default ? Guid.NewGuid() : model.Id, 
                          model.Name,
                          model.BirthDate,
                          model.PhotoFile is not null ? model.PhotoFile.GetByteImage() : Convert.FromBase64String(model.PhotoString ?? ""))            ) 
        : CRUDResult.Failed;

    public override CRUDResult Delete(Guid id)
        => UnitOfWork.Remove(id, typeof(Coach));
    
}