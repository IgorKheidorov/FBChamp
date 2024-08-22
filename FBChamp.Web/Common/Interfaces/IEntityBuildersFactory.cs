namespace FBChamp.Web.Common.Interfaces;

public interface IEntityBuildersFactory
{
    IEntityBuilder GetBuilder(string viewModelType);
}