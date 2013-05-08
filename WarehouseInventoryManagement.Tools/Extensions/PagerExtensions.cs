using System.Web.Mvc;
using MvcContrib.Pagination;
using OncologyNutrition.Admin.Common;

namespace WarehouseInventoryManagement.Tools.Extensions
{
    public static class PagerExtensions
    {
        public static PagingHelper Pager(this HtmlHelper helper, IPagination pagination, bool onlyQueryStringInLinks = false)
        {
            return new PagingHelper(pagination, helper.ViewContext.HttpContext.Request, onlyQueryStringInLinks);
        }
    }
}