using System;
using System.Configuration.Provider;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

using WarehouseInventoryManagement.DataEntities.Entities;
using WarehouseInventoryManagement.DataEntities.Enums;
using WarehouseInventoryManagement.ServiceContracts;
namespace WarehouseInventoryManagement.Common.Membership
{
    public static class WarehouseManagementAuthentication
    {
        public static void AuthenticateUser(string userName, int userId, string firstName, bool createPersistenctCookie)
        {
            try
            {
                var userData = new WarehouseManagementUserData()
                {
                    UserId = userId,
                    UserName = userName,
                    FirstName = firstName
                };

                var userDataString = WarehouseManagementUserData.Serialize(userData);
                var authCookie = FormsAuthentication.GetAuthCookie(userName, createPersistenctCookie);

                var ticket = FormsAuthentication.Decrypt(authCookie.Value);
                var extendedTicket = new FormsAuthenticationTicket(ticket.Version, ticket.Name, ticket.IssueDate, ticket.Expiration, ticket.IsPersistent, userDataString);

                authCookie.Value = FormsAuthentication.Encrypt(extendedTicket);

                HttpContext.Current.Response.Cookies.Add(authCookie);
            }
            catch (Exception e)
            {
                throw new ProviderException(string.Format("Failed to authenticate user {0}.", userName), e);
            }
        }
    }
}
