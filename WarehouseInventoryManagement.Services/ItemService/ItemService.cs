using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using WarehouseInventoryManagement.DataContracts;
using WarehouseInventoryManagement.DataEntities.Entities;
using WarehouseInventoryManagement.DataEntities.Enums;
using WarehouseInventoryManagement.Models.Dtos;
using WarehouseInventoryManagement.ServiceContracts;

namespace WarehouseInventoryManagement.Services
{
    public class ItemService : IItemService 
    {

        private IRepository repository;

        public ItemService(IRepository repository)
        {
            this.repository = repository;
        }

        public Item Get(Guid id)
        {
            throw new NotImplementedException();
        }

        public Item Save(Item item)
        {
            try
            {
                using (var transaction = new TransactionScope())
                {
                    item.CreatedOn = DateTime.Now;
                    item.ModifiedOn = DateTime.Now;
                    repository.Save(item);
                    repository.Commit();

                    transaction.Complete();
                }

                return item;
            }
            catch (Exception ex)
            {
                throw new ItemException(string.Format("Failed to register new item '{0}'.", item.Name), ex);
            } 
        }

        public Item Create(Item item)
        {
            try
            {
                using (var transaction = new TransactionScope())
                {
                    var initialState = GetState((int) StateEnum.Registered);
                    if (initialState != null)
                    {
                        item.States.Add(initialState);
                    }

                    item.CreatedOn = DateTime.Now;
                    item.ModifiedOn = DateTime.Now;
                    repository.Save(item);
                    repository.Commit();

                    transaction.Complete();
                }

                return item;
            }
            catch (Exception ex)
            {
                throw new UserException(string.Format("Failed to add new item name '{0}'.", item.Name), ex);
            } 
        }

        public State Create(State state)
        {
            try
            {
                using (var transaction = new TransactionScope())
                {
                    state.CreatedOn = DateTime.Now;
                    state.IsDeleted = false;
                    repository.Save(state);
                    repository.Commit();

                    transaction.Complete();
                }

                return state;
            }
            catch (Exception ex)
            {
                throw new ItemException(string.Format("Failed to save new item state '{0}'.", state.StateName), ex);
            } 
        }

        public void SaveItemLog(Item item)
        {
            try
            {
                var itemLog = new ItemLog
                    {
                        CreatedBy = "Anonymous",
                        CreatedOn = DateTime.Now,
                        Item = item,
                        State = item.States.FirstOrDefault()
                    };

                using (var transaction = new TransactionScope())
                {
                    state.CreatedOn = DateTime.Now;
                    state.IsDeleted = false;
                    repository.Save(state);
                    repository.Commit();

                    transaction.Complete();
                }

                return state;
            }
            catch (Exception ex)
            {
                throw new ItemException(string.Format("Failed to save new item state '{0}'.", state.StateName), ex);
            } 
        }

        public State GetState(int id)
        {
            try
            {
                var query = repository.AsQueryOver<State>()
                    .Where(f => f.DeletedOn == null && !f.IsDeleted && f.Id == id)
                    .Future();

                return query.FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw new ItemException("Failed to retrieve state by id.", ex);
            }
        }

        public List<Item> GetAll()
        {
            throw new NotImplementedException();
        }

        public PagedEntityListDto<Item> GetPage(PagedEntityListFilterDto filter)
        {
            throw new NotImplementedException();
        }

        public List<State> GetAllStates()
        {
            throw new NotImplementedException();
        }

        public void DeleteRole(int id)
        {
            throw new NotImplementedException();
        }

        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
