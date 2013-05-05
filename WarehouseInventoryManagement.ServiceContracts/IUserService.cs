using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WarehouseInventoryManagement.DataEntities.Entities;

namespace WarehouseInventoryManagement.ServiceContracts
{
    public interface IUserService
    {
        User GetUserByUsername(string username);

        bool IsUserAuthenticated();

        User GetCurrentUser();

        User CreateUser(User user);

        User ValidateUser(string username, string password);
    }
}
