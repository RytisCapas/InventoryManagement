namespace WarehouseInventoryManagement.DataEntities.Mappings
{
    public abstract class PersistentEntityMapBase<TEntity> : EntityMapBase<TEntity> 
        where TEntity : IPersistentEntity
    {
        protected PersistentEntityMapBase()
        {
            Map(x => x.CreatedOn).Not.Nullable();
            Map(x => x.ModifiedOn).Not.Nullable();
            Map(x => x.DeletedOn).Nullable();

            Map(x => x.CreatedBy).Not.Nullable();
            Map(x => x.ModifiedBy).Nullable();
            Map(x => x.DeletedBy).Nullable();
        }
    }
}