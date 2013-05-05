using System.ComponentModel.DataAnnotations;

namespace WarehouseInventoryManagement.Web.Logic.Models.Agreement
{
    public class CreateAgreementViewModel
    {
        [Required(ErrorMessage = "Please specify name of the agreement.")]        
        public string Name { get; set; }

        [Required]
        public int CustomerId { get; set; }
    }
}