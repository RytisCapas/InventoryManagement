using System;

namespace WarehouseInventoryManagement.DataEntities.Enums.Extensions
{
    public class StringValueAttribute : Attribute
    {
        public StringValueAttribute(string value)
        {
            this.StringValue = value;
        }

        public string StringValue { get; protected set; }
    }
}
