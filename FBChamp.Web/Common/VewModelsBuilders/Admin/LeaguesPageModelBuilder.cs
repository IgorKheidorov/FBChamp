using FBChamp.Common.Paging;
using FBChamp.Core.DALModels;
using FBChamp.Core.UnitOfWork;
using FBChamp.Web.Areas.Admin.Controllers.Models;
using FBChamp.Web.Common.Helpers;

namespace FBChamp.Web.Common.VewModelsBuilders.Admin;

public class LeaguesPageModelBuilder(IUnitOfWork unitOfWork)
    : ViewModelBuilder(unitOfWork)
{
    public override EntityModel Build(string parameters = "")
    {
        var page = parameters.GetIntValueFor("Page") ?? 1;
        var itemsPerPage = parameters.GetIntValueFor("ItemsPerPage") ?? 10;
        var filter = parameters.GetValueFor("Filter") ?? "";

        List<LeagueModel> leaguesModels = UnitOfWork.GetAllLeagueModels().ToList();

        if(!string.IsNullOrWhiteSpace(filter))
        {
            leaguesModels = leaguesModels
                .Where(l => l.FullName.Contains(filter, StringComparison.OrdinalIgnoreCase) ||
                       l.Teams.Any(t => t.FullName.Contains(filter, StringComparison.OrdinalIgnoreCase)))
                .ToList();
        }

        return new EntityPageModel<LeagueModel>(GetPagedList(new PageInfo(page, itemsPerPage), leaguesModels), filter);
    }

    private PagedList<LeagueModel> GetPagedList(PageInfo pageInfo, IEnumerable<LeagueModel> models) =>
       new PagedList<LeagueModel>((IList<LeagueModel>)models.Skip((pageInfo.Page - 1) * pageInfo.PerPage).Take(pageInfo.PerPage), models.Count(), pageInfo);
}
