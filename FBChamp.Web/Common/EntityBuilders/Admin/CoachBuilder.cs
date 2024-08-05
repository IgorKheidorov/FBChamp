using FBChamp.Core.Entities.Socker;
using FBChamp.Core.Entities;
using FBChamp.Web.Areas.Admin.Controllers.Models;
using FBChamp.Web.Areas.Admin.Controllers.Models.Coaches;
using FBChamp.Web.Common.Helpers;
using FBChamp.Core.DALModels;
using FBChamp.Core.UnitOfWork;

namespace FBChamp.Web.Common.EntityBuilders.Admin;

public class CoachBuilder : EntityBuilder
{
    public CoachBuilder(IUnitOfWork unitOfWork) : base(unitOfWork) { }

    public override bool Update(EntityModel viewModel)
    {
        var model = viewModel as CoachCreateEditModel;
        var coachId = model?.Id == default ? Guid.NewGuid() : model.Id;

        return model is not null ? UnitOfWork.Commit(new List<Entity>() 
        {
            new Coach(coachId, model.Name, model.BirthDate,
                            model.PhotoFile is not null ? model.PhotoFile.GetByteImage() : Convert.FromBase64String(model.PhotoString ?? ""))
        }) : false;
    }        
}
