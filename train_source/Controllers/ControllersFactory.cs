using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Controllers
{
    /// <summary>
    /// класс-фабрика
    /// </summary>
    public class ControllersFactory : IDisposable
    {
        private static UserController m_userController;
        private static ContentController m_contentController;
        private static StatisticController m_statisticController;
        private static ControllersFactory m_instance;

        /// <summary>
        /// Путь к папке с видео и тестами
        /// </summary>
        public static string m_ContentFolderName = "Content";

        /// <summary>
        /// объект синхронизации
        /// </summary>
        private static object m_lock = new object();

        /// <summary>
        /// Инстанс класса-фабрики
        /// </summary>
        public static ControllersFactory Instance
        {
            get
            {
                if (m_instance == null)
                    lock (m_lock)
                    {
                        if (m_instance == null)
                            m_instance = new ControllersFactory();
                    }
                return m_instance;
            }
        }

        /// <summary>
        /// Логика управления пользователями
        /// </summary>
        public UserController UserBC
        {
            get
            {
                if (m_userController == null)
                    lock (m_lock)
                    {
                        if (m_userController == null)
                            m_userController = new UserController();
                    }
                return m_userController;
            }
        }

        /// <summary>
        /// Логика управления контентом
        /// </summary>
        public ContentController ContentBC
        {
            get
            {
                if (m_contentController == null)
                    lock (m_lock)
                    {
                        if (m_contentController == null)
                            m_contentController = new ContentController(m_ContentFolderName);
                    }
                return m_contentController;
            }
        }

        /// <summary>
        /// Логика управления статистикой
        /// </summary>
        public StatisticController StatisticBC
        {
            get
            {
                if (m_statisticController == null)
                    lock (m_lock)
                    {
                        if (m_statisticController == null)
                            m_statisticController = new StatisticController();
                    }
                return m_statisticController;
            }
        }

        public void Dispose()
        {
            if (UserController.IsConnected)
            {
                ContentBC.Dispose();
                UserBC.Dispose();
                StatisticBC.Dispose();
            }
        }
    }
}
