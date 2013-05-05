using System.Linq;

using WarehouseInventoryManagement.DataContracts;
using WarehouseInventoryManagement.Tools.Commands;
using WarehouseInventoryManagement.Web.Logic.Models.Agreement;

namespace WarehouseInventoryManagement.Web.Logic.Commands.Agreement.GetAgreements
{
    public class GetAgreementsCommand : CommandBase, ICommand<GetAgreementsFilter, AgreementListViewModel>
    {
        private readonly IRepository repository;

        public GetAgreementsCommand(IRepository repository)
        {
            this.repository = repository;
        }

        /// <summary>
        /// Executes this command.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>Executed command result.</returns>
        public AgreementListViewModel Execute(GetAgreementsFilter request)
        {
            var query = repository.AsQueryable<DataEntities.Entities.Agreement>()
                .Select(f => new AgreementItemViewModel
                                 {
                                     Id = f.Id,
                                     Number = f.Number,
                                     CustomerName = f.Customer.Name,
                                     CustomerCode = f.Customer.Code,                                    
                                 }
                );

            if (request != null)
            {
                if (request.Id != null)
                {
                    query = query.Where(f => f.Id == request.Id);
                }

                if (!string.IsNullOrEmpty(request.CustomerName))
                {
                    query = query.Where(f => f.CustomerName.ToLower().Contains(request.CustomerName.ToLowerInvariant()));
                }
            }

            return new AgreementListViewModel {
                                                  Agreements = query.ToList()
                                              };
        }

    }
}