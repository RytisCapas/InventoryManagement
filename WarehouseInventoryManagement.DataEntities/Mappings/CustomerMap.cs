﻿using WarehouseInventoryManagement.DataEntities.Entities;

namespace WarehouseInventoryManagement.DataEntities.Mappings
{
    public sealed class CustomerMap :  PersistentEntityMapBase<Customer>
    {
        public CustomerMap()
        {
            Id(f => f.Id).GeneratedBy.Identity();

            Map(f => f.Name).Length(50).Not.Nullable();
            Map(f => f.Code).Length(10).Not.Nullable();
            Map(f => f.Type).Not.Nullable();

            HasMany(f => f.Agreements).LazyLoad().Inverse().Cascade.SaveUpdate();            
        }
    }
}
