using WarehouseInventoryManagement.Models.Mappers;

namespace WarehouseInventoryManagement.Models.Mappers.ViewModelToEntity
{
    /// <summary>
    /// View model to entity mapper. 
    /// </summary>
    public partial class ViewModelToEntityMapper : MapperBase
    {
        /// <summary>
        /// Static instance of ViewModelToEntityMapper mapper.
        /// </summary>
        public static readonly ViewModelToEntityMapper Mapper = new ViewModelToEntityMapper();
    }
}