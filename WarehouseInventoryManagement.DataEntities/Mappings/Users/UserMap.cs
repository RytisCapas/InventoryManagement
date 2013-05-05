using WarehouseInventoryManagement.DataEntities.Entities;

namespace WarehouseInventoryManagement.DataEntities.Mappings
{
    public sealed class UserMap : PersistentEntityMapBase<User>
    {
        public UserMap()
        {
            Table("Users");

            Id(f => f.Id).GeneratedBy.Identity();

            Map(f => f.UserName).Not.Nullable();

            Map(f => f.FirstName).Nullable();

            Map(f => f.LastName).Nullable();

            Map(f => f.Password).Not.Nullable();

            Map(f => f.PasswordSalt).Not.Nullable();

            Map(f => f.StreetRow).Nullable();

            Map(f => f.Country).Nullable();

            Map(f => f.City).Nullable();

            Map(f => f.Phone).Nullable();

            Map(f => f.Email).Nullable();

            Map(f => f.LastLogin).Nullable();

            HasManyToMany<Role>(f => f.Roles)
                .Table("UserRoles")
                .ParentKeyColumn("UserId")
                .ChildKeyColumn("RoleId")
                .OrderBy("CreatedOn DESC")
                .Cascade.All().BatchSize(10);

            //References(f => f.Customer).LazyLoad().Not.Nullable().Cascade.SaveUpdate();
        }
    }
}
