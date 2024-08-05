using FBChamp.Common.Paging;
using FBChamp.Core.DALModels;
using FBChamp.Core.Entities.Socker;
using FBChamp.Core.UnitOfWork;
using FBChamp.Web.Areas.Admin.Controllers.Models;
using FBChamp.Web.Areas.Admin.Controllers.Models.Coaches;
using FBChamp.Web.Areas.Admin.Controllers.Models.Players;
using FBChamp.Web.Areas.Admin.Controllers.Models.Teams;
using FBChamp.Web.Common.Helpers;

namespace FBChamp.Web.Common.VewModelsBuilders.Admin;

public class TeamsPageModelBuilder : ViewModelBuilder
{
    public TeamsPageModelBuilder(IUnitOfWork unitOfWork) : base(unitOfWork) { }

    public override EntityModel Build(string parameters)
    {
        var page = parameters.GetIntValueFor("Page") ?? 1;
        var itemsPerPage = parameters.GetIntValueFor("ItemsPerPage") ?? 10;
        var filter = parameters.GetValueFor("Filter");

        var teamModels = UnitOfWork.GetAllTeamModels();
       
        return new EntityPageModel<TeamModel>(GetPagedList(new PageInfo(page, itemsPerPage), teamModels), filter);
    }
    
    private PagedList<TeamModel> GetPagedList(PageInfo pageInfo, IEnumerable<TeamModel> teamModels) =>
        new PagedList<TeamModel>((IList<TeamModel>)teamModels.Skip((pageInfo.Page - 1) * pageInfo.PerPage).Take(pageInfo.PerPage), teamModels.Count(), pageInfo);        

}
