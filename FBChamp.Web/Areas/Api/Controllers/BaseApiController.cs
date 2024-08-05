using FBChamp.Core.DALModels;
using FBChamp.Core.UnitOfWork;
using FBChamp.Infrastructure;
using FBChamp.Web.Common.Interfaces;
using FBChamp.Web.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FBChamp.Web.Areas.Api.Controllers;

[Area("Api")]
[ApiController]
public class BaseApiController 
{
    private IUnitOfWork _unitOfWork;
    protected IUnitOfWork UnitOfWork => _unitOfWork;

    public BaseApiController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public BaseApiController() { }
}