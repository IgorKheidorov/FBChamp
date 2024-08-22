using FBChamp.Core.DALModels;
using FBChamp.Core.UnitOfWork;
using FBChamp.Web.Areas.Admin.Controllers.Models.Coaches;
using FBChamp.Web.Common.Helpers;

namespace FBChamp.Web.Common.VewModelsBuilders.Shared;

public class CoachCreateEditModelBuilder(IUnitOfWork unitOfWork)
    : ViewModelBuilder(unitOfWork)
{
    public override EntityModel Build(string parameters)
    {
        var coach = UnitOfWork.GetCoachModel(parameters.GetGuidValueFor("CoachId"));

        return coach is not null ? new CoachCreateEditModel(coach) : new CoachCreateEditModel();
    }
}