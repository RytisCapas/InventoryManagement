using System;
using System.Configuration.Provider;
using System.Web.Mvc;
using System.Web.Security;

using WarehouseInventoryManagement.DataEntities.Entities;
using WarehouseInventoryManagement.DataEntities.Enums;
using WarehouseInventoryManagement.ServiceContracts;

namespace WarehouseInventoryManagement.Common.Membership
{
    public class WarehouseManagementMembershipProvider : MembershipProvider
    {
        public readonly string ApplicationNameValue = "WarehouseManagement";

        public readonly string ProviderName = "WarehouseManagementMembershipProvider";

        public readonly int MaxInvalidPasswordAttemptsValue = 1000;

        public readonly int MinRequiredNonAlphanumericCharactersValue = 0;

        public readonly int MinRequiredPasswordLengthValue = 6;

        public readonly int PasswordAttemptWindowValue = 30;

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

        public override bool EnablePasswordReset
        {
            get
            {
                return true;
            }
        }

        public override bool EnablePasswordRetrieval
        {
            get
            {
                return false;
            }
        }

        public override int MaxInvalidPasswordAttempts
        {
            get
            {
                return MaxInvalidPasswordAttemptsValue;
            }
        }

        public override int MinRequiredNonAlphanumericCharacters
        {
            get
            {
                return MinRequiredNonAlphanumericCharactersValue;
            }
        }

        public override int MinRequiredPasswordLength
        {
            get
            {
                return MinRequiredPasswordLengthValue;
            }
        }

        public override int PasswordAttemptWindow
        {
            get
            {
                return PasswordAttemptWindowValue;
            }
        }

        public override MembershipPasswordFormat PasswordFormat
        {
            get
            {
                return MembershipPasswordFormat.Hashed;
            }
        }

        public override string PasswordStrengthRegularExpression
        {
            get
            {
                return null;
            }
        }

        public override bool RequiresQuestionAndAnswer
        {
            get
            {
                return false;
            }
        }

        public override bool RequiresUniqueEmail
        {
            get
            {
                return false;
            }
        }

        private IUserService UserService
        {
            get
            {
                return DependencyResolver.Current.GetService<IUserService>();
            }
        }

        private ICryptoService CryptoService
        {
            get
            {
                return DependencyResolver.Current.GetService<ICryptoService>();
            }
        }

        public override bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            throw new NotImplementedException();
        }

        public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer)
        {
            throw new NotImplementedException();
        }

        public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override int GetNumberOfUsersOnline()
        {
            throw new NotImplementedException();
        }

        public override string GetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public override MembershipUser GetUser(string username, bool userIsOnline)
        {
            try
            {
                User user = UserService.GetUserByUsername(username);

                if (user == null)
                {
                    throw new MembershipUserNotExistsException("User with specified email does not exist: " + username);
                }

                return GetMembershipUser(user);
            }
            catch (MembershipUserNotExistsException)
            {
                throw;
            }
            catch (Exception exc)
            {
                throw new ProviderException(string.Format("GetUser failed - username: {0}, userIsOnline: {1}", username, userIsOnline), exc);
            }
        }

        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            throw new NotImplementedException();
        }

        public override string GetUserNameByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public override string ResetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public override bool UnlockUser(string userName)
        {
            throw new NotImplementedException();
        }

        public override void UpdateUser(MembershipUser user)
        {
            throw new NotImplementedException();
        }

        public override bool ValidateUser(string username, string password)
        {
            try
            {
                var user = UserService.GetUserByUsername(username);

                string hashedPassword = null;

                if (user != null)
                {
                    hashedPassword = CryptoService.GetHashedValue(password, user.PasswordSalt);
                }

                var userExists = user != null && (user.Password == hashedPassword);

                return userExists;
            }
            catch (MembershipUserBlockedException)
            {
                throw;
            }
            catch (Exception exc)
            {
                throw new ProviderException(string.Format("ValidateUser failed: username: {0}", username), exc);
            }
        }

        private MembershipUser GetMembershipUser(User user)
        {
            return new MembershipUser(ProviderName, user.UserName, user.UserName, user.UserName, null, null, true, false, user.CreatedOn, DateTime.MinValue, DateTime.MinValue, DateTime.MinValue, DateTime.MinValue);
        }
    }
}
