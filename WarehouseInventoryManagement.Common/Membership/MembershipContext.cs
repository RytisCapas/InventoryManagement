using System.Threading;
using System.Web.Mvc;
using WarehouseInventoryManagement.DataEntities.Entities;
using WarehouseInventoryManagement.DataEntities.Enums;
using WarehouseInventoryManagement.ServiceContracts;

namespace WarehouseInventoryManagement.Common.Membership
{
    public class MembershipContext
    {
        public static bool IsUserAuthenticated
        {
            get
            {
                var userService = DependencyResolver.Current.GetService<IUserService>();
                return userService.IsUserAuthenticated();
            }
        }

        public static User CurrentUser
        {
            get
            {
                var userService = DependencyResolver.Current.GetService<IUserService>();
                return userService.GetCurrentUser();
            }
        }

        public static bool IsUserInRole(params UserRoleEnum[] roles)
        {
            if (roles == null || roles.Length == 0)
            {
                return true;
            }

            foreach (var role in roles)
            {
                if (Thread.CurrentPrincipal != null && Thread.CurrentPrincipal.IsInRole(role.ToString()))
                {
                    return true;
                }
            }

            return false;
        }

        public static bool IsUserInRole(UserRoleEnum role)
        {
            return IsUserInRole(new[] { role });
        }
    }
}
