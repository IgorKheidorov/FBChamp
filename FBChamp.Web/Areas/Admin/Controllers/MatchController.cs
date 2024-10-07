using FBChamp.Web.Areas.Admin.Controllers.Models.Matches;
using FBChamp.Web.Common.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FBChamp.Web.Areas.Admin.Controllers;

[Route("admin/matches")]
public class MatchController(
    IViewModelBuildersFactory factory,
    IEntityBuildersFactory entityBuildersFactory) : BaseAdminController(factory, entityBuildersFactory)
{
    private const int ItemsPerPage = 10;

    public IActionResult List(int page = 1, string filter = null) =>
        CreateView("MatchesPageModel",
            $"Page:{page};ItemsPerPage:{ItemsPerPage};Filter:{filter}",
            "List");

    [HttpGet("create")]
    public IActionResult Create() =>
        CreateView("MatchCreateEditModel",
            "",
            "Create");

    [HttpPost("create")]
    public IActionResult Create(MatchCreateEditModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        UpdateRepository(model, "Match");
        return RedirectToAction(nameof(List));
    }

    [Route("{id:guid}")]
    public IActionResult Profile(Guid id) =>
        CreateView("MatchViewModel", $"MatchId:{id}", "Profile");

    //[HttpDelete("delete")]
    //public IActionResult Delete(Guid id)
    //{
    //    DeleteFromRepository(id, "Match");
    //    return RedirectToAction(nameof(List));
    //}

    [HttpGet("update/{id:guid}")]
    public IActionResult Update(Guid id, int page = 1, string filter = "") =>
        CreateView("MatchCreateEditModel",
            $"MatchId:{id}; Page:{page};Filter:{filter};ItemsPerPage:2;Mode:Include",
            "Update");

    [HttpPost]
    [Route("update/{id:guid}")]
    public IActionResult Update(MatchCreateEditModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        UpdateRepository(model, "Match");
        return RedirectToAction(nameof(List), new { id = model.Id });
    }
}