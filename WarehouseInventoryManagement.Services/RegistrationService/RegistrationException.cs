using System;

namespace WarehouseInventoryManagement.Services
{
    public class RegistrationException : ServiceException
    {
        public RegistrationException(string message) : base(message)
        {
        }

        public RegistrationException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
