using System;

namespace WarehouseInventoryManagement.Web.Logic.Commands.Agreement.GetAgreements
{
    public class GetAgreementsFilter
    {
        public int? Id { get; set; }

        public string CustomerName { get; set; }
    }
}