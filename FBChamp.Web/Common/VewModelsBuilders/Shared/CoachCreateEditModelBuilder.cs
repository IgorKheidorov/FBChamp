using FBChamp.Web.Areas.Admin.Controllers.Models;
using FBChamp.Web.Common.Helpers;
using FBChamp.Web.Areas.Admin.Controllers.Models.Coaches;
using FBChamp.Core.DALModels;
using FBChamp.Web.Areas.Admin.Controllers.Models.Players;
using Microsoft.AspNetCore.Mvc.Rendering;
using FBChamp.Core.UnitOfWork;

namespace FBChamp.Web.Common.VewModelsBuilders.Shared
{
    public class CoachCreateEditModelBuilder : ViewModelBuilder
    {
        public CoachCreateEditModelBuilder(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        public override EntityModel Build(string parameters)
        {
            var coach = UnitOfWork.GetCoachModel(parameters.GetGuidValueFor("CoachId"));
            return coach is not null ? new CoachCreateEditModel(coach) : new CoachCreateEditModel();            
        }

    }
}
