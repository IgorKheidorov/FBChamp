﻿using FBChamp.Core.DALModels;
using FBChamp.Core.Entities.Soccer;
using FBChamp.Core.UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FBChamp.Web.Areas.Api.Controllers;

[Route("api/coaches")]
public class CoachController(IUnitOfWork unitOfWork)
    : BaseApiController(unitOfWork)
{
    [HttpGet]
    public ActionResult<List<CoachModel>> Coaches() =>
        UnitOfWork.GetAllCoachModels().ToList();

    [HttpGet]
    [Route("{id}")]
    public ActionResult<CoachModel> GetCoach(Guid id) =>
        UnitOfWork.GetCoachModel(id);

    [HttpDelete]
    [Authorize("Admin")]
    [Route("{id}")]
    public ActionResult<CRUDResult> RemoveCoach(Guid id) =>
        UnitOfWork.Remove(id, typeof(Coach));

    [HttpPut]
    [Authorize("Admin")]
    [Route("{id}")]
    public ActionResult<CRUDResult> UpdateCoach(Guid id, [FromBody] CoachModel coachModel) =>
        UnitOfWork.Commit(coachModel.Coach);
}