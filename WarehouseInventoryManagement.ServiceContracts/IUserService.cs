﻿using System.Collections.Generic;
using WarehouseInventoryManagement.DataEntities.Entities;
using WarehouseInventoryManagement.Models.Dtos;

namespace WarehouseInventoryManagement.ServiceContracts
{
    public interface IUserService
    {
        User GetUserByUsername(string username);

        bool IsUserAuthenticated();

        User GetCurrentUser();

        User CreateUser(User user);

        User ValidateUser(string username, string password);

        List<User> GetAllUsers();

        PagedEntityListDto<User> GetPage(PagedEntityListFilterDto filter);
    }
}
