using FBChamp.Core.UnitOfWork;
using FBChamp.Web.Areas.Admin.Controllers.Models.Coaches;
using FBChamp.Web.Areas.Admin.Controllers.Models.Players;
using FBChamp.Web.Areas.Admin.Controllers.Models.Teams;
using FBChamp.Web.Common.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FBChamp.Web.Areas.Admin.Controllers;

[Route("admin/teams")]
public class TeamController(
    IViewModelBuildersFactory factory,
    IEntityBuildersFactory entityBuilderFactory)
    : BaseAdminController(factory, entityBuilderFactory)
{
    private const int ItemsPerPage = 10;

    #region Total team info

    public IActionResult List(int page = 1, string filter = null) =>
        CreateView("TeamsPageModel", $"Page:{page};ItemsPerPage:{ItemsPerPage};Filter:{filter}", "List");

    [HttpGet("create")]
    public IActionResult Create() =>
        CreateView("TeamCreateEditModel", "", "Create");

    [HttpGet("profile")]
    public IActionResult Profile(Guid id) =>
        CreateView("TeamCreateEditModel", "", "Profile");

    [HttpPost("create")]
    public IActionResult Create(TeamCreateEditModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        UpdateRepository(model, "Team");

        return RedirectToAction(nameof(List));
    }

    [HttpGet("update/{id:guid}")]
    public IActionResult Update(Guid id, int page = 1, string filter = "") =>
        CreateView("TeamCreateEditModel", $"TeamId:{id}; Page:{page};Filter:{filter};ItemsPerPage:2;Mode:Include;",
            "Update");

    [HttpPost]
    [Route("update/{id:guid}")]
    public IActionResult Update(TeamCreateEditModel model)
    {
        UpdateRepository(model, "Team");

        return RedirectToAction(nameof(List), new { id = model.Id });
    }

    #endregion

    #region Players

    [HttpGet]
    [Route("playerprofile/{id}")]
    public IActionResult PlayerProfile(Guid id) =>
        CreateView("PlayerViewModel", $"PlayerId:{id}", "PlayerProfile");

    [HttpGet]
    [Route("addplayer/{id}")]
    public IActionResult AddPlayer(Guid id) =>
        CreateView("PlayersPageModel", $"TeamId:{id};Mode:Assign", "AddPlayer");

    [HttpGet("assignplayer")]
    public IActionResult AssignPlayer(Guid id, Guid teamid) =>
        CreateView("PlayerAssignModel", $"PlayerId:{id};TeamId:{teamid}", "PlayerAssign");

    [HttpPost]
    [Route("assignplayer")]
    public IActionResult AssignPlayer(PlayerAssignModel model)
    {
        if (!ModelState.IsValid)
        {
            return CreateView("PlayerAssignModel", $"PlayerId:{model.Id};TeamId:{model.TeamId}", "PlayerAssign");
        }

        return UpdateRepository(model, "PlayerAssignmentInfo") == CRUDResult.Success
            ? RedirectToAction(nameof(Update), new { id = model.TeamId })
            : View("_Error", "The playing number is already assigned to other player!");
    }

    [HttpPost]
    [Route("unassignplayer")]
    public IActionResult UnassignPlayer(Guid id)
    {
        DeleteFromRepository(id, "PlayerAssignmentInfo");

        return RedirectToAction(nameof(List));
    }

    #endregion

    #region Coaches

    [HttpGet]
    [Route("addcoach/{id}")]
    public IActionResult AddCoach(Guid id) =>
        CreateView("CoachesPageModel", $"TeamId:{id};Mode:Assign", "AddCoach");

    [HttpGet]
    [Route("coachprofile/{id}")]
    public IActionResult CoachProfile(Guid id) =>
        CreateView("CoachViewModel", $"CoachId:{id}", "CoachProfile");

    [HttpGet("assigncoach")]
    public IActionResult AssignCoach(Guid id, Guid teamid) =>
        CreateView("CoachAssignModel", $"CoachId:{id};TeamId:{teamid}", "CoachAssign");

    [HttpPost]
    [Route("assigncoach")]
    public IActionResult AssignCoach(CoachAssignModel model)
    {
        if (!ModelState.IsValid)
        {
            return CreateView("CoachAssignModel", $"CoachId:{model.Id};TeamId:{model.TeamId}", "CoachAssign");
        }

        return UpdateRepository(model, "CoachAssignmentInfo") == CRUDResult.Success
            ? RedirectToAction(nameof(Update), new { id = model.TeamId })
            : View("_Error", "The coach with role is already assigned to the team!");
    }

    [HttpPost]
    [Route("unassigncoach")]
    public IActionResult UnassignCoach(Guid id)
    {
        DeleteFromRepository(id, "CoachAssignmentInfo");

        return RedirectToAction(nameof(List));
    }

    #endregion
}