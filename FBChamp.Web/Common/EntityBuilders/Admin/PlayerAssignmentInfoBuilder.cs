using FBChamp.Core.Entities.Socker;
using FBChamp.Web.Areas.Admin.Controllers.Models.Players;
using FBChamp.Core.DALModels;
using FBChamp.Core.UnitOfWork;

namespace FBChamp.Web.Common.EntityBuilders.Admin;

internal class PlayerAssignmentInfoBuilder : EntityBuilder
{
    public PlayerAssignmentInfoBuilder(IUnitOfWork unitOfWork) : base(unitOfWork) { }

    public override CRUDResult CreateUpdate(EntityModel viewModel)=>
        viewModel is PlayerAssignModel model ?
            UnitOfWork.Commit(new PlayerAssignmentInfo(model.Id, model.TeamId, model.PlayingNumber))
        : CRUDResult.Failed;
    

    public override CRUDResult Delete(Guid id) => UnitOfWork.Remove(id, typeof(PlayerAssignmentInfo));
    
}