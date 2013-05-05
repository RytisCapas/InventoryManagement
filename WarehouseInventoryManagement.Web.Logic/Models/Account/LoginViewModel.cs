using System.ComponentModel.DataAnnotations;

namespace WarehouseInventoryManagement.Web.Logic.Models.Account
{
    public class LoginViewModel
    {
        [Display(Name = "Email")]
        [Required]
        public string UserName { get; set; }

        [Display(Name = "Password")]
        [Required]
        public string Password { get; set; }
    }
}