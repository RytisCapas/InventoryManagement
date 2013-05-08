using System;

namespace WarehouseInventoryManagement.DataEntities.Entities
{
    public class Role : EntityBase<Role>
    {
        public virtual string RoleName { get; set; }

        public virtual DateTime CreatedOn { get; set; }

        public virtual bool IsDeleted { get; set; }

        public virtual DateTime? DeletedOn { get; set; }

    }
}