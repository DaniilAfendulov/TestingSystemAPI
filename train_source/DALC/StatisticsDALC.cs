using System;
using System.Collections.Generic;
using System.Linq;
using Common.Data.Statistics;
using NHibernate;
using NHibernate.Criterion;

namespace DALC
{
    /// <summary>
    /// Класс для работы с БД по статистике
    /// </summary>
    public class StatisticsDALC
    {
        private StatisticsDALC()
        {
        }

        private static StatisticsDALC m_instance;
        private static object m_lock = new object();

        public static StatisticsDALC Instance
        {
            get
            {
                if (m_instance == null)
                {
                    lock (m_lock)
                    {
                        if (m_instance == null)
                            m_instance = new StatisticsDALC();
                    }
                }
                return m_instance;
            }
        }

        /// <summary>
        /// Создать статистику
        /// </summary>
        public void CreateStatistic(TestStatistic _statistic)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Save(_statistic, _statistic.ID);
                    transaction.Commit();
                }
            }
        }

        /// <summary>
        /// Удалить статистику
        /// </summary>
        public void DeleteStatistic(Guid _id)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    var statistic = session.Get<TestStatistic>(_id);
                    session.Delete(statistic);
                    transaction.Commit();
                }
            }
        }

        /// <summary>
        /// Изменить статистику
        /// </summary>
        public void UpdateStatistic(TestStatistic _statistic)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Update(_statistic);
                    transaction.Commit();
                }
            }
        }

        /// <summary>
        /// Получить статистику по студенту
        /// </summary>
        public List<TestStatistic> GetStatisticByStudentId(Guid _studentId)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    var statistics = session
                        .CreateCriteria<TestStatistic>()
                        .Add(Restrictions.Eq("StudentId", _studentId))
                        .List<TestStatistic>()
                        .ToList();
                    transaction.Rollback();
                    return statistics;
                }
            }
        }


        /// <summary>
        /// Получить статистику по студенту и тесту
        /// </summary>
        public List<TestStatistic> GetStatisticByStudentIdAndTestId(Guid _studentId, Guid _testId)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    var statistics = session
                        .CreateCriteria<TestStatistic>()
                        .Add(Restrictions.And(
                        Restrictions.Eq("StudentId", _studentId),
                        Restrictions.Eq("TestId", _testId)))
                        .List<TestStatistic>()
                        .ToList();
                    transaction.Rollback();
                    return statistics;
                }
            }
        }

        /// <summary>
        /// Получить статистику по тесту
        /// </summary>
        public List<TestStatistic> GetStatisticByTestId(Guid _testId)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    var statistics = session
                        .CreateCriteria<TestStatistic>()
                        .Add(Restrictions.Eq("TestId", _testId))
                        .List<TestStatistic>()
                        .ToList();
                    transaction.Rollback();
                    return statistics;
                }
            }
        }
    }
}
