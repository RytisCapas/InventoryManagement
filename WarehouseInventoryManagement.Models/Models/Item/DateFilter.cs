using System;
using System.ComponentModel.DataAnnotations;

namespace WarehouseInventoryManagement.Models.Models
{
    public class DateFilter
    {
        [Display(Name = "Nuo:")]
        public DateTime DateFrom { get; set; }

        [Display(Name = "Iki:")]
        public DateTime DateTo { get; set; }
    }
}
