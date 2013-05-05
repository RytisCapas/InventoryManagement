using System;
using WarehouseInventoryManagement.DataContracts.Exceptions;

namespace WarehouseInventoryManagement.Services
{
    public class ServiceException : KnownException
    {
        public ServiceException(string message) : base(message)
        {
        }

        public ServiceException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
