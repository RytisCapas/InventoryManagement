using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WarehouseInventoryManagement.DataEntities.Entities;
using WarehouseInventoryManagement.DataEntities.Enums;
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
            destination.CreatedOn = source.CreatedOn;
            destination.CreatedBy = source.CreatedBy;
            destination.ModifiedOn = source.ModifiedOn;
            destination.ModifiedBy = source.ModifiedBy;

            return destination;

        }

        public List<ItemViewModel> Map(IList<Item> source, IList<ItemViewModel> destination = null)
        {
            return MapList(Map, source, destination);
        }
    }
}
