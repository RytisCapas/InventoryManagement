﻿using WarehouseInventoryManagement.DataEntities.Entities;

namespace WarehouseInventoryManagement.DataEntities.Mappings
{
    public sealed class ItemLogMap : EntityMapBase<ItemLog>
    {
        public ItemLogMap()
        {
            Table("ItemLog");

            Id(f => f.Id).GeneratedBy.Identity();
            References(f => f.Item).Cascade.None();
            References(f => f.State).Cascade.None();
            Map(f => f.CreatedOn).Not.Nullable().Default("getdate()").Generated.Insert();
            Map(f => f.CreatedBy).Not.Nullable();
            Map(f => f.DeletedBy).Nullable();
            Map(f => f.DeletedOn).Nullable();
        }
    }
}
