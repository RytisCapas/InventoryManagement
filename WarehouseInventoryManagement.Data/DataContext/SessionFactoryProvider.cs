using BetterCms.Core.DataAccess.DataContext.EventListeners;

using WarehouseInventoryManagement.Data.DataContext.Conventions;
using WarehouseInventoryManagement.Data.EventListeners;
using WarehouseInventoryManagement.DataContracts;
using WarehouseInventoryManagement.DataEntities;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using FluentNHibernate.Conventions.Helpers;
using NHibernate;
using NHibernate.Event;

namespace WarehouseInventoryManagement.Data.DataContext
{
    public class SessionFactoryProvider : ISessionFactoryProvider
    {
        private readonly static object LockObject = new object();

        private volatile ISessionFactory sessionFactory;

        private IPrincipalAccessor principalAccessor;

        public SessionFactoryProvider(IPrincipalAccessor principalAccessor)
        {
            this.principalAccessor = principalAccessor;
        }

        public ISessionFactory SessionFactory
        {
            get
            {
                if (sessionFactory == null)
                {
                    lock (LockObject)
                    {
                        if (sessionFactory == null)
                        {
                            sessionFactory = CreateSessionFactory();
                        }
                    }
                }

                return sessionFactory;
            }
        }

        public ISession Open()
        {
            return SessionFactory.OpenSession();
        }

        private ISessionFactory CreateSessionFactory()
        {                  
            var eventListenerHelper = new EventListenerHelper(principalAccessor);
            var saveOrUpdateEventListener = new SaveOrUpdateEventListener(eventListenerHelper);
            var deleteEventListener = new DeleteEventListener(eventListenerHelper);

            return
                Fluently.Configure()
                        .Database(MsSqlConfiguration.MsSql2008.ConnectionString(config => config.FromConnectionStringWithKey("ApplicationServices")))
                        .Mappings(m => m.FluentMappings.AddFromAssemblyOf<IEntity>()
                                        .Conventions.Add(ForeignKey.EndsWith("Id"))
                                        .Conventions.Add<EnumConvention>())

                        .ExposeConfiguration(c => c.SetListener(ListenerType.Delete, deleteEventListener))
                        .ExposeConfiguration(c => c.SetListener(ListenerType.SaveUpdate, saveOrUpdateEventListener))
                        .ExposeConfiguration(c => c.SetListener(ListenerType.Save, saveOrUpdateEventListener))
                        .ExposeConfiguration(c => c.SetListener(ListenerType.Update, saveOrUpdateEventListener))

                        .BuildConfiguration()
                        .BuildSessionFactory();
        }
    }
}