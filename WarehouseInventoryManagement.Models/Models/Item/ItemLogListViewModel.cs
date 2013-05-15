using System.Collections.Generic;
using WarehouseInventoryManagement.Models.Models.Item;

namespace WarehouseInventoryManagement.Models.Models
{
    public class ItemLogListViewModel
    {
        public List<ItemLogViewModel> Logs { get; set; }

        public DateFilter DateFilter { get; set; }
    }
}
