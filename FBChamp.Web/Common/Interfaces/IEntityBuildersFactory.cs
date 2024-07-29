using FBChamp.Web.Common.VewModelsBuilders;

namespace FBChamp.Web.Common.Interfaces;

public interface IEntityBuildersFactory
{
    IEntityBuilder GetBuilder(string viewModelType);
}
