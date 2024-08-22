using FBChamp.Common.Paging;
using FBChamp.Core.DALModels;
using FBChamp.Core.UnitOfWork;
using FBChamp.Web.Areas.Admin.Controllers.Models;
using FBChamp.Web.Common.Helpers;
using FBChamp.Web.Common.Interfaces;

namespace FBChamp.Web.Common.VewModelsBuilders.Admin;

public class CoachesPageModelBuilder(
    IUnitOfWork unitOfWork,
    IViewModelBuildersFactory factory)
    : ViewModelBuilder(unitOfWork)
{
    private readonly IViewModelBuildersFactory _factory = factory;

    public override EntityModel Build(string parameters = "")
    {
        var page = parameters.GetIntValueFor("Page") ?? 1;
        var itemsPerPage = parameters.GetIntValueFor("ItemsPerPage") ?? 10;
        var filter = parameters.GetValueFor("Filter");
        var currentTeamId = parameters.GetGuidValueFor("TeamId");
        var mode = parameters.GetValueFor("Mode");

        var coachModels = mode switch
        {
            "Include" => UnitOfWork.GetAssignedCoachModels(currentTeamId),
            "Assign" => UnitOfWork.GetUnassignedCoachModels(),
            _ => UnitOfWork.GetAllCoachModels()
        };

        return new EntityPageModel<CoachModel>(GetPagedList(new PageInfo(page, itemsPerPage), coachModels),
            filter,
            currentTeamId);
    }

    private PagedList<CoachModel> GetPagedList(PageInfo pageInfo, IEnumerable<CoachModel> models) =>
        new((IList<CoachModel>)models
            .Skip((pageInfo.Page - 1) * pageInfo.PerPage)
            .Take(pageInfo.PerPage), models.Count(), pageInfo);
}