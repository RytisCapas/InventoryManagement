using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WarehouseInventoryManagement.Common.Membership
{
    public class MembershipUserBlockedException : Exception
    {
        public MembershipUserBlockedException()
        {
        }

        public MembershipUserBlockedException(string message)
            : base(message)
        {
        }

        public MembershipUserBlockedException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
