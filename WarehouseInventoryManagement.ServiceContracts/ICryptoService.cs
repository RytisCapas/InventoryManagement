using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WarehouseInventoryManagement.ServiceContracts
{
    public interface ICryptoService
    {
        string GetHashedValue(string value, string salt);

        string GetSalt();
    }
}
