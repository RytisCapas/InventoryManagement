using System;
using System.ComponentModel.DataAnnotations;

namespace WarehouseInventoryManagement.Models.Models.Item
{
    public class ItemLogViewModel
    {
        [Display(Name = "Id")]
        public Guid ItemId { get; set; }

        [Display(Name = "Pavadinimas")]
        public string ItemName { get; set; }

        [Display(Name = "Būsena")]
        public string StateName { get; set; }

        [Display(Name = "Pakeitė")]
        public string ModifiedBy { get; set; }

        [Display(Name = "Keitimo laikas")]
        public DateTime ModifiedOn { get; set; }

        [Display(Name = "Sukūrimo laikas")]
        public DateTime CreatedOn { get; set; }

    }
}
