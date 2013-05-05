using System;
using NHibernate;

namespace WarehouseInventoryManagement.DataContracts
{
    public interface ISessionFactoryProvider
    {
        ISessionFactory SessionFactory { get; }

        ISession Open();
    }
}
