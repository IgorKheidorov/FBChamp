using FBChamp.Common.Paging;
using FBChamp.Core.DALModels;
using FBChamp.Core.UnitOfWork;
using FBChamp.Web.Areas.Admin.Controllers.Models;
using FBChamp.Web.Common.Helpers;

namespace FBChamp.Web.Common.VewModelsBuilders.Admin;

public class PlayersPageModelBuilder(IUnitOfWork unitOfWork)
    : ViewModelBuilder(unitOfWork)
{
    public override EntityModel Build(string parameters = "")
    {
        var page = parameters.GetIntValueFor("Page") ?? 1;
        var itemsPerPage = parameters.GetIntValueFor("ItemsPerPage") ?? 10;
        var filter = parameters.GetValueFor("Filter") ?? "";
        var viewMode = parameters.GetValueFor("ViewMode") ?? "All";
        var currentTeamId = parameters.GetGuidValueFor("TeamId");
        var mode = parameters.GetValueFor("Mode");

        var playerModels = mode switch
        {
            "Include" => UnitOfWork.GetAssignedPlayerModels(currentTeamId),
            "Assign" => UnitOfWork.GetUnassignedPlayerModels(),
            _ => UnitOfWork.GetAllPlayerModels()
        };

        return new EntityPageModel<PlayerModel>(
            GetPagedList(new PageInfo(page, itemsPerPage), playerModels),
            filter,
            currentTeamId);
    }

    private PagedList<PlayerModel> GetPagedList(PageInfo pageInfo, IEnumerable<PlayerModel> models) =>
        new((IList<PlayerModel>)models
            .Skip((pageInfo.Page - 1) * pageInfo.PerPage)
            .Take(pageInfo.PerPage), models.Count(), pageInfo);
}