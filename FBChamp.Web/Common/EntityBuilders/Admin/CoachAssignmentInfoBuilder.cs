using FBChamp.Core.Entities.Socker;
using FBChamp.Core.Entities;
using FBChamp.Web.Areas.Admin.Controllers.Models.Coaches;
using FBChamp.Core.DALModels;
using FBChamp.Core.UnitOfWork;

namespace FBChamp.Web.Common.EntityBuilders.Admin;

internal class CoachAssignmentInfoBuilder: EntityBuilder
{
    public CoachAssignmentInfoBuilder(IUnitOfWork unitOfWork) : base(unitOfWork) { }
    
    public override CRUDResult CreateUpdate(EntityModel viewModel)=>
        viewModel is CoachAssignModel model ?
            UnitOfWork.Commit(new CoachAssignmentInfo(model.Id, model.TeamId, model.Role))
         : CRUDResult.Failed;    

    public override CRUDResult Delete(Guid id) => UnitOfWork.Remove(id, typeof(CoachAssignmentInfo));

}