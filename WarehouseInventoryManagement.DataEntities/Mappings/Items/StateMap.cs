using WarehouseInventoryManagement.DataEntities.Entities;

namespace WarehouseInventoryManagement.DataEntities.Mappings
{
    public sealed class StateMap : EntityMapBase<State>
    {
        public StateMap()
        {
            Table("States");

            Id(f => f.Id).GeneratedBy.Identity();
            Map(f => f.StateName).Not.Nullable();
            Map(f => f.CreatedOn).Not.Nullable().Default("getdate()").Generated.Insert();
            Map(f => f.IsDeleted).Not.Nullable().Default("false");
            Map(f => f.DeletedOn).Nullable();
        }
    }
}
