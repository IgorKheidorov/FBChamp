using FBChamp.Web.Areas.Admin.Controllers.Models.Coaches;
using FBChamp.Web.Common.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FBChamp.Web.Areas.Admin.Controllers;

[Route("admin/coaches")]
public class CoachController(
    IViewModelBuildersFactory factory,
    IEntityBuildersFactory entityBuilderFactory)
    : BaseAdminController(factory, entityBuilderFactory)
{
    private const int ItemsPerPage = 10;

    public IActionResult List(int page = 1, string filter = null) =>
        CreateView("CoachesPageModel",
            $"Page:{page};ItemsPerPage:2;Filter:{filter}",
            "List");

    [HttpGet("create")]
    public IActionResult Create() =>
        CreateView("CoachCreateEditModel",
            "",
            "Create");

    [HttpPost("create")]
    public IActionResult Create(CoachCreateEditModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        UpdateRepository(model, "Coach");

        return RedirectToAction(nameof(List));
    }

    [Route("{id:guid}")]
    public IActionResult Profile(Guid id) =>
        CreateView("CoachViewModel",
            $"CoachId:{id}",
            "Profile");

    [HttpGet("update/{id:guid}")]
    public IActionResult Update(Guid id) =>
        CreateView("CoachCreateEditModel",
            $"CoachId:{id}",
            "Update");

    [HttpPost("update/{id:guid}")]
    public IActionResult Update(CoachCreateEditModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        UpdateRepository(model, "Coach");

        return RedirectToAction(nameof(List));
    }
}