using System.Collections.Generic;
using WarehouseInventoryManagement.DataEntities.Entities;
using WarehouseInventoryManagement.Models.Dtos;

namespace WarehouseInventoryManagement.ServiceContracts
{
    public interface IUserService
    {
        User GetUserByUsername(string username);

        User GetUserById(int id);

        bool IsUserAuthenticated();

        User GetCurrentUser();

        User SaveUser(User user);

        User CreateUser(User user);

        User ValidateUser(string username, string password);

        List<User> GetAllUsers();

        PagedEntityListDto<User> GetPage(PagedEntityListFilterDto filter);

        List<Role> GetAllRoles();

        void Delete(int id);
    }
}
