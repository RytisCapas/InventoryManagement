﻿using WarehouseInventoryManagement.DataEntities.Entities;

namespace WarehouseInventoryManagement.DataEntities.Mappings
{
    public sealed class AgreementMap : PersistentEntityMapBase<Agreement>
    {
        public AgreementMap()
        {            
            Id(f => f.Id).GeneratedBy.Identity();

            Map(f => f.Number).Length(20).Not.Nullable();

            References(f => f.Customer).LazyLoad().Not.Nullable().Cascade.SaveUpdate();            
        }
    }
}
