using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WarehouseInventoryManagement.Common.Membership
{
    public class MembershipUserNotExistsException : Exception
    {
        public MembershipUserNotExistsException(string message)
            : base(message)
        {
        }

        public MembershipUserNotExistsException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
