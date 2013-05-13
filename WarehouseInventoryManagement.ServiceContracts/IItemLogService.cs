using System;
using System.Collections.Generic;
using WarehouseInventoryManagement.DataEntities.Entities;

namespace WarehouseInventoryManagement.ServiceContracts
{
    public interface IItemLogService
    {
        List<ItemLog> GetItemLogsByItemId(Guid id, bool deleted = false);

        void DeleteItemLogs(Guid id);

        void Save(Item item);

    }
}
