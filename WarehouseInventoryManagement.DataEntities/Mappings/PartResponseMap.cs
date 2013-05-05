using WarehouseInventoryManagement.DataEntities.Entities;

namespace WarehouseInventoryManagement.DataEntities.Mappings
{
    public class PartResponseMap : EntityMapBase<PartResponse>
    {
        public PartResponseMap()
        {
            Table("PartResponses");

            Id(f => f.Id).GeneratedBy.Identity();

            Map(f => f.PartNumber).Not.Nullable();
            Map(f => f.ETag).Length(250).Not.Nullable();

            References(f => f.MultipartUpload).LazyLoad().Cascade.All();
        }
    }
}