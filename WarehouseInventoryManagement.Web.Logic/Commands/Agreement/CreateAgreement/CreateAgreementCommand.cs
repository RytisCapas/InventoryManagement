using System;

using WarehouseInventoryManagement.DataContracts;
using WarehouseInventoryManagement.DataEntities.Entities;
using WarehouseInventoryManagement.ServiceContracts;
using WarehouseInventoryManagement.Tools.Commands;
using WarehouseInventoryManagement.Tools.Messages;
using WarehouseInventoryManagement.Web.Logic.Models.Agreement;

namespace WarehouseInventoryManagement.Web.Logic.Commands.Agreement.CreateAgreement
{
    public class CreateAgreementCommand : CommandBase, ICommand<CreateAgreementViewModel, bool>
    {
        private IRepository repository;

        private IAgreementManagementService agreementManagementService;

        public CreateAgreementCommand(IRepository repository, IAgreementManagementService agreementManagementService)
        {
            this.agreementManagementService = agreementManagementService;
            this.repository = repository;
        }

        bool ICommand<CreateAgreementViewModel, bool>.Execute(CreateAgreementViewModel request)
        {
            DataEntities.Entities.Agreement agreement = new DataEntities.Entities.Agreement();
            agreement.Customer = repository.AsProxy<Customer>(request.CustomerId);
            agreement.Number = agreementManagementService.GenerateAgreementNumber();
            
            repository.Save(agreement);
            repository.Commit();

            return true;
        }
    }
}