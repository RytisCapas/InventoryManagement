using System.Collections.Generic;
using System.Linq;
using WarehouseInventoryManagement.DataEntities.Entities;
using WarehouseInventoryManagement.DataEntities.Enums;
using WarehouseInventoryManagement.Models.Models.Item;

namespace WarehouseInventoryManagement.Models.Mappers.EntityToViewModel
{
    public partial class EntityToViewModelMapper
    {
        public ItemEditViewModel Map(Item source, ItemEditViewModel destination)
        {
            if (source == null)
            {
                return null;
            }

            if (destination == null)
            {
                destination = new ItemEditViewModel();
            }

            destination.Id = source.Id;
            destination.Name = source.Name;
            destination.SerialNumber = source.SerialNumber;
            destination.Type = source.Type;
            destination.Length = source.Length;
            destination.Height = source.Height;
            destination.Width = source.Width;
            destination.Weight = source.Weight;

            var state = source.States.FirstOrDefault();

            destination.States = state == null ? StateEnum.Registered : (StateEnum)state.Id;

            return destination;

        }

        public List<ItemEditViewModel> Map(IList<Item> source, IList<ItemEditViewModel> destination = null)
        {
            return MapList(Map, source, destination);
        }
    }
}
