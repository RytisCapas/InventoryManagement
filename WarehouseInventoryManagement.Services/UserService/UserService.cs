using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Transactions;
using System.Web.Security;
using WarehouseInventoryManagement.DataContracts;
using WarehouseInventoryManagement.DataEntities.Entities;
using WarehouseInventoryManagement.ServiceContracts;

namespace WarehouseInventoryManagement.Services
{
    public class UserService : IUserService
    {
        private IRepository repository;

        private readonly ICryptoService cryptoService;

        public UserService(IRepository repository, ICryptoService cryptoService)
        {
            this.repository = repository;
            this.cryptoService = cryptoService;
        }
        public User GetUserByUsername(string username)
        {
            try
            {
                var query = repository.AsQueryOver<User>()
                    .Where(f => f.DeletedOn == null && f.UserName == username)
                    .Future();

                return query.FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw new AgreementManagementException("Failed to retrieve agreements list.", ex);
            }
        }

        public bool IsUserAuthenticated()
        {
            try
            {
                bool result = Thread.CurrentPrincipal != null && Thread.CurrentPrincipal.Identity.IsAuthenticated;

                if (result)
                {
                    var roles = Roles.GetRolesForUser();
                }

                return result;
            }
            catch (Exception inner)
            {
                throw new UserException("IsUserAuthenticated failed: ", inner);
            }
        }

        public User GetCurrentUser()
        {
            try
            {
                User user = null;

                if (IsUserAuthenticated())
                {
                    string email = Thread.CurrentPrincipal.Identity.Name;

                    if (!string.IsNullOrEmpty(email))
                    {
                        user = GetUserByUsername(email);
                    }
                }

                return user;
            }
            catch (Exception ex)
            {
                throw new UserException("Failed to get current user.", ex);
            }
        }

        public User CreateUser(User user)
        {
            try
            {
                using (var transaction = new TransactionScope())
                {
                    user.PasswordSalt = cryptoService.GetSalt();
                    user.Password = cryptoService.GetHashedValue(user.Password, user.PasswordSalt);
                    user.CreatedOn = DateTime.Now;
                    repository.Save(user);
                    repository.Commit();

                    transaction.Complete();
                }

                return user;
            }
            catch (Exception ex)
            {
                throw new UserException(string.Format("Failed to register user with User Name '{0}'.", user.UserName), ex);
            }              
        }

        public User ValidateUser(string username, string password)
        {
            try
            {
                var user = GetUserByUsername(username);

                string hashedPassword = null;

                if (user != null)
                {
                    hashedPassword = cryptoService.GetHashedValue(password, user.PasswordSalt);
                }

                var userExists = user != null && (user.Password == hashedPassword);

                if (userExists && user != null)
                {
                    return user;
                }
            }
            catch (Exception exc)
            {
                throw new UserException(string.Format("ValidateUser failed: username: {0}", username), exc);
            }

            return null;
        }
    }
}
