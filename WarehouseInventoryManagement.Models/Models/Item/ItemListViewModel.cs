using MvcContrib.Pagination;
using MvcContrib.UI.Grid;

namespace WarehouseInventoryManagement.Models.Models
{
    public class ItemListViewModel
    {
        public IPagination<ItemViewModel> Items { get; set; }

        public GridSortOptions GridSortOptions { get; set; }

        public int Page { get; set; }

        public string ControllerName { get; set; }

        public string ActionName { get; set; }

        public string Search { get; set; }
    }
}
