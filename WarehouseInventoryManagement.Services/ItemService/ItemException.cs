using System;

namespace WarehouseInventoryManagement.Services
{
    public class ItemException : ServiceException
    {
        public ItemException(string message)
            : base(message)
        {
        }

        public ItemException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}

