using System.Security.Principal;
using System.Web;

using WarehouseInventoryManagement.DataContracts;

namespace WarehouseInventoryManagement.Web.Logic
{
    public class WebPrincipalAccessor : IPrincipalAccessor
    {
        /// <summary>
        /// Gets the current principal.
        /// </summary>
        /// <returns></returns>
        public IPrincipal GetCurrentPrincipal()
        {
            if (HttpContext.Current.User != null)
            {
                return HttpContext.Current.User;
            }

            return null;
        }      
    }
}