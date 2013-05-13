using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WarehouseInventoryManagement.DataEntities.Entities;
using WarehouseInventoryManagement.Models.Models.User;

namespace WarehouseInventoryManagement.Models.Mappers.ViewModelToEntity
{
    public partial class ViewModelToEntityMapper
    {
        public User Map(UserViewModel source, User destination)
        {
            if (source == null)
            {
                return null;
            }

            if (destination == null)
            {
                destination = new User();
            }

            destination.FirstName = source.FirstName;
            destination.LastName = source.LastName;
            destination.Email = source.Email;
            destination.Country = source.Country;
            destination.City = source.City;
            destination.StreetRow = source.StreetRow;
            destination.Phone = source.Phone;

            if (destination.Roles == null)
            {
                destination.Roles = new List<Role>();
            }

            return destination;
        }

        public List<User> Map(IList<UserViewModel> source, IList<User> destination = null)
        {
            return MapList(Map, source, destination);
        }
    }
}