using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using WarehouseInventoryManagement.DataEntities.Enums;
using WarehouseInventoryManagement.Models.Models.Message;

namespace WarehouseInventoryManagement.Models.Models
{
    public class ItemCreateViewModel
    {
        public ItemCreateViewModel()
        {
            Message = new MessageViewModel();
        }
        [Display(Name = "Pavadinimas")]
        [Required(ErrorMessage = "Įveskite pavadinimą")]
        public string Name { get; set; }

        [Display(Name = "Tipas")]
        [Required(ErrorMessage = "Pasirinkite tipą")]
        public ItemType Type { get; set; }

        [Display(Name = "Ilgis")]
        [RegularExpression(@"[0-9]*\.?[0-9]+", ErrorMessage = "Ilgis turi būti skačius")]
        public double Length { get; set; }

        [Display(Name = "Aukštis")]
        [RegularExpression(@"[0-9]*\.?[0-9]+", ErrorMessage = "Aukštis turi būti skačius")]
        public double Height { get; set; }

        [Display(Name = "Plotis")]
        [RegularExpression(@"[0-9]*\.?[0-9]+", ErrorMessage = "Plotis turi būti skačius")]
        public double Width { get; set; }

        [Display(Name = "Svoris")]
        [RegularExpression(@"[0-9]*\.?[0-9]+", ErrorMessage = "Svoris turi būti skačius")]
        public double Weight { get; set; }

        public MessageViewModel Message { get; set; }

    }
}
