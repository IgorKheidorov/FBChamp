using FBChamp.Core.DALModels;
using FBChamp.Core.UnitOfWork;
using FBChamp.Web.Areas.Admin.Controllers.Models;
using FBChamp.Web.Common.Interfaces;

namespace FBChamp.Web.Common.VewModelsBuilders
{
    public abstract class ViewModelBuilder : IViewModelBuilder
    {
        protected readonly char Separator = ';';

        protected readonly IUnitOfWork UnitOfWork;

        protected ViewModelBuilder(IUnitOfWork unitOfWork) => UnitOfWork = unitOfWork;

        public abstract EntityModel Build(string item);
    }
}
