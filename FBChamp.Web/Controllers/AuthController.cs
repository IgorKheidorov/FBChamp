using System.Security.Claims;
using FBChamp.Web.Common.Interfaces;
using FBChamp.Web.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FBChamp.Web.Controllers;

[Route("[action]")]
public class AuthController(IViewModelBuildersFactory factory)
    : BaseController(factory)
{
    [AllowAnonymous]
    public IActionResult Login(string returnUrl)
    {
        ViewBag.ReturnUrl = returnUrl;

        return View();
    }

    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> Login(LoginModel model, string returnUrl = null)
    {
        ViewBag.ReturnUrl = returnUrl;

        if (!ModelState.IsValid)
        {
            return View();
        }

        var claims = new List<Claim>();

        var role = model.Email.ToLower().Contains("admin") ? "Admin" : "User";

        claims.Add(new Claim(ClaimTypes.Role, role));
        claims.Add(new Claim(ClaimTypes.Email, model.Email));
        //claims.Add(new Claim(ClaimTypes.Name, "Admin"));

        var principal = new ClaimsPrincipal(new ClaimsIdentity(claims, "FBChampType"));

        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal,
            new AuthenticationProperties { AllowRefresh = true });

        return string.IsNullOrEmpty(returnUrl) ? RedirectToAction("Welcome", "Welcome") : Redirect(returnUrl);
    }

    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

        return RedirectToAction(nameof(Login));
    }
}