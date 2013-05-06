using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WarehouseInventoryManagement.Web.Helpers
{
    public static class IsActiveExtensions
    {
        public static MvcHtmlString IsActive(this HtmlHelper helper, ActionResult action)
        {
            RouteValueDictionary dic = action.GetRouteValueDictionary();
            var actionName = dic["Action"].ToString();
            var controllerName = dic["Controller"].ToString();
            var currentAction = helper.ViewContext.RouteData.Values["Action"].ToString();
            var currentController = helper.ViewContext.RouteData.Values["Controller"].ToString();
            var rslt = string.Equals(actionName, currentAction, StringComparison.OrdinalIgnoreCase) && string.Equals(controllerName, currentController, StringComparison.OrdinalIgnoreCase);

            return rslt ? new MvcHtmlString(@" class=""active "" ") : new MvcHtmlString(string.Empty);
        }

        public static MvcHtmlString IsActive(this HtmlHelper helper, ActionResult[] actions)
        {
            var found = false;

            foreach (var action in actions)
            {
                RouteValueDictionary dic = action.GetRouteValueDictionary();
                var actionName = dic["Action"].ToString();
                var controllerName = dic["Controller"].ToString();
                var currentAction = helper.ViewContext.RouteData.Values["Action"].ToString();
                var currentController = helper.ViewContext.RouteData.Values["Controller"].ToString();
                var rslt = string.Compare(actionName, currentAction, StringComparison.OrdinalIgnoreCase) == 0
                    && string.Compare(controllerName, currentController, StringComparison.OrdinalIgnoreCase) == 0;

                if (rslt)
                {
                    found = true;
                    break;
                }
            }

            return found ? new MvcHtmlString(" class=active ") : new MvcHtmlString(string.Empty);
        }

        public static MvcHtmlString IsActive(this HtmlHelper helper, string link)
        {
            var url = HttpContext.Current.Request.Url.AbsoluteUri.ToString();
            var rslt = url.ToLower().Contains(link.ToLower());
            return rslt ? new MvcHtmlString(@" class=""active"" ") : new MvcHtmlString(string.Empty);
        }
    }
}