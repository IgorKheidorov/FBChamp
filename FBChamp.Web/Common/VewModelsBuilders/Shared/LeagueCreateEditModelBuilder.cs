using FBChamp.Core.DALModels;
using FBChamp.Core.UnitOfWork;
using FBChamp.Web.Areas.Admin.Controllers.Models;
using FBChamp.Web.Areas.Admin.Controllers.Models.Leagues;
using FBChamp.Web.Common.Helpers;
using FBChamp.Web.Common.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace FBChamp.Web.Common.VewModelsBuilders.Shared;

public class LeagueCreateEditModelBuilder(
    IUnitOfWork unitOfWork,
    IViewModelBuildersFactory factory)
    : ViewModelBuilder(unitOfWork)
{
    public override EntityModel Build(string item)
    {
        if(item.IsNullOrEmpty())
        {
            return new LegueCreateEditModel();
        }

        var league = UnitOfWork.GetLeagueModel(item.GetGuidValueFor("LeagueId"));

        var teamsPageModel = factory.GetBuilder("TeamsPageModel").Build(item) as EntityPageModel<TeamModel>;

        return league is null ? null : new LegueCreateEditModel(league, teamsPageModel);
    }
}
