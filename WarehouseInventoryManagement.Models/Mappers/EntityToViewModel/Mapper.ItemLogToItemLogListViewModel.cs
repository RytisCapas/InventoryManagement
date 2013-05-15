using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WarehouseInventoryManagement.DataEntities.Entities;
using WarehouseInventoryManagement.Models.Models.Item;

namespace WarehouseInventoryManagement.Models.Mappers.EntityToViewModel
{
    public partial class EntityToViewModelMapper
    {
        public ItemLogViewModel Map(ItemLog source, ItemLogViewModel destination)
        {
            if (source == null)
            {
                return null;
            }

            if (destination == null)
            {
                destination = new ItemLogViewModel();
            }

            if (source.Item != null)
            {
                destination.ItemId = source.Item.Id;
                destination.ItemName = source.Item.Name;
                destination.CreatedOn = source.Item.CreatedOn;
            }

            if (source.State != null)
            {
                destination.StateName = source.State.StateName;
            }

            destination.ModifiedOn = source.CreatedOn;
            destination.ModifiedBy = source.CreatedBy;

            return destination;

        }

        public List<ItemLogViewModel> Map(IList<ItemLog> source, IList<ItemLogViewModel> destination = null)
        {
            return MapList(Map, source, destination);
        }
    }
}
