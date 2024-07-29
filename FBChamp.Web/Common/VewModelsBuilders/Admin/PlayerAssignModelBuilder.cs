using FBChamp.Core.DALModels;
using FBChamp.Core.Repositories;
using FBChamp.Web.Areas.Admin.Controllers.Models;
using FBChamp.Web.Areas.Admin.Controllers.Models.Players;
using FBChamp.Web.Common.Helpers;

namespace FBChamp.Web.Common.VewModelsBuilders.Admin;

public class PlayerAssignModelBuilder : ViewModelBuilder
{
    public PlayerAssignModelBuilder(IUnitOfWork unitOfWork) : base(unitOfWork) { }

    public override EntityModel Build(string parameters)
    {
        var playerModel = UnitOfWork.GetPlayerModel(parameters.GetGuidValueFor("PlayerId"));
        uint playingNumber = uint.TryParse(playerModel?.PlayingNumber, out playingNumber) ? playingNumber : 0;
        return new PlayerAssignModel(playerModel ?? default, parameters.GetGuidValueFor("TeamId"), playingNumber);
    }
}
