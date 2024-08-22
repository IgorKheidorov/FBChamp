using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace FBChamp.Web.TagHelpers;

public class BackLinkTagHelper(IHtmlGenerator generator)
    : AnchorTagHelper(generator)
{
    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        base.Process(context, output);

        output.TagName = "a";
        output.AddClass("btn", HtmlEncoder.Default);
        output.AddClass("btn-outline-secondary", HtmlEncoder.Default);

        var referer = ViewContext.HttpContext.Request.Headers["Referer"];

        output.Attributes.SetAttribute("href", referer);
    }
}