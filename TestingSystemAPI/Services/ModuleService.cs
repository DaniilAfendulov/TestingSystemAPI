using Common.Data.Content;
using Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestingSystemAPI.Models;

namespace TestingSystemAPI.Services
{
    public class ModuleService : IModuleService
    {
        private ContentController _contentController;

        public ModuleService(ContentController contentController)
        {
            _contentController = contentController;
        }

        public List<Module> GetModules()
        {
            return _contentController.LoadModules();
        }

        public Module FindModule(Guid guid)
        {
            return GetModules().FirstOrDefault(m => m.ID == guid);
        }

        public List<Lesson> GetModuleLessons(Guid moduleId)
        {
            return _contentController.LoadLessons(moduleId);
        }

        public ModuleInfoDTO GetModuleInfo(Guid moduleId)
        {
            return new ModuleInfoDTO()
            {
                Module = FindModule(moduleId),
                Lessons = GetModuleLessons(moduleId)
            };
        }

        public Lesson GetLesson(Guid moduleId, Guid lessonId)
        {
            return GetModuleLessons(moduleId).FirstOrDefault(l => l.ID == lessonId);
        }

        public Video GetVideo(Lesson lesson)
        {
            return _contentController.LoadVideo(lesson);
        }

        public PracticeTest GetPracticeTest(Lesson lesson)
        {
            return _contentController.LoadPracticeTest(lesson);
        }

        public TheoryTest GetTheoryTest(Lesson lesson)
        {
            return _contentController.LoadTheoryTest(lesson);
        }

        public IEnumerable<TestDTO> GetTheoryTests(Guid moduleId, Guid id)
        {
            return GetTests(moduleId, id).Select(test => new TestDTO(test));
        }

        public List<Test> GetTests(Guid moduleId, Guid lessonId)
        {
            var lesson = GetLesson(moduleId, lessonId);
            if (lesson == null)
            {
                return new List<Test>();
            }
            return GetTests(lesson);
        }

        public List<Test> GetTests(Lesson lesson)
        {
            return GetTests(GetTheoryTest(lesson));
        }

        public List<Test> GetTests(TheoryTest theoryTest)
        {
            return _contentController.LoadTests(theoryTest);
        }

        public LessonInfoDTO GetLessonInfo(Guid moduleId, Guid id)
        {
            Lesson lesson = GetLesson(moduleId, id);
            if (lesson == null) return null;
            return new LessonInfoDTO()
            {
                ID = lesson.ID,
                ModuleID = lesson.ModuleID,
                Title = lesson.Name,
                IsVideoDisabled = GetPracticeTest(lesson).ID == Guid.Empty,
                IsPracticeDisabled = GetPracticeTest(lesson).ID == Guid.Empty,
                IsTheoryDisabled = GetTheoryTest(lesson).ID == Guid.Empty
            };
        }

        public int CheckTests(List<Test> tests, IEnumerable<AnswerDTO> answers)
        {
            if (tests == null || tests.Count == 0 || answers.Count() != tests.Count) return 0;
            Dictionary<Guid, Test> dictionaryTests = tests.ToDictionary(t => t.ID);
            if (!answers.All(ans => dictionaryTests.ContainsKey(ans.ID)))
            {
                return 0;
            }
            return answers.Aggregate(0, 
                (acc, next) =>
                    acc + (CheckTest(dictionaryTests[next.ID], next.Answers) ? 1 : 0),
                res => 100 * res / tests.Count);
        }

        public bool CheckTest(Test test, IEnumerable<string> answers)
        {
            if (test.CorrectAnswers.Count != answers.Count()) return false;
            return test.CorrectAnswers.All(ans => answers.Contains(ans));
        }

        public int CheckTests(Guid moduleId, Guid lessonId, IEnumerable<AnswerDTO> answers)
        {
            List<Test> tests = GetTests(moduleId, lessonId);
            return CheckTests(tests, answers);
        }
    }
}
