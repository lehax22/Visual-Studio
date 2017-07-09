using NHibernate;
using NHibernate.Cfg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;

namespace Practice.Helper
{
    public class NHibernateSession
    {
        public static ISession OpenSession()
        {
            var configuration = new Configuration();
            configuration.Configure();
            ISessionFactory sessionFactory = configuration.BuildSessionFactory();
            LogManager.GetCurrentClassLogger().Debug("Configuration collected");
            return sessionFactory.OpenSession();
        }
    }
}
