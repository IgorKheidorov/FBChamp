using FBChamp.Core.Entities.Socker;
using FBChamp.Core.Entities;
using FBChamp.Core.Repositories;
using FBChamp.Infrastructure;
using FBChamp.Web.Areas.Admin.Controllers.Models.Players;
using FBChamp.Web.Areas.Admin.Controllers.Models;
using FBChamp.Web.Areas.Admin.Controllers.Models.Coaches;
using FBChamp.Core.DALModels;

namespace FBChamp.Web.Common.EntityBuilders.Admin;

internal class CoachAssignmentInfoBuilder: EntityBuilder
{
    public CoachAssignmentInfoBuilder(IUnitOfWork unitOfWork) : base(unitOfWork) { }
    
    public override bool Update(EntityModel viewModel)
    {
        var model = viewModel as CoachAssignModel;

        return (viewModel is not null) ? UnitOfWork.Commit(new List<Entity>()
                                     {new CoachAssignmentInfo(model.Id, model.TeamId, model.Role)})
                                   : false;
    }

    public override bool Delete(Guid id) => UnitOfWork.Remove(id, typeof(CoachAssignmentInfo));

}