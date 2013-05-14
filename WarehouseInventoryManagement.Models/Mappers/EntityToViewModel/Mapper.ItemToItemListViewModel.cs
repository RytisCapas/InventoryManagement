using System.Collections.Generic;
using System.Linq;
using WarehouseInventoryManagement.DataEntities.Entities;
using WarehouseInventoryManagement.Models.Models;

namespace WarehouseInventoryManagement.Models.Mappers.EntityToViewModel
{
    public partial class EntityToViewModelMapper
    {
        public ItemViewModel Map(Item source, ItemViewModel destination)
        {
            if (source == null)
            {
                return null;
            }

            if (destination == null)
            {
                destination = new ItemViewModel();
            }
            destination.Id = source.Id;
            destination.Name = source.Name;
            destination.SerialNumber = source.SerialNumber;
            destination.CreatedOn = source.CreatedOn;
            destination.CreatedBy = source.CreatedBy;
            destination.ModifiedOn = source.ModifiedOn;
            destination.ModifiedBy = source.ModifiedBy;

            var state = source.States.FirstOrDefault();

            destination.StateName = state == null ? "-" : state.StateName;

            return destination;

        }

        public List<ItemViewModel> Map(IList<Item> source, IList<ItemViewModel> destination = null)
        {
            return MapList(Map, source, destination);
        }
    }
}
