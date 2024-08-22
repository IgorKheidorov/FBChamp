using FBChamp.Core.DALModels;
using FBChamp.Core.UnitOfWork;
using FBChamp.Web.Common.Interfaces;
using FBChamp.Web.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FBChamp.Web.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize("Admin")]
public class BaseAdminController : BaseController
{
    public BaseAdminController()
    {
    }

    public BaseAdminController(IViewModelBuildersFactory factory = null, IEntityBuildersFactory entityFactory = null)
        : base(factory, entityFactory)
    {
    }

    protected IActionResult CreateView(string modelTypeName, string parameters, string viewName)
    {
        var model = ViewModelBuilderFactory?.GetBuilder(modelTypeName)?.Build(parameters);

        return model is null ? NotFound() : View(viewName, model);
    }

    protected CRUDResult UpdateRepository(EntityModel model, string entityType) =>
        (CRUDResult)EntityBuilderFactory?.GetBuilder(entityType)?.CreateUpdate(model);

    protected CRUDResult DeleteFromRepository(Guid guid, string entityType) =>
        EntityBuilderFactory.GetBuilder(entityType).Delete(guid);
}