using System;
using System.Security.Cryptography;
using System.Text;
using WarehouseInventoryManagement.ServiceContracts;

namespace WarehouseInventoryManagement.Services
{
    public class CryptoService : ICryptoService
    {

        public string GetHashedValue(string value, string salt)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentNullException("value");
            }

            var valueBytes = Encoding.UTF8.GetBytes(value);

            var saltBytes = Encoding.UTF8.GetBytes(salt);

            var hmacMd5 = new HMACMD5(saltBytes);

            var saltedHash = hmacMd5.ComputeHash(valueBytes);

            return Convert.ToBase64String(saltedHash);
        }

        public string GetSalt()
        {
            var salt = Guid.NewGuid().ToString();

            return salt;
        }
    }
}
