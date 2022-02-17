using Books.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Books.Infastructure
{
    [HtmlTargetElement("div", Attributes = "page-blah")]
    public class PaginationTagHelper : TagHelper
    {
        //Dynamically create page links

        private IUrlHelperFactory uhf { get; set; }

        public PaginationTagHelper(IUrlHelperFactory x) {
            uhf = x;
        }

        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext Vc { get; set; }


        //Is linked to attribute page-blah
        public PageInfo PageBlah { get; set; }
        public string PageAction { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            IUrlHelper uh = uhf.GetUrlHelper(Vc);
            TagBuilder final = new TagBuilder("div");

            for(int i = 1; i < PageBlah.TotalPages+1; i++)
            {
                TagBuilder tb = new TagBuilder("a");
                tb.Attributes["href"] = uh.Action(PageAction, new {PageNum = i});
                tb.InnerHtml.Append(i.ToString());

                final.InnerHtml.AppendHtml(tb);
            }
            output.Content.AppendHtml(final.InnerHtml);
        }

    }
}
