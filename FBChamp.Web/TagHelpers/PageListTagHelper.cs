using System.Text.Encodings.Web;
using FBChamp.Common.Paging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace FBChamp.Web.TagHelpers;

public sealed class PageListTagHelper(IUrlHelperFactory helperFactory)
    : TagHelper
{
    [ViewContext]
    [HtmlAttributeNotBound]
    public ViewContext ViewContext { get; set; }

    public PagedListBase Model { get; set; }

    [HtmlAttributeName(DictionaryAttributePrefix = "page-url-")]
    public Dictionary<string, string> PageUrlValues { get; } = [];

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "ul";
        output.AddClass("pagination", HtmlEncoder.Default);

        var urlHelper = helperFactory.GetUrlHelper(ViewContext);
        var back = CreatePageButton("«", Model.Page - 1, urlHelper, Model.CanPrevious);

        output.Content.AppendHtml(back);

        var previousPageIsEllipsis = false;

        for (var pageNumber = 1; pageNumber <= Model.TotalPages; pageNumber++)
        {
            if (pageNumber == Model.Page)
            {
                var pageButton = CreatePageButton(pageNumber.ToString(), pageNumber, urlHelper);
                output.Content.AppendHtml(pageButton);
                previousPageIsEllipsis = false;
            }
            else if (Model.IsNearFromPageOrBoundary(pageNumber))
            {
                var pageButton = CreatePageButton(pageNumber.ToString(), pageNumber, urlHelper);
                output.Content.AppendHtml(pageButton);
                previousPageIsEllipsis = false;
            }
            else if (!previousPageIsEllipsis)
            {
                var pageButton = CreatePageButton("…", pageNumber, urlHelper, false);
                output.Content.AppendHtml(pageButton);
                previousPageIsEllipsis = true;
            }
        }

        var next = CreatePageButton("»", Model.Page + 1, urlHelper, Model.CanNext);
        output.Content.AppendHtml(next);
    }

    private TagBuilder CreatePageButton(string caption, int pageNumber, IUrlHelper urlHelper, bool isActive = true)
    {
        if (Model.TotalPages == 1)
        {
            return null;
        }

        var tag = new TagBuilder("li");
        tag.AddCssClass("page-item");

        var link = new TagBuilder("a");
        link.AddCssClass("page-link");
        link.InnerHtml.Append(caption);

        tag.InnerHtml.AppendHtml(link);

        if (isActive)
        {
            PageUrlValues["page"] = pageNumber.ToString();
            var action = ViewContext.RouteData.Values["action"] as string;
            link.Attributes["href"] = urlHelper.Action(action, PageUrlValues);
        }
        else
        {
            tag.AddCssClass("disabled");
        }

        if (pageNumber == Model.Page)
        {
            tag.AddCssClass("active");
        }

        return tag;
    }
}