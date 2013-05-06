using System.Collections.Generic;
using WarehouseInventoryManagement.DataEntities;

namespace WarehouseInventoryManagement.Models.Dtos
{
    public class PagedEntityListDto<T> where T : class, IEntity
    {
        public PagedEntityListDto(IList<T> entities, int count)
        {
            Entities = entities;
            Count = count;
        }

        public IList<T> Entities { get; set; }

        public int Count { get; set; }

        public int Page { get; set; }
    }
}
