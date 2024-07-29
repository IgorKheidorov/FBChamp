using FBChamp.Core.DALModels;
using FBChamp.Core.Repositories;
using FBChamp.Infrastructure;
using FBChamp.Web.Common.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FBChamp.Web.Areas.Api.Controllers;

[Route("api/coaches")]
[AllowAnonymous]
public class CoachController : BaseApiController
{
    private IUnitOfWork _unitOfWork;
    public CoachController(IUnitOfWork unitOfWork, IViewModelBuildersFactory factory, IEntityBuildersFactory entityBuilderFactory) : base(factory, entityBuilderFactory)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    public ActionResult<List<CoachModel>> Coaches()=>    
        _unitOfWork.GetAllCoachModels().ToList();

}
