using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

using MvcContrib.Sorting;
using MvcContrib.UI.Grid;

namespace WarehouseInventoryManagement.Tools
{
    public class EmptyHeaderHtmlTableGridRenderer<T> : HtmlTableGridRenderer<T> where T : class
    {
        private readonly string actionName;

        private readonly string controllerName;

        private readonly IDictionary<string, object> parameters;

        private readonly bool renderTableHeadSection;

        private readonly bool onlyQueryStringInSortLink;

        public EmptyHeaderHtmlTableGridRenderer()
        {
            this.renderTableHeadSection = true;
            this.onlyQueryStringInSortLink = false;
        }

        public EmptyHeaderHtmlTableGridRenderer(bool renderTableHeadSection)
        {
            this.renderTableHeadSection = renderTableHeadSection;
            this.onlyQueryStringInSortLink = false;
        }

        public EmptyHeaderHtmlTableGridRenderer(bool renderTableHeadSection, bool onlyQueryStringInSortLink)
        {
            this.renderTableHeadSection = renderTableHeadSection;
            this.onlyQueryStringInSortLink = onlyQueryStringInSortLink;
        }

        public EmptyHeaderHtmlTableGridRenderer(string controllerName, string actionName)
        {
            this.actionName = actionName;
            this.controllerName = controllerName;
            this.renderTableHeadSection = true;
            this.onlyQueryStringInSortLink = false;
        }

        public EmptyHeaderHtmlTableGridRenderer(string controllerName, string actionName, IDictionary<string, object> parameters) : this(controllerName, actionName)
        {
            this.parameters = parameters;
            this.renderTableHeadSection = true;
            this.onlyQueryStringInSortLink = false;
        }

        public EmptyHeaderHtmlTableGridRenderer(string controllerName, string actionName, IDictionary<string, object> parameters, bool renderTableHeadSection) : this(controllerName, actionName, parameters)
        {
            this.renderTableHeadSection = renderTableHeadSection;
            this.onlyQueryStringInSortLink = false;
        }

        public EmptyHeaderHtmlTableGridRenderer(string controllerName, string actionName, IDictionary<string, object> parameters, bool renderTableHeadSection, bool onlyQueryStringInSortLink) : this(controllerName, actionName, parameters, renderTableHeadSection)
        {
            this.onlyQueryStringInSortLink = onlyQueryStringInSortLink;
        }

        protected override bool RenderHeader()
        {
            if (renderTableHeadSection)
            {
                RenderHeadStart();

                foreach (var column in VisibleColumns())
                {
                    RenderHeaderCellStart(column);
                    RenderHeaderText(column);
                    RenderHeaderCellEnd();
                }

                RenderHeadEnd();
            }

            return !IsDataSourceEmpty();
        }

        protected override void RenderHeaderCellStart(GridColumn<T> column)
        {
            if (IsSortingEnabled && column.Sortable)
            {
                var isSortedByThisColumn = GridModel.SortOptions.Column == GenerateSortColumnName(column);

                var sortClass = GridModel.SortOptions.Direction == SortDirection.Descending && isSortedByThisColumn
                                                                 ? "headerSortUp"
                                                                 : "headerSortDown";
                if (isSortedByThisColumn)
                {
                    column.HeaderAttributes.Add("class", sortClass);
                }
            }

            var attributes = new Dictionary<string, object>(column.HeaderAttributes);

            var attrs = BuildHtmlAttributes(attributes);

            if (attrs.Length > 0)
            {
                attrs = " " + attrs;
            }

            RenderText(string.Format("<th{0}>", attrs));
        }

        protected override void RenderHeaderText(GridColumn<T> column)
        {
            if (IsSortingEnabled && column.Sortable && !IsDataSourceEmpty())
            {
                var isSortedByThisColumn = GridModel.SortOptions.Column == GenerateSortColumnName(column);

                var linkAttributesdictionary = new Dictionary<string, object>
                    {
                        { "class", "sort-arrow" }
                    };

                var displayName = column.DisplayName;

                var sortColumnName = GenerateSortColumnName(column);

                var sortOptions = new GridSortOptions
                {
                    Column = sortColumnName
                };

                sortOptions.Direction = (GridModel.SortOptions.Direction == SortDirection.Ascending && isSortedByThisColumn)
                                                                          ? SortDirection.Descending
                                                                          : SortDirection.Ascending;

                var routeValues = CreateRouteValues(sortOptions, GridModel.SortPrefix);

                var link = GenerateLink(displayName, routeValues, linkAttributesdictionary);

                link = HttpUtility.HtmlDecode(link);

                if (onlyQueryStringInSortLink)
                {
                    link = LeaveOnlyQueryStringInLink(link);
                }

                RenderText(link);
            }
            else
            {
                RenderText(column.DisplayName);
            }
        }

        protected virtual string GenerateLink(string displayName, RouteValueDictionary routeValues, IDictionary<string, object> htmlAttributes)
        {
            var link = HtmlHelper.GenerateLink(
                Context.RequestContext,
                RouteTable.Routes,
                displayName,
                null,
                actionName,
                controllerName,
                routeValues,
                htmlAttributes);

            return link;
        }

        protected override void RenderEmpty()
        {
            var columnsCount = GridModel.Columns.Count;

            RenderBodyStart();
            WriteEmptyText(Writer, columnsCount);
            RenderBodyEnd();
        }

        protected void WriteEmptyText(TextWriter writer, int columnsCount)
        {
            var colspan = string.Empty;

            if (columnsCount > 1)
            {
                colspan = "colspan=\"" + columnsCount + "\"";
            }

            var emptyText = GridModel.EmptyText;

            writer.Write("<tr><td class=\"empty-text\" " + colspan + ">" + emptyText + "</td></tr>");
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

        private RouteValueDictionary CreateRouteValues(GridSortOptions sortOptions, string prefix)
        {
            RouteValueDictionary routeValues;

            if (string.IsNullOrEmpty(prefix))
            {
                routeValues = new RouteValueDictionary(sortOptions);
            }
            else
            {
                routeValues = new RouteValueDictionary(new Dictionary<string, object>
                                                           {
                                                               { prefix + "." + "Column", sortOptions.Column },
                                                               { prefix + "." + "Direction", sortOptions.Direction }
                                                           });
            }

            if (parameters != null)
            {
                foreach (var parameter in parameters)
                {
                    routeValues.Add(parameter.Key, parameter.Value);
                }
            }

            var requestParams = Context.RequestContext.HttpContext.Request.Params;

            foreach (var param in new[] { "search", "itemsPerPage", "SearchText", "StartDate", "EndDate", "OrderStatus" })
            {
                if (requestParams[param] != null)
                {
                    routeValues.Add(param, requestParams[param]);
                }
            }

            return routeValues;
        }
    }
}