using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Transactions;
using System.Web.Security;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Criterion.Lambda;
using WarehouseInventoryManagement.DataContracts;
using WarehouseInventoryManagement.DataEntities.Entities;
using WarehouseInventoryManagement.Models.Dtos;
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

        public List<User> GetAllUsers()
        {
            try
            {
                var query = repository.AsQueryOver<User>().Where(f => f.DeletedOn == null).Future();

                return query.ToList();
            }
            catch (Exception ex)
            {
                throw new UserException("Failed to retrieve Users list.", ex);
            }
        }

        public PagedEntityListDto<User> GetPage(PagedEntityListFilterDto filter)
        {
            try
            {
                int count;

                var query = repository.AsQueryOver<User>().Where(f => f.DeletedOn == null);

                if (filter != null)
                {
                    query = AddSortingCriterias(filter,query);
                }

                if (!string.IsNullOrEmpty(filter.SearchText))
                {
                    query = AddSearchCriterias(filter.SearchText, query);
                }

                var entities = query
                    .Skip((filter.StartPage - 1) * filter.ItemsPerPage)
                    .Take(filter.ItemsPerPage)
                    .Future<User>();

                var rowCount = CriteriaTransformer
                    .TransformToRowCount(query.UnderlyingCriteria)
                    .Future<int>();
                count = rowCount.FirstOrDefault();
                var entitiesList = entities.ToList();

                var pagedList = new PagedEntityListDto<User>(entitiesList, count);

                pagedList.Page = filter.StartPage;

                return pagedList;

            }
            catch (Exception ex)
            {
                throw new UserException("Failed to retrieve Users list.", ex);
            }
        }

        private static IQueryOver<User, User> AddSearchCriterias(string search, IQueryOver<User, User> query)
        {
            IList<ICriterion> searchCriterias = new List<ICriterion>();

            User userAlias = null;

            searchCriterias.Add(Restrictions.On(() => userAlias.UserName ).IsInsensitiveLike(string.Format("%{0}%", search.ToLower())));
            searchCriterias.Add(Restrictions.On(() => userAlias.FirstName).IsInsensitiveLike(string.Format("%{0}%", search.ToLower())));
            searchCriterias.Add(Restrictions.On(() => userAlias.LastName).IsInsensitiveLike(string.Format("%{0}%", search.ToLower())));

            int searchInteger;

            if (int.TryParse(search, out searchInteger))
            {
                searchCriterias.Add(Restrictions.On(() => userAlias.Id).IsLike(searchInteger));
            }

            query = CommonUtils.AddQueryOverSearchCriterias(query, searchCriterias);

            return query;
        }

        private static IQueryOver<User, User> AddSortingCriterias(PagedEntityListFilterDto filter, IQueryOver<User, User> query)
        {
            IQueryOverOrderBuilder<User, User> builder = null;

            switch (filter.Column)
            {
                case "Id":
                    builder = query.OrderBy(x => x.Id);
                    break;
                case "UserName":
                    builder = query.OrderBy(x => x.UserName);
                    break;
                case "LastName":
                    builder = query.OrderBy(x => x.LastName);
                    break;
                case "LastLogin":
                    builder = query.OrderBy(x => x.LastLogin);
                    break;
            }

            if (builder != null)
            {
                query = filter.AscendingOrder ? builder.Asc : builder.Desc;
            }

            return query;
        }
    }
}
