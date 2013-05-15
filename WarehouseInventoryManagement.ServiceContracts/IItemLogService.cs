using System;
using System.Collections.Generic;
using WarehouseInventoryManagement.DataEntities.Entities;
using WarehouseInventoryManagement.Models.Models;

namespace WarehouseInventoryManagement.ServiceContracts
{
    public interface IItemLogService
    {
        List<ItemLog> GetItemLogsByItemId(Guid id, bool deleted = false);

        List<ItemLog> GetItemLogs(DateFilter filter); 

        void DeleteItemLogs(Guid id);

        void Save(Item item);

    }
}
