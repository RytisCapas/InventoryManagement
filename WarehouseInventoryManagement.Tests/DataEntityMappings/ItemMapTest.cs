using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace WarehouseInventoryManagement.Tests.DataEntityMappings
{
    [TestFixture]
    public class ItemprMapTest : DatabaseTestBase<int>
    {
        [Test]
        public void Should_Check_Item_Mappings_Successfully()
        {

            var entity = TestDataProvider.CreateItem();
            RunEntityMapTestsInTransaction(entity);

        }
    }
}
