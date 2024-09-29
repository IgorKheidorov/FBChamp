using FBChamp.Core.DALModels;
using FBChamp.Core.UnitOfWork;
using FBChamp.Web.Areas.Admin.Controllers.Models;
using FBChamp.Web.Areas.Admin.Controllers.Models.Matches;
using FBChamp.Web.Common.Helpers;
using FBChamp.Web.Common.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace FBChamp.Web.Common.VewModelsBuilders.Shared;

public class MatchCreateEditModelBuilder(
    IUnitOfWork unitOfWork,
    IViewModelBuildersFactory factory)
    : ViewModelBuilder(unitOfWork)
{
    public override EntityModel Build(string parameters)
    {
        if (parameters.IsNullOrEmpty())
        {
            return new MatchCreateEditModel();
        }

        var match = UnitOfWork.GetMatchModel(parameters.GetGuidValueFor("MatchId"));

        return match is null ? null : new MatchCreateEditModel(match);
    }
}