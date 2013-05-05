using System;
using System.Collections.Generic;
using System.Configuration.Provider;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using WarehouseInventoryManagement.DataEntities.Enums;
using WarehouseInventoryManagement.ServiceContracts;

namespace WarehouseInventoryManagement.Common.Membership
{
    public class WarehouseManagementRoleProvider : RoleProvider
    {
        public readonly string ApplicationNameValue = "WarehouseManagement";

        public readonly string ProviderName = "WarehouseManagementRoleProvider";

        public override string ApplicationName
        {
            get
            {
                return ApplicationNameValue;
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        private IUserService UserService
        {
            get
            {
                return DependencyResolver.Current.GetService<IUserService>();
            }
        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            var allRoles = ((UserRoleEnum[])Enum.GetValues(typeof(UserRoleEnum))).Select(r => r.ToString()).ToArray();

            return allRoles;
        }

        public override string[] GetRolesForUser(string username)
        {
            try
            {
                var user = UserService.GetUserByUsername(username);

                if (user != null && user.Roles != null && user.Roles.Any())
                {
                    var userRoleEnums = new List<string>();

                    foreach (var roleEnum in GetAllRoles())
                    {
                        if (user.Roles.Any(x => x.RoleName == roleEnum))
                        {
                            userRoleEnums.Add(roleEnum);
                        }
                    }

                    return userRoleEnums.ToArray();
                }

                return new string[] { };
            }
            catch (Exception exc)
            {
                throw new ProviderException(string.Format("GetRolesForUser failed - username: {0}", username), exc);
            }
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            try
            {
                var role = (UserRoleEnum)Enum.Parse(typeof(UserRoleEnum), roleName);
                var user = UserService.GetUserByUsername(username);

                return user != null && user.Roles.Any(x => x.Id == (int)role);
            }
            catch (Exception exc)
            {
                throw new ProviderException(string.Format("IsUserInRole failed - username: {0}, roleName: {1}", username, roleName), exc);
            }
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
    }
}
