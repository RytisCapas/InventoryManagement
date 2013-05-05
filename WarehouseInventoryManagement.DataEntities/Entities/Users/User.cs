using System;
using System.Collections.Generic;

namespace WarehouseInventoryManagement.DataEntities.Entities
{
    public class User : PersistentEntityBase<User>
    {
        public virtual string UserName { get; set; }

        public virtual string FirstName { get; set; }
        
        public virtual string LastName { get; set; }

        public virtual string Password { get; set; }

        public virtual string PasswordSalt { get; set; }

        public virtual string StreetRow { get; set; }

        public virtual string City { get; set; }

        public virtual string Country { get; set; }

        public virtual string Phone { get; set; }

        public virtual string Email { get; set; }

        public virtual DateTime? LastLogin { get; set; }

        public virtual IList<Role> Roles { get; set; }

    }
}
