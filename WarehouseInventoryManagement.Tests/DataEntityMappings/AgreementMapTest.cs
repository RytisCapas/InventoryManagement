using WarehouseInventoryManagement.Data.DataContext;
using WarehouseInventoryManagement.DataContracts;
using WarehouseInventoryManagement.DataEntities.Entities;
using WarehouseInventoryManagement.Tests.TestHelpers;
using FluentNHibernate.Testing;
using NUnit.Framework;

namespace WarehouseInventoryManagement.Tests.DataEntityMappings
{
    [TestFixture]
    public class AgreementMapTest : DatabaseTestBase<int>
    {
        [Test]
        public void Should_Check_Agreement_Mappings_Successfully()
        {
            var entity = TestDataProvider.CreateCustomer();
            RunEntityMapTestsInTransaction(entity);            
        }   
    }
}
