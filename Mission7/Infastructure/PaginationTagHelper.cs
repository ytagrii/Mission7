using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Mission7.Models.ViewModels;

namespace Mission7.Infastructure
{

    [HtmlTargetElement("div", Attributes = "book-page")]
    public class PagnationTagHelper : TagHelper
    {
        //create page links
        private IUrlHelperFactory uhf;

        public PagnationTagHelper(IUrlHelperFactory temp)
        {
            uhf = temp;
        }


        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext vc {get; set;}

        //This is received from teh html 
        public PageInfo BookPage { get; set; } //this has to be named the same as the Attributes thing up above.
        public string PageAction { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            IUrlHelper uh = uhf.GetUrlHelper(vc);
            TagBuilder final = new TagBuilder("div");
            for (int i = 1; i < BookPage.TotalPages + 1; i++)
            {
                TagBuilder tb = new TagBuilder("a");
                tb.Attributes["href"] = uh.Action(PageAction, new { pageNum = i });
                tb.InnerHtml.Append(i.ToString());
                final.InnerHtml.AppendHtml(tb);
            }

            output.Content.AppendHtml(final.InnerHtml);
        }
    }
}
