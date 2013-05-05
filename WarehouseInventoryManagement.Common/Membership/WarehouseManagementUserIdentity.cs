using System.Security.Principal;
using System.Web.Security;

namespace WarehouseInventoryManagement.Common.Membership
{
    public class OncologyNutritionUserIdentity : GenericIdentity
    {
        private readonly WarehouseManagementUserData userData;

        public OncologyNutritionUserIdentity(FormsAuthenticationTicket ticket, string userName)
            : base(userName)
        {
            WarehouseManagementUserData desiarializedUserData = WarehouseManagementUserData.Deserialize(ticket.UserData);
            if (desiarializedUserData != null)
            {
                userData = desiarializedUserData;
            }
        }

        public int UserId
        {
            get { return userData.UserId; }
        }

        public string UserName
        {
            get { return userData.UserName; }
        }

        public string FirstName
        {
            get { return userData.FirstName; }
        }

        public string ImpersonatorUserName
        {
            get { return userData.ImpersonatorUserName; }
        }
    }
}
