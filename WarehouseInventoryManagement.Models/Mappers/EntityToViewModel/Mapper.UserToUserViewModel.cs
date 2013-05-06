using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WarehouseInventoryManagement.DataEntities.Entities;
using WarehouseInventoryManagement.Models.Models.User;

namespace WarehouseInventoryManagement.Models.Mappers.EntityToViewModel
{
    public partial class EntityToViewModelMapper
    {
        public UserViewModel Map(User source, UserViewModel destination)
        {
            if (source == null)
            {
                return null;
            }

            if (destination == null)
            {
                destination = new UserViewModel();
            }

            destination.Id = source.Id;
            destination.UserName = source.UserName;
            destination.FirstName = source.FirstName;
            destination.LastName = source.LastName;
            destination.LastLogin = source.LastLogin; 

            return destination;
        }

        public List<UserViewModel> Map(IList<User> source, IList<UserViewModel> destination = null)
        {
            return MapList(Map, source, destination);
        }
    }
}
