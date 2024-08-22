using FBChamp.Core.DALModels;
using FBChamp.Core.UnitOfWork;
using FBChamp.Web.Areas.Admin.Controllers.Models.Players;
using FBChamp.Web.Common.Helpers;

namespace FBChamp.Web.Common.VewModelsBuilders.Admin;

public class PlayerAssignModelBuilder(IUnitOfWork unitOfWork)
    : ViewModelBuilder(unitOfWork)
{
    public override EntityModel Build(string parameters)
    {
        var playerModel = UnitOfWork.GetPlayerModel(parameters.GetGuidValueFor("PlayerId"));
        uint playingNumber = uint.TryParse(playerModel?.PlayingNumber, out playingNumber) ? playingNumber : 0;

        return new PlayerAssignModel(playerModel ?? default, parameters.GetGuidValueFor("TeamId"), playingNumber);
    }
}