using System;
using System.Transactions;

using WarehouseInventoryManagement.DataContracts;
using WarehouseInventoryManagement.DataEntities.Entities;
using WarehouseInventoryManagement.DataEntities.Enums;
using WarehouseInventoryManagement.ServiceContracts;

using NHibernate;

namespace WarehouseInventoryManagement.Services
{
    public class RegistrationService : IRegistrationService
    {
        private readonly IAgreementManagementService agreementManagementService;

        private readonly IRepository repository;

        public RegistrationService(IRepository repository, IAgreementManagementService agreementManagementService)
        {
            this.repository = repository;
            this.agreementManagementService = agreementManagementService;
        }

        public void RegisterCustomer(string name)
        {
            RegisterCustomer(name, CustomerType.Standard);
        }

        public void RegisterCustomer(string name, CustomerType customerType)
        {            
            try
            {
                using (var transaction = new TransactionScope())
                {
                    Customer customer = new Customer();
                    customer.Name = name;
                    customer.Type = customerType;

                    customer.Agreements.Add(
                        new Agreement
                            {
                                Number = agreementManagementService.GenerateAgreementNumber(),
                                CreatedOn = DateTime.Now,
                                Customer = customer
                            });

                    repository.Save(customer);
                    repository.Commit();

                    transaction.Complete();
                }
            }
            catch (Exception ex)
            {                
                throw new RegistrationException(string.Format("Failed to register customer with name '{0}' and type {1}.", name, customerType), ex);
            }                                
        }

        public void UnregisterCustomer(int id)
        {
            try
            {
                repository.Delete<Customer>(id);
                repository.Commit();
            }
            catch (Exception ex)
            {
                throw new RegistrationException(string.Format("Failed to unregister customer {0}.", id), ex);
            }
        }
    }
}
