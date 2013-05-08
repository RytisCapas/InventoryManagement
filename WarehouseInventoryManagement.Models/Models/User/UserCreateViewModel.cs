using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace WarehouseInventoryManagement.Models.Models.User
{
    public class UserCreateViewModel
    {
        [Display(Name = "Prisijungimo vardas")]
        [Required(ErrorMessage = "Įveskite prisijungimo vardą")]
        public string UserName { get; set; }

        [Display(Name = "Vardas")]
        [Required(ErrorMessage = "Įveskite vardą")]
        public string FirstName { get; set; }

        [Display(Name = "Vardas")]
        [Required(ErrorMessage = "Įveskite pavardę")]
        public string LastName { get; set; }

        [Display(Name = "Slaptažodis")]
        [Required(ErrorMessage = "Įveskite slaptažodį")]
        [StringLength(100,MinimumLength = 6, ErrorMessage = "Slaptažodis turi būti bent 6 simbolių ilgio")]
        public string Password { get; set; }

        [Display(Name = "Pakartokite slaptažodį")]
        [Required(ErrorMessage = "Įveskite slaptažodį")]
        [Compare("Password", ErrorMessage = "Slaptažodžiai nesutampa")]
        public string PasswordConfirmation { get; set; }

        [Display(Name = "Adresas")]
        public string StreetRow { get; set; }

        [Display(Name = "Miestas")]
        public string City { get; set; }

        [Display(Name = "Šalis")]
        public string Country { get; set; }

        [Display(Name = "Telefono Numeris")]
        public string Phone { get; set; }

        [Display(Name = "El. paštas")]
        public string Email { get; set; }

        [Display(Name = "Administratorius")]
        public bool IsAdmin { get; set; }

        [Display(Name = "Sandėlio darbuotojas")]
        public bool IsWorker { get; set; }
    }
}
