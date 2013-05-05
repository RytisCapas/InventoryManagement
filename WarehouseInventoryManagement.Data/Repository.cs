using WarehouseInventoryManagement.DataContracts;

using NHibernate;

namespace WarehouseInventoryManagement.Data
{
    public class Repository: GenericRepositoryBase<int>, IRepository
    {
        public Repository(ISessionFactoryProvider sessionFactoryProvider)
            : base(sessionFactoryProvider)
        {
        }

        public Repository(ISession session)
            : base(session)
        {
        }
    }  
}
