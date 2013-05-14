using WarehouseInventoryManagement.DataEntities.Entities;

namespace WarehouseInventoryManagement.DataEntities.Mappings
{
    public sealed class ItemMap : PersistentEntityMapBase<Item>
    {
        public ItemMap()
        {
            Table("Items");

            Id(f => f.Id).GeneratedBy.GuidComb();

            Map(f => f.SerialNumber).Nullable();

            Map(f => f.Name).Nullable();

            Map(f => f.Type).Not.Nullable().Column("ItemType");

            Map(f => f.Height).Nullable();

            Map(f => f.Length).Nullable();

            Map(f => f.Width).Nullable();

            Map(f => f.Weight).Nullable();

            HasManyToMany<State>(f => f.States)
                .Table("ItemStates")
                .ParentKeyColumn("ItemId")
                .ChildKeyColumn("StateId")
                .Cascade.All();

            HasMany<ItemLog>(f => f.Logs)
                .Cascade.None();
        }
    }
}
