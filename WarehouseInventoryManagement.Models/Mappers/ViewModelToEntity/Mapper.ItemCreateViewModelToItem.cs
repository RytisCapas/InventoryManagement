using System.Collections.Generic;
using WarehouseInventoryManagement.DataEntities.Entities;
using WarehouseInventoryManagement.Models.Models;

namespace WarehouseInventoryManagement.Models.Mappers.ViewModelToEntity
{
    public partial class ViewModelToEntityMapper
    {
        public Item Map(ItemCreateViewModel source, Item destination)
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

        public List<Item> Map(IList<ItemCreateViewModel> source, IList<Item> destination = null)
        {
            return MapList(Map, source, destination);
        }
    }
}
