using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WarehouseInventoryManagement.DataEntities.Entities;
using WarehouseInventoryManagement.DataEntities.Enums;
using WarehouseInventoryManagement.Models.Models.User;


namespace WarehouseInventoryManagement.Models.Mappers.ViewModelToEntity
{
    public partial class ViewModelToEntityMapper
    {
        public User Map(UserCreateViewModel source, User destination)
        {
            if (source == null)
            {
                return null;
            }

            if (destination == null)
            {
                destination = new User();
            }

            destination.UserName = source.UserName;
            destination.FirstName = source.FirstName;
            destination.LastName = source.LastName;
            destination.Password = source.Password;
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

        public List<User> Map(IList<UserCreateViewModel> source, IList<User> destination = null)
        {
            return MapList(Map, source, destination);
        }
    }
}
