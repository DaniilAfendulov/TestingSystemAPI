using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Data.Statistics;
using ServiceProxy;
using Common.Data.Content;
using Common.Data.Users;

namespace Controllers
{
    public class StatisticController : IDisposable
    {
        private StatisticProxy m_statisticProxy;

        internal StatisticController()
        {
            m_statisticProxy = StatisticProxy.Instance;
        }

        public void Dispose()
        {

        }

        public List<DifficultyStatistic> GetDifficultyStatistic(List<Guid> _testIds)
        {
            if (UserController.IsConnected)
                return m_statisticProxy.GetDifficultyStatistic(_testIds);
            else
                return getLocalDifficultyStatistic(_testIds);
        }

        public List<StudentModuleStatistic> GetStudentModuleStatistic(Guid _moduleId, Guid _groupId)
        {
            var lessons = ControllersFactory.Instance.ContentBC.LoadLessons(_moduleId);
            var theoryTests = lessons.Select(x => ControllersFactory.Instance.ContentBC.LoadTheoryTest(x)).Where(x => x.ID != Guid.Empty).ToList();
            var practiceTests = lessons.Select(x => ControllersFactory.Instance.ContentBC.LoadPracticeTest(x)).Where(x => x.ID != Guid.Empty).ToList();

            if (UserController.IsConnected)
                return m_statisticProxy.GetStudentModuleStatistic(lessons, theoryTests, practiceTests, _groupId);
            else
                return getLocalStudentModuleStatistic(lessons, theoryTests, practiceTests);
        }

        private List<StudentModuleStatistic> getLocalStudentModuleStatistic(List<Lesson> _lessons, List<TheoryTest> _theoryTests, List<PracticeTest> _practiceTests)
        {
            List<StudentModuleStatistic> result = new List<StudentModuleStatistic>();

            var allLocalStatistic = ControllersFactory.Instance.ContentBC.LoadAllStatistic();
            var students = allLocalStatistic.Select(x => x.StudentName).Distinct().ToList();
            foreach (var student in students)
            {
                var stat = new StudentModuleStatistic { StudentName = student };
                stat.StudentNameWOGroup = student.Remove(student.IndexOf(", гр. "));
                stat.StudentNameGroupOnly = student.Substring(student.IndexOf(", гр. ") + 6);
                foreach (var lesson in _lessons)
                {
                    var theoryTest = _theoryTests.FirstOrDefault(x => x.LessonId == lesson.ID);
                    var practiceTest = _practiceTests.FirstOrDefault(x => x.LessonId == lesson.ID);
                    var lessonStat = new LessonResultStatistic
                    {
                        HasTheoryStatistic = false,
                        AttemptsCount = 0,
                        BestTheoryResult = 0,
                        HasPracticeStatistic = false,
                        PracticeResult = 0,
                    };

                    if (theoryTest != null)
                    {
                        var theoryStats = allLocalStatistic.Where(x => x.StudentName == student && x.TestId == theoryTest.ID && x.Result != -1).ToList();
                        if (theoryStats.Any())
                        {
                            lessonStat.HasTheoryStatistic = true;
                            lessonStat.BestTheoryResult = theoryStats.Max(x => x.Result);
                            lessonStat.AttemptsCount = theoryStats.Count;
                        }
                    }

                    if (practiceTest != null)
                    {
                        var practiceStats = allLocalStatistic.Where(x => x.StudentName == student && x.TestId == practiceTest.ID && x.Result != -1).ToList();
                        if (practiceStats.Any())
                        {
                            lessonStat.HasPracticeStatistic = true;
                            lessonStat.PracticeResult = practiceStats.Max(x => x.Result);
                        }
                    }

                    stat.LessonsStatistic.Add(lesson.ID, lessonStat);
                }
                if (stat.LessonsStatistic.Values.Where(x => x.HasTheoryStatistic).Any())
                    stat.Total = MeanNum(stat.LessonsStatistic.Values.Where(x => x.HasTheoryStatistic).Select(x => x.BestTheoryResult).ToList());
                else
                    stat.Total = 0;
                stat.TotalBool = (stat.Total > 50);
                result.Add(stat);
            }

            return result;
        }

        private List<DifficultyStatistic> getLocalDifficultyStatistic(List<Guid> _testIds)
        {
            List<DifficultyStatistic> result = new List<DifficultyStatistic>();

            var allLocalStatistic = ControllersFactory.Instance.ContentBC.LoadAllStatistic();
            foreach (var id in _testIds)
            {
                var testStatistics = allLocalStatistic.Where(x => x.TestId == id && x.Result != -1).ToList();

                result.Add(new DifficultyStatistic
                {
                    TestId = id,
                    StudentsCount = testStatistics.GroupBy(x => x.StudentName).Count(),
                    MeanResult = MeanNum(testStatistics.Select(x => x.Result).ToList()),
                    MeanAttemptsCount = MeanNum(testStatistics.GroupBy(x => x.StudentName).Select(x => x.Count()).ToList())
                });
            }

            return result;
        }

        public TestStatistic CreateStatistic(Guid _testId, Student _student)
        {
            var stat = new TestStatistic
            {
                ID = Guid.NewGuid(),
                Result = -1,
                StudentId = _student.ID,
                StudentName = _student.Name,
                TestId = _testId
            };
            if (UserController.IsConnected)
                m_statisticProxy.CreateStatistic(stat);
            else
                ControllersFactory.Instance.ContentBC.SaveStatistic(stat);
            return stat;
        }

        public void UpdateStatistic(TestStatistic _statistic)
        {
            if (UserController.IsConnected)
                m_statisticProxy.UpdateStatistic(_statistic);
            else
                ControllersFactory.Instance.ContentBC.UpdateStatistic(_statistic);
        }

        private static double MeanNum(List<int> _nums)
        {
            double result = 0;

            if (_nums.Any())
            {
                foreach (var n in _nums)
                    result += n;
                result = result / _nums.Count;
            }

            return Math.Round(result, 2);
        }

        public List<Student> GetLocalStatisticStudents()
        {
            var allLocalStatistic = ControllersFactory.Instance.ContentBC.LoadAllStatistic();
            var students = allLocalStatistic.Select(x => x.StudentName).Distinct().ToList();
            return students.Select(x => new Student { Name = x }).ToList();
        }

        /*public List<Group> GetLocalStatisticGroups()
        {
            var allLocalStatistic = ControllersFactory.Instance.ContentBC.LoadAllStatistic();
            var groups = allLocalStatistic.Select(x => x.GroupName).Distinct().ToList();
            return groups.Select(x => new Group { Name = x }).ToList();
        }

        public List<Student> GetLocalStatisticStudentsByGroupName(string _groupName)
        {
            var allLocalStatistic = ControllersFactory.Instance.ContentBC.LoadAllStatistic();
            var students = allLocalStatistic.Where(x => x.GroupName == _groupName).Select(x => x.StudentName).Distinct().ToList();
            return students.Select(x => new Student { Name = x }).ToList();
        }*/
    }
}
