using FBChamp.Core.DALModels;
using FBChamp.Core.UnitOfWork;
using FBChamp.Web.Common.Helpers;

namespace FBChamp.Web.Common.VewModelsBuilders.Shared;

internal class PlayerViewModelBuilder : ViewModelBuilder
{
    public PlayerViewModelBuilder(IUnitOfWork unitOfWork) : base(unitOfWork) { }

    public override EntityModel Build(string parameters) =>
        UnitOfWork.GetPlayerModel(parameters.GetGuidValueFor("PlayerId"));
}
