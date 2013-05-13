using System;
using System.Collections.Generic;
using WarehouseInventoryManagement.DataEntities.Entities;
using WarehouseInventoryManagement.Models.Dtos;

namespace WarehouseInventoryManagement.ServiceContracts
{
    public interface IItemService
    {
        Item Get(Guid id);

        Item Save(Item item);

        Item Create(Item item);

        State Create(State state);

        State GetState(int id);

        List<Item> GetAll();

        PagedEntityListDto<Item> GetPage(PagedEntityListFilterDto filter);

        List<State> GetAllStates();

        void DeleteRole(int id);

        void Delete(Guid id);
    }
}
