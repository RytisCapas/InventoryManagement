﻿using System;
using System.Collections.Generic;
using WarehouseInventoryManagement.DataEntities.Enums;

namespace WarehouseInventoryManagement.DataEntities.Entities
{
    public class Item : PersistentEntityBase<Item>
    {
        public virtual new Guid Id { get; set; }

        public virtual string Name { get; set; }

        public virtual ItemType Type { get; set; }

        public virtual double Height { get; set; }

        public virtual double Length { get; set; }

        public virtual double Width { get; set; }

        public virtual double Weight { get; set; }

        public virtual List<State> States { get; set; }

        public virtual List<ItemLog> Logs { get; set; } 

    }
}