using Common.Data.Content;
using System;
using System.Collections.Generic;
using TestingSystemAPI.Models;

namespace TestingSystemAPI.Services
{
    public interface IModuleService
    {
        Module FindModule(Guid guid);
        Lesson GetLesson(Guid moduleId, Guid id);
        LessonInfo GetLessonInfo(Guid moduleId, Guid id);
        ModuleInfo GetModuleInfo(Guid moduleId);
        List<Lesson> GetModuleLessons(Guid moduleId);
        List<Module> GetModules();
        PracticeTest GetPracticeTest(Lesson lesson);
        List<Test> GetTests(Guid moduleId, Guid id);
        List<Test> GetTests(Lesson lesson);
        List<Test> GetTests(TheoryTest theoryTest);
        TheoryTest GetTheoryTest(Lesson lesson);
        IEnumerable<TestModel> GetTheoryTests(Guid moduleId, Guid id);
        Video GetVideo(Lesson lesson);
    }
}