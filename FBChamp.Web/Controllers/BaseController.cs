using FBChamp.Web.Common.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FBChamp.Web.Controllers;

[Authorize]
public class BaseController : Controller
{
    protected readonly IEntityBuildersFactory EntityBuilderFactory;
    protected readonly IViewModelBuildersFactory ViewModelBuilderFactory;

    public BaseController()
    {
    }

    public BaseController(IViewModelBuildersFactory viewModelBuildersFactory = null,
        IEntityBuildersFactory entityFactory = null)
    {
        ViewModelBuilderFactory = viewModelBuildersFactory;
        EntityBuilderFactory = entityFactory;
    }
}