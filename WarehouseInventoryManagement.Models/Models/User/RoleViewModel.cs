using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WarehouseInventoryManagement.Models.Models.User
{
    public class RoleViewModel
    {
        public int Id { get; set; }

        public virtual string RoleName { get; set; }

        public virtual bool IsDeleted { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
