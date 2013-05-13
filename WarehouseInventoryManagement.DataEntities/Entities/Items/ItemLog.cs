using System;

namespace WarehouseInventoryManagement.DataEntities.Entities
{
    public class ItemLog : EntityBase<ItemLog>
    {

        public virtual Item Item { get; set; }

        public virtual State State { get; set; }

        public virtual DateTime CreatedOn { get; set; }

        public virtual string CreatedBy { get; set; }

        public virtual DateTime? DeletedOn { get; set; }

        public virtual string DeletedBy { get; set; }
    }
}
