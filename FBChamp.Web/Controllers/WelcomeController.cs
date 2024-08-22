using Microsoft.AspNetCore.Mvc;

namespace FBChamp.Web.Controllers;

public class WelcomeController : BaseController
{
    [Route("")]
    public IActionResult Welcome() => View();
}