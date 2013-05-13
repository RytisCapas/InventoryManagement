using System;

namespace WarehouseInventoryManagement.DataEntities.Entities
{
    public class State : EntityBase<State>
    {
        public virtual string StateName { get; set; }

        public virtual DateTime CreatedOn { get; set; }

        public virtual bool IsDeleted { get; set; }

        public virtual DateTime? DeletedOn { get; set; }

    }
}