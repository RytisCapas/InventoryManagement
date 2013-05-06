using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace WarehouseInventoryManagement.Models.Models.User
{
    public class UserViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Prisijungimo vardas")]
        public string UserName { get; set; }

        [Display(Name = "Vardas")]
        [StringLength(100, ErrorMessage = "First name is too long")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [Display(Name = "Pavardė")]
        [StringLength(100, ErrorMessage = "Last name is too long")]
        public string LastName { get; set; }

        [Display(Name = "Paskutinis prisijungimas")]
        public DateTime? LastLogin { get; set; }
    }
}
