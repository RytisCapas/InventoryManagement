﻿using System;

namespace WarehouseInventoryManagement.Models.Models
{
    public class ItemViewModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string SerialNumber { get; set; }

        public string StateName { get; set; }

        public DateTime CreatedOn { get; set; }

        public string CreatedBy { get; set; }

        public DateTime ModifiedOn { get; set; }

        public string ModifiedBy { get; set; }
    }
}
