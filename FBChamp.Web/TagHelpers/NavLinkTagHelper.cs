using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace FBChamp.Web.TagHelpers;

public class NavLinkTagHelper : AnchorTagHelper
{
    public NavLinkTagHelper(IHtmlGenerator generator) : base(generator)
    {
    }

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        base.Process(context, output);
        output.TagName = "a";
        var routeData = ViewContext.RouteData.Values;
        var currentController = routeData["controller"] as string;
        var currentArea = routeData["area"] as string ?? string.Empty;
        if (Controller == currentController && Area == currentArea)
        {
            output.AddClass("active", HtmlEncoder.Default);
        }
    }
}