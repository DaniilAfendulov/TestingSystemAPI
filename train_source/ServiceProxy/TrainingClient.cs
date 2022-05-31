using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ServiceProxy.TrainingServiceReference;

namespace ServiceProxy
{
    public static class TrainingClient
    {
        private static ServiceInterfaceClient m_instance;
        private static object m_lock = new object();

        public static ServiceInterfaceClient Client
        {
            get
            {
                if (m_instance == null)
                {
                    lock (m_lock)
                    {
                        if (m_instance == null)
                            m_instance = new ServiceInterfaceClient();
                    }
                }
                return m_instance;
            }
        }

        public static void PingServer()
        {
            Client.PingServer();
        }

        internal static void CloseConnection()
        {
            lock (m_lock)
            {
                if(m_instance != null)
                {
                    try
                    {
                        m_instance.Close();
                    }
                    catch (Exception)
                    {
                    }
                    m_instance = null;
                }
            }
        }
    }
}
