using WarehouseInventoryManagement.Data.DataContext;
using WarehouseInventoryManagement.DataContracts;
using WarehouseInventoryManagement.DataEntities.Entities;
using WarehouseInventoryManagement.DataEntities.Enums;
using WarehouseInventoryManagement.Tests.TestHelpers;
using FluentNHibernate.Testing;
using NUnit.Framework;

namespace WarehouseInventoryManagement.Tests.DataEntityMappings
{
    [TestFixture]
    public class CustomerMapTest : DatabaseTestBase<int>
    {
        [Test]
        public void Should_Check_Customer_Mappings_Successfully()
        {
            var entity = TestDataProvider.CreateCustomer();
            RunEntityMapTestsInTransaction(entity);
        }
    }
}
