using System.Configuration;

namespace WarehouseInventoryManagement.Services
{
    public class AgreementManagementServiceConfiguration : ConfigurationSection
    {
        [ConfigurationProperty("agreementCodePrefix", IsRequired = false, DefaultValue = "A")]
        public string AgreementCodePrefix
        {
            get { return (string)this["agreementCodePrefix"]; }
            set { this["agreementCodePrefix"] = value; }
        }

        public AgreementManagementServiceConfiguration()
        {
        }
    }
}
