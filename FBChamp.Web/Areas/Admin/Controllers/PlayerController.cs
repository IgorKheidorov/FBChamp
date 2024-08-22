using FBChamp.Core.UnitOfWork;
using FBChamp.Web.Areas.Admin.Controllers.Models.Players;
using FBChamp.Web.Common.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FBChamp.Web.Areas.Admin.Controllers;

[Route("admin/players")]
public class PlayerController(
    IViewModelBuildersFactory factory,
    IEntityBuildersFactory entityBuilderFactory)
    : BaseAdminController(factory, entityBuilderFactory)
{
    private const int ItemsPerPage = 10;

    public IActionResult List(int page = 1, string filter = null) =>
        CreateView("PlayersPageModel", $"Page:{page};ItemsPerPage:2;Filter:{filter}", "List");

    [HttpGet("create")]
    public IActionResult Create() =>
        CreateView("PlayerCreateEditModel", "", "Create");

    [HttpPost("create")]
    public IActionResult Create(PlayerCreateEditModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        model.Height = 700;

        return UpdateRepository(model, "Player") == CRUDResult.Success
            ? RedirectToAction(nameof(List))
            : View("_Error", "Invalid data for player");
    }

    [Route("{id:guid}")]
    public IActionResult Profile(Guid id) =>
        CreateView("PlayerViewModel", $"PlayerId:{id}", "Profile");

    [HttpGet("update/{id:guid}")]
    public IActionResult Update(Guid id) =>
        CreateView("PlayerCreateEditModel", $"PlayerId:{id}", "Update");

    [HttpPost("update/{id:guid}")]
    public IActionResult Update(PlayerCreateEditModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        UpdateRepository(model, "Player");

        return RedirectToAction(nameof(List));
    }

    [HttpPost("delete")]
    public IActionResult Delete(Guid id)
    {
        DeleteFromRepository(id, "Player");

        return RedirectToAction(nameof(List));
    }
}