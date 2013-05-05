using System.Security.Principal;

using WarehouseInventoryManagement.DataContracts;

namespace WarehouseInventoryManagement.Tests.TestHelpers
{
    public class FakePrincipalAccessor : IPrincipalAccessor
    {
        public IPrincipal GetCurrentPrincipal()
        {
            return new GenericPrincipal(new GenericIdentity("UnitTest"), new[] { "admin" });
        }
    }
}
