using log4net;

namespace Controllers
{
    /// <summary>
    /// Класс для логгирования
    /// </summary>
    public static class LogController
    {
        private static ILog m_log;

        /// <summary>
        /// объект синхронизации
        /// </summary>
        private static object m_lock = new object();

        static LogController()
        {
            log4net.Config.XmlConfigurator.Configure();
        }

        public static ILog Log
        {
            get
            {
                if (m_log == null)
                {
                    lock (m_lock)
                    {
                        if (m_log == null)
                            m_log = log4net.LogManager.GetLogger("RollingFileAppender");
                    }
                }
                return m_log;
            }
        }
    }
}
