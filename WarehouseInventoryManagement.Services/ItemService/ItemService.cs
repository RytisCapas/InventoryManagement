using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Criterion.Lambda;
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

        private readonly IItemLogService itemLogService;

        public ItemService(IRepository repository, IItemLogService itemLogService)
        {
            this.repository = repository;
            this.itemLogService = itemLogService;
        }

        public Item Get(Guid id)
        {
            try
            {
                var query = repository.AsQueryOver<Item>()
                    .Where(f => f.DeletedOn == null && f.Id == id)
                    .Future();

                return query.FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw new ItemException("Failed to retrieve Item by Guid.", ex);
            }
        }

        public Item Save(Item item)
        {
            try
            {
                using (var transaction = new TransactionScope())
                {
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

                itemLogService.Save(item);

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
            try
            {
                int count;

                var query = repository.AsQueryOver<Item>().Where(f => f.DeletedOn == null);

                if (filter != null)
                {
                    query = AddSortingCriterias(filter, query);
                }

                if (!string.IsNullOrEmpty(filter.SearchText))
                {
                    query = AddSearchCriterias(filter.SearchText, query);
                }

                var entities = query
                    .Skip((filter.StartPage - 1) * filter.ItemsPerPage)
                    .Take(filter.ItemsPerPage)
                    .Future<Item>();

                var rowCount = CriteriaTransformer
                    .TransformToRowCount(query.UnderlyingCriteria)
                    .Future<int>();
                count = rowCount.FirstOrDefault();
                var entitiesList = entities.ToList();

                var pagedList = new PagedEntityListDto<Item>(entitiesList, count);

                pagedList.Page = filter.StartPage;

                return pagedList;

            }
            catch (Exception ex)
            {
                throw new ItemException("Failed to retrieve Users list.", ex);
            }
        }

        public List<State> GetAllStates()
        {
            try
            {
                var query = repository.AsQueryOver<State>().Where(f => f.DeletedOn == null).Future();

                return query.ToList();
            }
            catch (Exception ex)
            {

                throw new ItemException("Failed to retrieve State list.", ex);
            }
        }

        public void DeleteRole(int id)
        {
            throw new NotImplementedException();
        }

        public void Delete(Item item)
        {
            try
            {

                repository.Delete<Item>(item);
                repository.Commit();
            }
            catch (Exception ex)
            {
                throw new ItemException(string.Format("Failed to delete item {0}.", item.Id), ex);
            }
        }

        private static IQueryOver<Item, Item> AddSearchCriterias(string search, IQueryOver<Item, Item> query)
        {
            IList<ICriterion> searchCriterias = new List<ICriterion>();


            searchCriterias.Add(Restrictions.On<Item>(x => x.Name).IsInsensitiveLike(string.Format("%{0}%", search.ToLower())));
            searchCriterias.Add(Restrictions.On<Item>(x => x.SerialNumber).IsInsensitiveLike(string.Format("%{0}%", search.ToLower())));

            Guid guid;

            if (Guid.TryParse(search, out guid))
            {
                searchCriterias.Add(Restrictions.On<User>(x => x.Id).IsLike(guid));
            }

            query = CommonUtils.AddQueryOverSearchCriterias(query, searchCriterias);

            return query;
        }

        private static IQueryOver<Item, Item> AddSortingCriterias(PagedEntityListFilterDto filter, IQueryOver<Item, Item> query)
        {
            IQueryOverOrderBuilder<Item, Item> builder = null;

            switch (filter.Column)
            {
                case "Id":
                    builder = query.OrderBy(x => x.Id);
                    break;
                case "Name":
                    builder = query.OrderBy(x => x.Name);
                    break;
                case "SerialNumber":
                    builder = query.OrderBy(x => x.SerialNumber);
                    break;
                case "CreatedOn":
                    builder = query.OrderBy(x => x.CreatedOn);
                    break;
                case "ModifiedOn":
                    builder = query.OrderBy(x => x.ModifiedOn);
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
