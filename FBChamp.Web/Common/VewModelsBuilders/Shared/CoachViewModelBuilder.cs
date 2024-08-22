using FBChamp.Core.DALModels;
using FBChamp.Core.UnitOfWork;
using FBChamp.Web.Common.Helpers;

namespace FBChamp.Web.Common.VewModelsBuilders.Shared;

public class CoachViewModelBuilder(IUnitOfWork unitOfWork)
    : ViewModelBuilder(unitOfWork)
{
    public override EntityModel Build(string parameters) =>
        UnitOfWork.GetCoachModel(parameters.GetGuidValueFor("CoachId"));
}