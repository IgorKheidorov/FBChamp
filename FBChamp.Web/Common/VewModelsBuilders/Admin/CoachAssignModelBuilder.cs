using FBChamp.Web.Areas.Admin.Controllers.Models;
using FBChamp.Web.Common.Helpers;
using FBChamp.Web.Common.Interfaces;
using FBChamp.Web.Areas.Admin.Controllers.Models.Coaches;
using FBChamp.Core.DALModels;
using FBChamp.Core.UnitOfWork;

namespace FBChamp.Web.Common.VewModelsBuilders.Admin;

public class CoachAssignModelBuilder : ViewModelBuilder
{
    IViewModelBuildersFactory _factory;

    public CoachAssignModelBuilder(IUnitOfWork unitOfWork, IViewModelBuildersFactory factory) : base(unitOfWork)
    {
        _factory = factory;
    }

    public override EntityModel Build(string parameters)
    {
        var coachModel = _factory.GetBuilder("CoachViewModel").Build(parameters) as CoachModel;

        return new CoachAssignModel(coachModel, parameters.GetGuidValueFor("TeamId"));
    }

}
