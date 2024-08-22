using FBChamp.Core.DALModels;
using FBChamp.Core.UnitOfWork;
using FBChamp.Web.Areas.Admin.Controllers.Models;
using FBChamp.Web.Areas.Admin.Controllers.Models.Teams;
using FBChamp.Web.Common.Helpers;
using FBChamp.Web.Common.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace FBChamp.Web.Common.VewModelsBuilders.Admin;

public class TeamCreateEditModelBuilder(
    IUnitOfWork unitOfWork,
    IViewModelBuildersFactory factory)
    : ViewModelBuilder(unitOfWork)
{
    public override EntityModel Build(string item)
    {
        if (item.IsNullOrEmpty())
        {
            return new TeamCreateEditModel();
        }

        var team = UnitOfWork.GetTeamModel(item.GetGuidValueFor("TeamId"));

        var playersPageModel = factory.GetBuilder("PlayersPageModel").Build(item) as EntityPageModel<PlayerModel>;
        var coachesPageModel = factory.GetBuilder("CoachesPageModel").Build(item) as EntityPageModel<CoachModel>;

        return team is null ? null : new TeamCreateEditModel(team, playersPageModel, coachesPageModel);
    }
}