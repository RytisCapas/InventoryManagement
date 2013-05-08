using System.Dynamic;
using System.Linq;

namespace WarehouseInventoryManagement.Tools.Extensions
{
    public static class ViewBagExtensions
    {
        public static bool Has(this object obj, string propertyName)
        {
            var dynamic = obj as DynamicObject;

            if (dynamic == null)
            {
                return false;
            }

            return dynamic.GetDynamicMemberNames().Contains(propertyName);
        }
    }
}
