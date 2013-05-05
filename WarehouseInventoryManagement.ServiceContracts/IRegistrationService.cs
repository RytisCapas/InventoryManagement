using WarehouseInventoryManagement.DataEntities.Enums;

namespace WarehouseInventoryManagement.ServiceContracts
{
    public interface IRegistrationService
    {
        void RegisterCustomer(string name);
        void RegisterCustomer(string name, CustomerType customerType);
        void UnregisterCustomer(int id);        
    }
}
