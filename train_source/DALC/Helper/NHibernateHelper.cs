using NHibernate;
using NHibernate.Cfg;

namespace DALC
{
    /// <summary>
    /// Класс для работы NHibernate
    /// </summary>
    public class NHibernateHelper
    {
        private static object m_lock = new object();
        private static ISessionFactory m_sessionFactory;

        private static ISessionFactory SessionFactory
        {
            get
            {
                lock (m_lock)
                {
                    if (m_sessionFactory == null)
                    {
                        Configuration cfg = new Configuration();
                        cfg.AddAssembly(typeof(NHibernateHelper).Assembly);
                        ISessionFactory sf = cfg.Configure().BuildSessionFactory();
                        m_sessionFactory = cfg.BuildSessionFactory();
                    }
                    return m_sessionFactory;
                }
            }
        }

        public static ISession OpenSession()
        {
            return SessionFactory.OpenSession();
        }
    }
}
