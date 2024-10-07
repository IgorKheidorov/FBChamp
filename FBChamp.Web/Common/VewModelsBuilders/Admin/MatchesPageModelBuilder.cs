using FBChamp.Common.Paging;
using FBChamp.Core.DALModels;
using FBChamp.Core.UnitOfWork;
using FBChamp.Web.Areas.Admin.Controllers.Models;
using FBChamp.Web.Common.Helpers;

namespace FBChamp.Web.Common.VewModelsBuilders.Admin;

public class MatchesPageModelBuilder(IUnitOfWork unitOfWork)
    : ViewModelBuilder(unitOfWork)
{
    public override EntityModel Build(string parameters = "")
    {
        var page = parameters.GetIntValueFor("Page") ?? 1;
        var itemsPerPage = parameters.GetIntValueFor("ItemsPerPage") ?? 10;
        var filter = parameters.GetValueFor("Filter") ?? "";

        List<MatchModel> matchesModels = UnitOfWork.GetAllMatchModels().ToList();

        if (!string.IsNullOrWhiteSpace(filter))
        {
            matchesModels = matchesModels
                .Where(m => m.FullName.Contains(filter, StringComparison.OrdinalIgnoreCase) ||
                        m.HostTeam.FullName.Contains(filter, StringComparison.OrdinalIgnoreCase) ||
                        m.GuestTeam.FullName.Contains(filter, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        return new EntityPageModel<MatchModel>(GetPagedList(new PageInfo(page, itemsPerPage), matchesModels), filter);
    }

    private PagedList<MatchModel> GetPagedList(PageInfo pageInfo, IEnumerable<MatchModel> models) =>
        new PagedList<MatchModel>((IList<MatchModel>)models.Skip((pageInfo.Page - 1) * pageInfo.PerPage).Take(pageInfo.PerPage), models.Count(), pageInfo);
}