using FBChamp.Core.UnitOfWork;
using FBChamp.Infrastructure;
using FBChamp.Web.Common.EntityBuilders;
using FBChamp.Web.Common.Interfaces;
using FBChamp.Web.Common.VewModelsBuilders;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder();
builder.Services.AddSingleton<IUnitOfWork, UnitOfWork>();
builder.Services.AddSingleton<IViewModelBuildersFactory, ViewModelBuldersFactory>();
builder.Services.AddSingleton<IEntityBuildersFactory, EntityBuildersFactory>();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
{
    options.LoginPath = "/login";
});

builder.Services.AddAuthorization(options => { options.AddPolicy("Admin", policy => policy.RequireRole("Admin")); });
builder.Services.Configure<RouteOptions>(options => options.LowercaseUrls = true);
builder.Services.AddControllersWithViews();

var app = builder.Build();
app.UseExceptionHandler("/error");
app.UseStatusCodePagesWithRedirects("/error/{0}");
app.UseStaticFiles();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();