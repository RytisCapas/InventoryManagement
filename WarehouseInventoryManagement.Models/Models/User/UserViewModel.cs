using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using WarehouseInventoryManagement.Models.Models.Message;

namespace WarehouseInventoryManagement.Models.Models.User
{
    public class UserViewModel
    {
        public UserViewModel()
        {
            Message = new MessageViewModel();
        }

        [Display(Name = "Id")]
        [Required(ErrorMessage = "Įveskite prisijungimo vardą")]
        public int Id { get; set; }

        [Display(Name = "Prisijungimo vardas")]
        [Required(ErrorMessage = "Įveskite prisijungimo vardą")]
        public string UserName { get; set; }

        [Display(Name = "Vardas")]
        [Required(ErrorMessage = "Įveskite vardą")]
        public string FirstName { get; set; }

        [Display(Name = "Pavardė")]
        [Required(ErrorMessage = "Įveskite pavardę")]
        public string LastName { get; set; }

        [Display(Name = "Naujas slaptažodis")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Slaptažodis turi būti bent 6 simbolių ilgio")]
        public string Password { get; set; }

        [Display(Name = "Pakartokite naują slaptažodį")]
        [Compare("Password", ErrorMessage = "Slaptažodžiai nesutampa")]
        public string PasswordConfirmation { get; set; }

        [Display(Name = "Adresas")]
        public string StreetRow { get; set; }

        [Display(Name = "Miestas")]
        public string City { get; set; }

        [Display(Name = "Šalis")]
        public string Country { get; set; }

        [Display(Name = "Telefono numeris")]
        public string Phone { get; set; }

        [Display(Name = "El. paštas")]
        public string Email { get; set; }

        [Display(Name = "Administratorius")]
        public bool IsAdmin { get; set; }

        [Display(Name = "Sandėlio darbuotojas")]
        public bool IsWorker { get; set; }

        public MessageViewModel Message { get; set; }

    }
}
