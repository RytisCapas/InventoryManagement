using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using WarehouseInventoryManagement.DataContracts;
using WarehouseInventoryManagement.DataEntities.Entities;
using WarehouseInventoryManagement.Models.Models;
using WarehouseInventoryManagement.ServiceContracts;

namespace WarehouseInventoryManagement.Services
{
    public class ItemLogService : IItemLogService
    {
       private readonly IPrincipalAccessor principalAccessor;
     
        private IRepository repository;

        public ItemLogService(IPrincipalAccessor principalAccessor, IRepository repository)
        {
            this.principalAccessor = principalAccessor;
            this.repository = repository;
        }

        private string PrincipalName
        {
            get
            {
                var principal = principalAccessor.GetCurrentPrincipal();

                string name = null;

                if (principal != null && principal.Identity != null)
                {
                    name = principal.Identity.Name;
                }

                if (string.IsNullOrWhiteSpace(name))
                {
                    name = "Anonymous";
                }

                return name;
            }
        }

        public List<ItemLog> GetItemLogsByItemId(Guid id, bool deleted = false)
        {
            try
            {
                var query = repository.AsQueryOver<ItemLog>()
                                      .Where(f => f.DeletedOn == null && f.Item.Id == id);

                if (!deleted)
                {
                    query.Where(f => f.DeletedOn == null);
                }
                var future = query.Future();

                return future.ToList();
            }
            catch (Exception ex)
            {
                throw new ItemException("Failed to retrieve user by username.", ex);
            }
        }

        public List<ItemLog> GetItemLogs(DateFilter filter)
        {
            try
            {
                var query = repository.AsQueryOver<ItemLog>()
                                      .Where(f => f.DeletedOn == null 
                                          //&& f.Item.DeletedOn == null
                                          );

                if (filter.DateFrom != DateTime.MinValue)
                {
                    query = query.Where(f => f.CreatedOn >= filter.DateFrom);
                }

                if (filter.DateTo != DateTime.MinValue)
                {
                    filter.DateTo = filter.DateTo.AddDays(1).AddSeconds(-1);
                    query = query.Where(f => f.CreatedOn <= filter.DateTo);
                }
                query = query.OrderBy(f => f.CreatedOn).Asc;

                var future = query.Future();

                return future.ToList();
            }
            catch (Exception ex)
            {
                throw new ItemException("Failed to retrieve item logs.", ex);
            }
        }

        public void DeleteItemLogs(Guid id)
        {
            try
            {
                var logs = GetItemLogsByItemId(id);

                if (logs != null && logs.Count > 0)
                {
                    var date = DateTime.Now;

                    using (var transaction = new TransactionScope())
                    {
                        foreach (var log in logs)
                        {
                            log.DeletedOn = date;
                            log.DeletedBy = PrincipalName;

                            repository.Save(log);
                        }

                        repository.Commit();

                        transaction.Complete();
                    }
                    
                }

            }
            catch (Exception ex)
            {
                throw new ItemException("Failed to delete item logs", ex);
            }
        }


    

        public void Save(Item item)
        {
            if (item == null || !item.States.Any())
            {
                return;
            }
            var itemLog = new ItemLog
                {
                    CreatedBy = PrincipalName,
                    CreatedOn = DateTime.Now,
                    Item = item,
                    State = item.States.FirstOrDefault()
                };

            try
            {
                    using (var transaction = new TransactionScope())
                    {
                        
                        repository.Save(itemLog);

                        repository.Commit();

                        transaction.Complete();

                }

            }
            catch (Exception ex)
            {
                throw new ItemException("Failed to save item log", ex);
            }
        }
    }
}
