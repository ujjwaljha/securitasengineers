using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SecuritasEngineering.Web.Utility
{
    public static class ImageHelper
    {
        public static MvcHtmlString Image(this HtmlHelper helper, string src, string altText = "Image missing")
        {
            var builder = new TagBuilder("img");
            builder.MergeAttribute("src", src);
            builder.MergeAttribute("alt", altText);


            return MvcHtmlString.Create(builder.ToString(TagRenderMode.SelfClosing));
        }
    }
}