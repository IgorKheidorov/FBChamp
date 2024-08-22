using FBChamp.Core.DALModels;
using FBChamp.Core.UnitOfWork;
using FBChamp.Web.Areas.Admin.Controllers.Models.Coaches;
using FBChamp.Web.Common.Helpers;
using FBChamp.Web.Common.Interfaces;

namespace FBChamp.Web.Common.VewModelsBuilders.Admin;

public class CoachAssignModelBuilder(
    IUnitOfWork unitOfWork,
    IViewModelBuildersFactory factory)
    : ViewModelBuilder(unitOfWork)
{
    public override EntityModel Build(string parameters)
    {
        var coachModel = factory.GetBuilder("CoachViewModel").Build(parameters) as CoachModel;

        return new CoachAssignModel(coachModel, parameters.GetGuidValueFor("TeamId"));
    }
}