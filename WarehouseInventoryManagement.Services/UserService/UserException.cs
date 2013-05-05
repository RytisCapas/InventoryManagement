using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System;

namespace WarehouseInventoryManagement.Services
{
    public class UserException : ServiceException
    {
        public UserException(string message)
            : base(message)
        {
        }

        public UserException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}

