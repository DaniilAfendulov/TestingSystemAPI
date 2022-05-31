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

        public ModuleInfo GetModuleInfo(Guid moduleId)
        {
            return new ModuleInfo()
            {
                Module = FindModule(moduleId),
                Lessons = GetModuleLessons(moduleId)
            };
        }

        public Lesson GetLesson(Guid moduleId, Guid id)
        {
            return GetModuleLessons(moduleId).FirstOrDefault(l => l.ID == id);
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

        public IEnumerable<TestModel> GetTheoryTests(Guid moduleId, Guid id)
        {
            return GetTests(moduleId, id).Select(test => new TestModel(test));
        }

        public List<Test> GetTests(Guid moduleId, Guid id)
        {
            return GetTests(GetLesson(moduleId, id));
        }

        public List<Test> GetTests(Lesson lesson)
        {
            return GetTests(GetTheoryTest(lesson));
        }

        public List<Test> GetTests(TheoryTest theoryTest)
        {
            return _contentController.LoadTests(theoryTest);
        }

        public LessonInfo GetLessonInfo(Guid moduleId, Guid id)
        {
            Lesson lesson = GetLesson(moduleId, id);
            if (lesson == null) return null;
            return new LessonInfo()
            {
                ID = lesson.ID,
                ModuleID = lesson.ModuleID,
                Title = lesson.Name,
                IsVideoDisabled = GetPracticeTest(lesson).ID == Guid.Empty,
                IsPracticeDisabled = GetPracticeTest(lesson).ID == Guid.Empty,
                IsTheoryDisabled = GetTheoryTest(lesson).ID == Guid.Empty
            };
        }


    }
}
