using System.Configuration;

namespace WarehouseInventoryManagement.ServiceContracts
{
    public interface IConfigurationLoaderService
    {
        T LoadConfig<T>() where T : ConfigurationSection;
    }
}
