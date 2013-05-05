using System.Security.Principal;

namespace WarehouseInventoryManagement.DataContracts
{
    public interface IPrincipalAccessor
    {
        /// <summary>
        /// Gets the current principal.
        /// </summary>
        /// <returns></returns>
        IPrincipal GetCurrentPrincipal();       
    }
}
