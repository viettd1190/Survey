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
                                           string controllerName)
        {
            TagBuilder pagingTag = new TagBuilder("paging");

            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("<ul class=\"pagination pagination-primary\">");

            for (int i = 1; i <= lastPage; i++)
            {
                string className = "page-item";
                if(page == i)
                {
                    className = "active page-item";
                }

                if(i == 1)
                {
                    builder.AppendFormat($"<li class=\"{className}\">{helper.ActionLink(i.ToString(), action, controllerName, new { page = i }, new { @class = "page-link" })}</li>");
                }
                else if(i < page)
                {
                    if(i + 3 == page)
                    {
                        builder.AppendFormat($"<li class=\"{className}\">{helper.ActionLink("...", string.Empty, string.Empty, new { page = i }, new { @class = "page-link" })}</li>");
                    }
                    else if(i + 3 > page)
                    {
                        builder.AppendFormat($"<li class=\"{className}\">{helper.ActionLink(i.ToString(), action, controllerName, new { page = i }, new { @class = "page-link" })}</li>");
                    }
                }
                else if(i == page)
                {
                    builder.AppendFormat($"<li class=\"{className}\">{helper.ActionLink(i.ToString(), action, controllerName, new { page = i }, new { @class = "page-link" })}</li>");
                }
                else if(i > page)
                {
                    if(i - 3 < page)
                    {
                        builder.AppendFormat($"<li class=\"{className}\">{helper.ActionLink(i.ToString(), action, controllerName, new { page = i }, new { @class = "page-link" })}</li>");
                    }
                    else if(i - 3 == page)
                    {
                        builder.AppendFormat($"<li class=\"{className}\">{helper.ActionLink("...", string.Empty, string.Empty, new { page = i }, new { @class = "page-link" })}</li>");
                    }
                }
            }

            if(page + 2 < lastPage)
            {
                builder.AppendFormat($"<li class=\"page-item\">{helper.ActionLink(lastPage.ToString(), action, controllerName, new { page = lastPage }, new { @class = "page-link" })}</li>");
            }

            builder.AppendFormat("</ul>");

            pagingTag.InnerHtml = builder.ToString();

            return MvcHtmlString.Create(pagingTag.ToString(TagRenderMode.Normal));
        }
    }
}
