using FBChamp.Core.Repositories;
using FBChamp.Web.Common.Helpers;
using FBChamp.Core.DALModels;

namespace FBChamp.Web.Common.VewModelsBuilders.Shared;

public class CoachViewModelBuilder : ViewModelBuilder
{
    public CoachViewModelBuilder(IUnitOfWork unitOfWork) : base(unitOfWork) { }

    public override EntityModel Build(string parameters) => UnitOfWork.GetCoachModel(parameters.GetGuidValueFor("CoachId"));
}
