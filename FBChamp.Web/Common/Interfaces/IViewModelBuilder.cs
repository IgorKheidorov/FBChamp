using FBChamp.Core.DALModels;

namespace FBChamp.Web.Common.Interfaces;

public interface IViewModelBuilder
{
    EntityModel Build(string item);
}