using System.Collections.Generic;
using WarehouseInventoryManagement.DataEntities.Entities;

namespace WarehouseInventoryManagement.ServiceContracts
{
    public interface IAgreementManagementService
    {
        string GenerateAgreementNumber();

        Agreement ExtendAgreement(int customerId);

        IList<Agreement> GetAgreements();       
    }
}
