using FBChamp.Core.UnitOfWork;
using Microsoft.AspNetCore.Mvc;

namespace FBChamp.Web.Areas.Api.Controllers;

[Area("Api")]
[ApiController]
public class BaseApiController
{
    protected IUnitOfWork UnitOfWork { get; }

    public BaseApiController()
    {
    }

    public BaseApiController(IUnitOfWork unitOfWork)
    {
        UnitOfWork = unitOfWork;
    }
}