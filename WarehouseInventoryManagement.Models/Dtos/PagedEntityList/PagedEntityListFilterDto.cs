using System.Configuration;

namespace WarehouseInventoryManagement.Models.Dtos
{
    public class PagedEntityListFilterDto
    {
        private const string DefaultItemsPerPageConfiguration = "DefaultItemsPerPage";

        private const int DefaultItemsPerPageValue = 10;

        private static int itemsPerPage;

        public static int DefaultItemsPerPage
        {
            get
            {
                if (itemsPerPage == 0)
                {
                    var configValueStr = ConfigurationManager.AppSettings.Get(DefaultItemsPerPageConfiguration);

                    if (!int.TryParse(configValueStr, out itemsPerPage) || itemsPerPage <= 0)
                    {
                        itemsPerPage = DefaultItemsPerPageValue;
                    }
                }

                return itemsPerPage;
            }
        }

        /// <summary>
        /// Gets or sets the start page.
        /// </summary>
        public int StartPage { get; set; }

        /// <summary>
        /// Gets or sets the items per page.
        /// </summary>
        public int ItemsPerPage { get; set; }

        /// <summary>
        /// Gets or sets sorting column.
        /// </summary>
        public string Column { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether sorting should be ascending or descending.
        /// </summary>
        public bool AscendingOrder { get; set; }

        /// <summary>
        /// Gets or sets the search text.
        /// </summary>
        public string SearchText { get; set; }
    }
}
