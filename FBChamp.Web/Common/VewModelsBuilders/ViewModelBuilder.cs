using FBChamp.Core.DALModels;
using FBChamp.Core.UnitOfWork;
using FBChamp.Web.Common.Interfaces;

namespace FBChamp.Web.Common.VewModelsBuilders;

public abstract class ViewModelBuilder(IUnitOfWork unitOfWork)
    : IViewModelBuilder
{
    protected readonly char Separator = ';';

    protected readonly IUnitOfWork UnitOfWork = unitOfWork;

    public abstract EntityModel Build(string item);
}