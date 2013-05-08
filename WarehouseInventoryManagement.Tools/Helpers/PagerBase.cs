using System;
using System.Globalization;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

using MvcContrib.Pagination;

namespace WarehouseInventoryManagement.Tools.Helpers
{
    public abstract class PagerBase
    {
        public const string DefaultItemsPerPageQueryName = "itemsPerPage";

        public const string DefaultPageQueryName = "page";

        private readonly HttpRequestBase request;

        private readonly IPagination pagination;

        private string url;

        protected PagerBase(IPagination pagination, HttpRequestBase request)
        {
            this.pagination = pagination;
            this.request = request;

            UrlBuilder = CreateDefaultUrl;
        }

        public RouteValueDictionary AjaxOptions { get; set; }

        public virtual string PageQueryName
        {
            get
            {
                return DefaultPageQueryName;
            }
        }

        public virtual string ItemsPerPageQueryName
        {
            get
            {
                return DefaultItemsPerPageQueryName;
            }
        }

        public Func<int, string, string> UrlBuilder { get; set; }

        public IPagination Pagination
        {
            get { return pagination; }
        }

        public HttpRequestBase RequestBase
        {
            get
            {
                return request;
            }
        }

        public string ClassName { get; set; }

        public PagerBase Class(string className)
        {
            ClassName = className;
            return this;
        }

        public PagerBase Url(string url)
        {
            this.url = url;
            return this;
        }

        public PagerBase Url(string url, RouteValueDictionary ajaxOptions = null)
        {
            this.url = url;
            this.AjaxOptions = ajaxOptions;

            return this;
        }

        public virtual string CreateQueryString()
        {
            var values = request.QueryString;

            if (values == null || values.Count == 0)
            {
                values = request.Form;
            }

            var builder = new StringBuilder();

            foreach (string str in values.Keys)
            {
                if (str != PageQueryName)
                {
                    var parameters = values.GetValues(str);
                    if (parameters != null)
                    {
                        foreach (string str2 in parameters)
                        {
                            string parameter = string.Format("&amp;{0}={1}", str, HttpUtility.HtmlEncode(str2));
                            if (!builder.ToString().Contains(parameter))
                            {
                                builder.Append(parameter);
                            }
                        }
                    }
                }
            }

            return builder.ToString();
        }

        public abstract void RenderPager(StringBuilder builder, int maxResults);

        public virtual string ToString(int maxResults)
        {
            if (Pagination.TotalItems == 0)
            {
                return string.Empty;
            }

            var builder = new TagBuilder("div");

            builder.AddCssClass("pager clearfix");

            var stringBuilder = new StringBuilder();

            if (Pagination.TotalPages > 1)
            {
                RenderPager(stringBuilder, maxResults);
            }

            builder.InnerHtml += stringBuilder.ToString();

            return string.Format("{0}", builder);
        }

        public override string ToString()
        {
            return ToString(0);
        }

        protected virtual string CreateDefaultUrl(int pageNumber, string hash)
        {
            string str = CreateQueryString();
            string filePath = url ?? request.FilePath;
            return string.Format("{0}?{1}={2}{3}{4}", new object[] { filePath, PageQueryName, pageNumber, str, string.IsNullOrWhiteSpace(hash) ? string.Empty : '#' + hash });
        }

        protected virtual string CreatePageLink(int pageNumber, string text, string hash, string className = null)
        {
            className = (className != null) ? string.Format(@" class=""{0}""", className) : string.Empty;

            return string.Format(@"<a href=""{0}""{1}>{2}</a>&nbsp;", UrlBuilder(pageNumber, hash), className, text);
        }

        protected virtual string CreatePageLink(int pageNumber, string hash)
        {
            return CreatePageLinkWithClass(pageNumber, hash, "ui-link");
        }

        protected virtual string CreatePageLinkWithClass(int pageNumber, string hash, string className)
        {
            if (AjaxOptions != null)
            {
                return CreateAjaxPageLink(pageNumber, pageNumber.ToString(CultureInfo.InvariantCulture), hash, className);
            }

            return CreatePageLink(pageNumber, pageNumber.ToString(CultureInfo.InvariantCulture), hash, className);
        }

        protected virtual string CreateAjaxPageLink(int pageNumber, string text, string hash, string className = null)
        {
            var tagBuilder = new TagBuilder("a");

            tagBuilder.InnerHtml = text;
            tagBuilder.MergeAttribute("href", UrlBuilder(pageNumber, hash));
            tagBuilder.MergeAttribute("class", className);
            tagBuilder.MergeAttributes(AjaxOptions);

            return MvcHtmlString.Create(tagBuilder.ToString(TagRenderMode.Normal)).ToHtmlString();
        }
    }
}