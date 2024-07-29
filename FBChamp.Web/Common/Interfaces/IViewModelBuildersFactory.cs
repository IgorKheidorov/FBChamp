using Microsoft.Identity.Client;

namespace FBChamp.Web.Common.Interfaces
{
    public interface IViewModelBuildersFactory
    {
        IViewModelBuilder GetBuilder(string forType);
    }
}
