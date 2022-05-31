using System;
using System.Collections.Generic;
using Common.Data.Content;
using Common.Data.Statistics;

namespace ServiceProxy
{
    public class StatisticProxy
    {
        private static StatisticProxy m_instance;
        private static object m_lock = new object();

        public static StatisticProxy Instance
        {
            get
            {
                if (m_instance == null)
                {
                    lock (m_lock)
                    {
                        if (m_instance == null)
                            m_instance = new StatisticProxy();
                    }
                }
                return m_instance;
            }
        }

        public void CreateStatistic(TestStatistic _statistic)
        {
            TrainingClient.Client.CreateStatistic(_statistic);
        }

        public void UpdateStatistic(TestStatistic _statistic)
        {
            TrainingClient.Client.UpdateStatistic(_statistic);
        }

        public List<DifficultyStatistic> GetDifficultyStatistic(List<Guid> _testIds)
        {
            return TrainingClient.Client.GetDifficultyStatistic(_testIds);
        }

        public List<StudentModuleStatistic> GetStudentModuleStatistic(List<Lesson> _lessons, List<TheoryTest> _theoryTests, List<PracticeTest> _practiceTests, Guid _groupId)
        {
            return TrainingClient.Client.GetStudentModuleStatistic(_lessons, _theoryTests, _practiceTests, _groupId);
        }
    }
}
