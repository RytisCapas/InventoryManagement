using System.Collections.Generic;
using WarehouseInventoryManagement.DataEntities.Entities;
using WarehouseInventoryManagement.Models.Models.Item;

namespace WarehouseInventoryManagement.Models.Mappers.ViewModelToEntity
{
    public partial class ViewModelToEntityMapper
    {
        public Item Map(ItemEditViewModel source, Item destination)
        {
            if (source == null)
            {
                return null;
            }

            if (destination == null)
            {
                destination = new Item();
            }

            destination.SerialNumber = source.SerialNumber;
            destination.Name = source.Name;
            destination.Type = source.Type;
            destination.Length = source.Length;
            destination.Height = source.Height;
            destination.Width = source.Width;
            destination.Weight = source.Weight;


            if (destination.States == null)
            {
                destination.States = new List<State>();
            }

            return destination;
        }

        public List<Item> Map(IList<ItemEditViewModel> source, IList<Item> destination = null)
        {
            return MapList(Map, source, destination);
        }
    }
}
