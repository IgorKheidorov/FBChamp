using FBChamp.Common.Paging;
using FBChamp.Web.Areas.Admin.Controllers.Models;
using FBChamp.Web.Common.Helpers;
using FBChamp.Web.Common.Interfaces;
using FBChamp.Web.Areas.Admin.Controllers.Models.Coaches;
using FBChamp.Core.DALModels;
using FBChamp.Web.Areas.Admin.Controllers.Models.Players;
using FBChamp.Core.UnitOfWork;

namespace FBChamp.Web.Common.VewModelsBuilders.Admin
{
    public class CoachesPageModelBuilder : ViewModelBuilder
    {
        private readonly IViewModelBuildersFactory _factory;

        public CoachesPageModelBuilder(IUnitOfWork unitOfWork, IViewModelBuildersFactory factory) : base(unitOfWork)
        {
            _factory = factory;
        }

        public override EntityModel Build(string parameters = "")
        {
            int page = parameters.GetIntValueFor("Page") ?? 1;
            int itemsPerPage = parameters.GetIntValueFor("ItemsPerPage") ?? 10;
            string filter = parameters.GetValueFor("Filter");
            Guid currentTeamId = parameters.GetGuidValueFor("TeamId");
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
        new PagedList<CoachModel>((IList<CoachModel>)models.Skip((pageInfo.Page - 1) * pageInfo.PerPage).Take(pageInfo.PerPage), models.Count(), pageInfo);

    }
}
