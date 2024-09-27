using FBChamp.Web.Areas.Admin.Controllers.Models.Leagues;
using FBChamp.Web.Common.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FBChamp.Web.Areas.Admin.Controllers;

[Route("admin/leagues")]
public class LeagueController(
    IViewModelBuildersFactory factory,
    IEntityBuildersFactory entityBuildersFactory) : BaseAdminController(factory, entityBuildersFactory)
{
    private const int ItemsPerPage = 10;

    #region Total League Info

    public IActionResult List(int page = 1, string filter = null) =>
        CreateView("LeaguesPageModel",
            $"Page:{page};ItemsPerPage:{ItemsPerPage};Filter:{filter}",
            "List");

    [HttpGet("create")]
    public IActionResult Create() =>
        CreateView("LeagueCreateEditModel",
            "",
            "Create");

    [HttpPost("create")]
    public IActionResult Create(LegueCreateEditModel model)
    {
        if(!ModelState.IsValid)
        {
            return View(model);
        }

        UpdateRepository(model, "League");
        return RedirectToAction(nameof(List));
    }

    [Route("{id:guid}")]
    public IActionResult Profile(Guid id) =>
      CreateView("LeagueViewModel", $"LeagueId:{id}", "Profile");

    [HttpPost("delete")]
    public IActionResult Delete(Guid id)
    {
        DeleteFromRepository(id, "League");
        return RedirectToAction(nameof(List));
    }

    [HttpGet("update/{id:guid}")]
    public IActionResult Update(Guid id, int page = 1, string filter = "") =>
        CreateView("LeagueCreateEditModel",
            $"LeagueId:{id}; Page:{page};Filter:{filter};ItemsPerPage:2;Mode:Include;",
            "Update");

    [HttpPost]
    [Route("update/{id:guid}")]
    public IActionResult Update(LegueCreateEditModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        UpdateRepository(model, "League");
        return RedirectToAction(nameof(List), new { id = model.Id });
    }

    #endregion
}
