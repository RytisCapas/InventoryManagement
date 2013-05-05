using FluentNHibernate.Mapping;

namespace WarehouseInventoryManagement.DataEntities.Mappings
{
    public abstract class EntityMapBase<TEntity> : ClassMap<TEntity>
        where TEntity : IEntity
    {
    }
}
