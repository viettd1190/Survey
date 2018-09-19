using System.Text;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace Survey.WebApp.Helpers.Html
{
    public static class PagingExtension
    {
        public static MvcHtmlString Paging(this HtmlHelper helper,
                                           int page,
                                           int lastPage,
                                           string action,
                                           string controller)
        {
            var pagingTag = new TagBuilder("paging");

            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("<ul class=class=\"pagination pagination-primary\">");
            bool firstFlag = page <= 4;
            bool lastFlag = lastPage-page>=4;
            for (int i = 1; i <= lastPage; i++)
            {
                var className = "page-item";
                if (page == i)
                {
                    className = "active page-item";
                }

                if(i==1)
                {
                    builder.AppendFormat($"<li class=\"{className}\">{helper.ActionLink(i.ToString(), action, controller, new { @class = "page-link" })}</li>");
                }
                else
                {
                    if(i<page)
                    {
                        if (firstFlag)
                        {
                            builder.AppendFormat($"<li class=\"{className}\">{helper.ActionLink(i.ToString(), action, controller, new { @class = "page-link" })}</li>");
                        }
                        else
                        {
                            if(i+3==page)
                            {
                                firstFlag = true;
                                builder.AppendFormat($"<li class=\"{className}\">{helper.ActionLink("...", string.Empty, string.Empty, new { @class = "page-link" })}</li>");
                            }
                        }
                    }
                    else if(i==page)
                    {
                        builder.AppendFormat($"<li class=\"{className}\">{helper.ActionLink(i.ToString(), action, controller, new { @class = "page-link" })}</li>");
                    }
                    else
                    {
                        if(i==lastPage)
                        {
                            builder.AppendFormat($"<li class=\"{className}\">{helper.ActionLink(i.ToString(), action, controller, new { @class = "page-link" })}</li>");
                        }
                        else
                        {
                            if(!lastFlag)
                            {
                                builder.AppendFormat($"<li class=\"{className}\">{helper.ActionLink(i.ToString(), action, controller, new { @class = "page-link" })}</li>");
                            }
                        }
                    }
                }
                

                if (firstFlag)
                {
                    builder.AppendFormat($"<li class=\"{className}\">{helper.ActionLink(i.ToString(), action, controller, new { @class = "page-link" })}</li>");
                }
                else
                {

                }
                
            }
            builder.AppendFormat("</ul>");

            pagingTag.InnerHtml = "<ul class=\"pagination pagination-primary\"><li class=\"page-item\"><a href=\"\" class=\"page-link\">1</a></li><li class=\"page-item\"><a href=\"\" class=\"page-link\">...</a></li><li class=\"page-item\"><a href=\"\" class=\"page-link\">5</a></li><li class=\"page-item\"><a href=\"\" class=\"page-link\">6</a></li><li class=\"active page-item\"><a href=\"\" class=\"page-link\">7</a></li><li class=\"page-item\"><a href=\"\" class=\"page-link\">8</a></li><li class=\"page-item\"><a href=\"\" class=\"page-link\">9</a></li><li class=\"page-item\"><a href=\"\" class=\"page-link\">...</a></li><li class=\"page-item\"><a href=\"\" class=\"page-link\">12</a></li></ul>";

            return MvcHtmlString.Create(pagingTag.ToString(TagRenderMode.Normal));
        }

        //public static MvcHtmlString Button(this HtmlHelper helper, string id, string name)
        //{
        //    return helper.Button(id, name, null);
        //}

        //public static MvcHtmlString Button(this HtmlHelper helper, string id,
        //                                   string name, IDictionary<string, object> attributes)
        //{
        //    var buttonTag = new TagBuilder("button");
        //    buttonTag.Attributes.Add("type", "button");
        //    buttonTag.Attributes.Add("id", id);
        //    buttonTag.Attributes.Add("name", name);
        //    buttonTag.MergeAttributes(attributes);

        //    return MvcHtmlString.Create(buttonTag.ToString(TagRenderMode.Normal));
        //}
    }
}
