using FBChamp.Core.Entities.Socker;
using FBChamp.Core.Entities;
using FBChamp.Web.Areas.Admin.Controllers.Models;
using FBChamp.Web.Areas.Admin.Controllers.Models.Players;
using FBChamp.Core.DALModels;
using FBChamp.Core.UnitOfWork;

namespace FBChamp.Web.Common.EntityBuilders.Admin;

internal class PlayerAssignmentInfoBuilder : EntityBuilder
{
    public PlayerAssignmentInfoBuilder(IUnitOfWork unitOfWork) : base(unitOfWork) { }

    public override bool Update(EntityModel viewModel)
    {
        var model = viewModel as PlayerAssignModel;

        return (viewModel is not null) ? UnitOfWork.Commit(new List<Entity>() 
                                         {new PlayerAssignmentInfo(model.Id, model.TeamId, model.PlayingNumber)})
                                       : false;
    }

    public override bool Delete(Guid id) => UnitOfWork.Remove(id, typeof(PlayerAssignmentInfo));
    
}