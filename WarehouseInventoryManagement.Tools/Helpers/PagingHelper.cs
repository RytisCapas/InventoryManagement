using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using MvcContrib.Pagination;

using WarehouseInventoryManagement.Tools.Helpers;

namespace OncologyNutrition.Admin.Common
{
    public class PagingHelper : PagerBase
    {
        private const int TotalPagingLinks = 10;

        private const int ActivePagePosition = 5;
        
        private const string PaginationNextCssClass = "pager-forward ui-link";

        private const string PaginationPrevCssClass = "pager-back ui-link";

        private const string PaginationFirst = "1";

        private const string PaginationLast = "last »";

        private const string PaginationEllipsis = "&nbsp;&nbsp;...&nbsp;&nbsp;";

        private readonly bool onlyQueryStringInLinks;

        private string paginationNext = "kitas »";

        private string paginationPrev = "« buvęs";

        private string paginationHash = string.Empty;

        public PagingHelper(IPagination pagination, HttpRequestBase request, bool onlyQueryStringInLinks = false) : base(pagination, request)
        {
            this.onlyQueryStringInLinks = onlyQueryStringInLinks;
        }

        public PagingHelper Link(Func<int, string, string> builder)
        {
            if (builder != null)
            {
                UrlBuilder = builder;
            }

            return this;
        }

        public PagingHelper Next(string next)
        {
            paginationNext = next;
            return this;
        }

        public PagingHelper Previous(string previous)
        {
            this.paginationPrev = previous;
            return this;
        }

        public PagingHelper Hash(string hash)
        {
            paginationHash = hash;
            return this;
        }

        public override void RenderPager(StringBuilder builder, int maxResults)
        {
            if (Pagination.TotalPages <= 1)
            {
                return;
            }

            var totalPages = Pagination.TotalPages;

            if (maxResults > 0 && Pagination.TotalItems > maxResults)
            {
                totalPages = maxResults % Pagination.PageSize == 0 ? maxResults / Pagination.PageSize : (maxResults / Pagination.PageSize) + 1;
            }

            var pageNumber = Pagination.PageNumber;
            var pagingLowerBound = pageNumber - ActivePagePosition;

            if (pagingLowerBound < 1)
            {
                pagingLowerBound = 1;
            }

            var pagingUpperBound = pagingLowerBound + TotalPagingLinks;

            if (pagingUpperBound > totalPages)
            {
                pagingUpperBound = totalPages;
            }

            if (pagingUpperBound - pagingLowerBound < TotalPagingLinks)
            {
                pagingLowerBound = pagingUpperBound - TotalPagingLinks;
                if (pagingLowerBound < 1)
                {
                    pagingLowerBound = 1;
                }
            }

            // First page link
            if (pageNumber > 1 && pagingLowerBound > 1)
            {
                builder.Append(CreatePageLink(1, PaginationFirst, paginationHash));
            }

            // Previous page link:
            if (pageNumber > 1)
            {
                builder.Append(CreatePageLink(pageNumber - 1, this.paginationPrev, paginationHash, PaginationPrevCssClass));
            }

            if (pagingLowerBound - 1 > 1)
            {
                builder.Append(PaginationEllipsis);
            }

            // Pages numbers:
            for (var i = pagingLowerBound; i <= pagingUpperBound; i++)
            {
                if (i == pageNumber)
                {
                    // Current page:
                    builder.Append(CreatePageLinkWithClass(i, paginationHash, "active-pager"));
                }
                else
                {
                    builder.Append(CreatePageLink(i, paginationHash));
                }
            }

            if (pageNumber < totalPages && pagingUpperBound + 1 < totalPages)
            {
                builder.Append(PaginationEllipsis);
            }

            // Next page link:
            if (pageNumber < totalPages)
            {
                builder.Append(CreatePageLink(pageNumber + 1, paginationNext, paginationHash, PaginationNextCssClass));
            }

            // Last page link
            if (pageNumber < totalPages && pagingUpperBound < totalPages)
            {
                builder.Append(CreatePageLink(totalPages, totalPages.ToString(), paginationHash));
            }
        }

        protected override string CreatePageLink(int pageNumber, string text, string hash, string className = null)
        {
            if (AjaxOptions != null)
            {
                return CreateAjaxPageLink(pageNumber, text, hash, className);
            }

            className = (className != null) ? string.Format(@" class=""{0}""", className) : string.Empty;

            var link = string.Format(@"<a href=""{0}""{1}>{2}</a>", UrlBuilder(pageNumber, hash), className, text);

            if (onlyQueryStringInLinks)
            {
                link = LeaveOnlyQueryStringInLink(link);
            }

            return link;
        }

        private static string LeaveOnlyQueryStringInLink(string link)
        {
            var href = string.Empty;

            var queryString = string.Empty;

            if (link != null && link.Contains("?"))
            {
                var regexLink = new Regex("(?<=s*?href=(?:'|\"))[^'\"]*?(?=(?:'|\"))");

                foreach (var match in regexLink.Matches(link))
                {
                    href = match.ToString();
                }

                if (!string.IsNullOrEmpty(href))
                {
                    var parts = href.Split('?');

                    if (parts.Length > 1)
                    {
                        queryString = "?" + parts[1];
                    }

                    link = link.Replace(href, queryString);
                }
            }

            return link;
        }
    }
}