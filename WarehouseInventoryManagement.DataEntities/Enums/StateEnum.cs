using WarehouseInventoryManagement.DataEntities.Enums.Extensions;

namespace WarehouseInventoryManagement.DataEntities.Enums
{
    public enum StateEnum
    {
        [StringValue("Registruotas sistemoje")]
        Registered = 1,

        [StringValue("Patalpintas sandėlyje")]
        Stored = 2,

        [StringValue("Pašalintas iš sandėlio")]
        Removed = 3
    }
}
