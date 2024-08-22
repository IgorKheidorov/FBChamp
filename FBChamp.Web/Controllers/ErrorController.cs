using Microsoft.AspNetCore.Mvc;

namespace FBChamp.Web.Controllers;

public class ErrorController : Controller
{
    [Route("[action]/{code?}")]
    public IActionResult ShowError(string message) =>
        message switch
        {
            "400" => View("Error400"),
            "404" => View("Error404"),
            "500" => View("Error500"),
            _ => View("Error", message)
        };
}