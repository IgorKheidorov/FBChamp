using FBChamp.Core.DALModels;
using FBChamp.Web.Common.Interfaces;
using FBChamp.Web.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FBChamp.Web.Areas.Api.Controllers;

[Area("Api")]
[ApiController]
[Authorize("Admin")]
public class BaseApiController : BaseController
{
    public BaseApiController(IViewModelBuildersFactory factory = null, IEntityBuildersFactory entityFactory = null) : base(factory, entityFactory)
    {
    }

    public BaseApiController() { }

    protected IActionResult CreateView(string modelTypeName, string parameters, string viewName)
    {
        var model = ViewModelBuilderFactory?.GetBuilder(modelTypeName)?.Build(parameters);
        return model is null ? NotFound() : View(viewName, model);
    }

    protected bool UpdateRepository(EntityModel model, string entityType) =>
        (bool)(EntityBuilderFactory?.GetBuilder(entityType)?.Update(model));


    protected bool DeleteFromRepository(Guid guid, string entityType) =>
        EntityBuilderFactory.GetBuilder(entityType).Delete(guid);

}