using FBChamp.Core.DALModels;
using FBChamp.Core.UnitOfWork;
using FBChamp.Web.Common.Helpers;

namespace FBChamp.Web.Common.VewModelsBuilders.Shared;

public class MatchViewModelBuilder(IUnitOfWork unitOfWork)
    : ViewModelBuilder(unitOfWork)
{
    public override EntityModel Build(string parameters) =>
        UnitOfWork.GetMatchModel(parameters.GetGuidValueFor("MatchId"));
}