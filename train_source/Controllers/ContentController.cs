using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Data;
using System.IO;
using Common;
using Common.Data.Content;
using Common.Data.Statistics;

namespace Controllers
{
    public class ContentController : IDisposable
    {
        private string ContentFolderName = "Content";
        private const string VideoFolderName = "Video";
        private const string PracticeTestFolderName = "PracticeTest";
        private const string TheoryTestFolderName = "TheoryTest";
        private const string ConfigFileName = "config.xml";
        private const string StatisticFileName = "stat.xml";
        private const string FileExtension = ".xml";
        private const int MAX_LOCAL_STATISTICS_COUNT = 10000;

        internal ContentController()
        {
        }

        internal ContentController(string _ContentFolderName)
        {
            ContentFolderName = _ContentFolderName;
        }

        /// <summary>
        /// Сохранить модуль
        /// </summary>
        /// <param name="_module">модуль</param>
        public void SaveModule(Module _module)
        {
            var contentFolder = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), ContentFolderName);
            if (!Directory.Exists(contentFolder))
                Directory.CreateDirectory(contentFolder);

            var moduleFolder = Path.Combine(contentFolder, _module.ID.ToString());
            if (!Directory.Exists(moduleFolder))
                Directory.CreateDirectory(moduleFolder);
            var configFile = Path.Combine(moduleFolder, ConfigFileName);

            XMLHelper.WriteXML<Module>(_module, configFile);
        }

        /// <summary>
        /// Сохранить урок
        /// </summary>
        /// <param name="_lesson">урок</param>
        public void SaveLesson(Lesson _lesson)
        {
            var contentFolder = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), ContentFolderName);
            if (!Directory.Exists(contentFolder))
                Directory.CreateDirectory(contentFolder);

            var moduleFolder = Path.Combine(contentFolder, _lesson.ModuleID.ToString());
            if (!Directory.Exists(moduleFolder))
                Directory.CreateDirectory(moduleFolder);

            var lessonFolder = Path.Combine(moduleFolder, _lesson.ID.ToString());
            if (!Directory.Exists(lessonFolder))
                Directory.CreateDirectory(lessonFolder);

            var configFile = Path.Combine(lessonFolder, ConfigFileName);
            XMLHelper.WriteXML<Lesson>(_lesson, configFile);

        }

        /// <summary>
        /// Сохранить видеоурок
        /// </summary>
        /// <param name="_video">видео</param>
        /// <param name="_lesson">урок</param>
        public Video SaveVideo(Video _video, Lesson _lesson)
        {
            var contentFolder = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), ContentFolderName);
            if (!Directory.Exists(contentFolder))
                Directory.CreateDirectory(contentFolder);
            var moduleFolder = Path.Combine(contentFolder, _lesson.ModuleID.ToString());
            if (!Directory.Exists(moduleFolder))
                Directory.CreateDirectory(moduleFolder);
            var lessonFolder = Path.Combine(moduleFolder, _lesson.ID.ToString());
            if (!Directory.Exists(lessonFolder))
                Directory.CreateDirectory(lessonFolder);
            var videoFolder = Path.Combine(lessonFolder, VideoFolderName);

            try
            {
                if (Directory.Exists(videoFolder))
                    Directory.Delete(videoFolder, true);
            }
            catch (Exception ex)
            {
                LogController.Log.Error(ex);
            }
            Directory.CreateDirectory(videoFolder);

            if(_video.ID != Guid.Empty)
            {
                var filePath = Path.Combine(videoFolder, _video.ID.ToString() + Path.GetExtension(_video.FilePath));
                File.Copy(_video.FilePath, filePath);

                /*
                var videoPath = Path.Combine(ContentFolderName, _lesson.ModuleID.ToString(), 
                    _lesson.ID.ToString(), VideoFolderName, _video.ID.ToString() + Path.GetExtension(_video.FilePath));
                 * */
                var videoPath = Path.Combine(_lesson.ModuleID.ToString(),
                    _lesson.ID.ToString(), VideoFolderName, _video.ID.ToString() + Path.GetExtension(_video.FilePath));
                _video.FilePath = videoPath;
            }

            var configPath = Path.Combine(videoFolder, ConfigFileName);
            XMLHelper.WriteXML<Video>(_video, configPath);

            return _video;
        }

        /// <summary>
        /// Сохранить описание теоретического теста
        /// </summary>
        /// <param name="_theoryTest">теоретический тест</param>
        public void SaveTheoryTest(TheoryTest _theoryTest)
        {
            var contentFolder = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), ContentFolderName);
            if (!Directory.Exists(contentFolder))
                Directory.CreateDirectory(contentFolder);
            var moduleFolder = Path.Combine(contentFolder, _theoryTest.ModuleId.ToString());
            if (!Directory.Exists(moduleFolder))
                Directory.CreateDirectory(moduleFolder);
            var lessonFolder = Path.Combine(moduleFolder, _theoryTest.LessonId.ToString());
            if (!Directory.Exists(lessonFolder))
                Directory.CreateDirectory(lessonFolder);
            var theoryTestFolder = Path.Combine(lessonFolder, TheoryTestFolderName);

            if (Directory.Exists(theoryTestFolder))
                Directory.Delete(theoryTestFolder, true);
            Directory.CreateDirectory(theoryTestFolder);

            var configPath = Path.Combine(theoryTestFolder, ConfigFileName);
            XMLHelper.WriteXML<TheoryTest>(_theoryTest, configPath);
        }

        /// <summary>
        /// Сохранить описание практического теста
        /// </summary>
        /// <param name="_practiceTest">практический тест</param>
        public void SavePracticeTest(PracticeTest _practiceTest)
        {
            var contentFolder = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), ContentFolderName);
            if (!Directory.Exists(contentFolder))
                Directory.CreateDirectory(contentFolder);
            var moduleFolder = Path.Combine(contentFolder, _practiceTest.ModuleId.ToString());
            if (!Directory.Exists(moduleFolder))
                Directory.CreateDirectory(moduleFolder);
            var lessonFolder = Path.Combine(moduleFolder, _practiceTest.LessonId.ToString());
            if (!Directory.Exists(lessonFolder))
                Directory.CreateDirectory(lessonFolder);
            var practiceTestFolder = Path.Combine(lessonFolder, PracticeTestFolderName);

            if (Directory.Exists(practiceTestFolder))
            {
                try
                {
                    Directory.Delete(practiceTestFolder, true);
                }
                catch (Exception ex)
                {
                    LogController.Log.Error(ex);
                }
            }
            Directory.CreateDirectory(practiceTestFolder);

            var configPath = Path.Combine(practiceTestFolder, ConfigFileName);
            XMLHelper.WriteXML<PracticeTest>(_practiceTest, configPath);
        }
        
        /// <summary>
        /// Сохранить тест
        /// </summary>
        /// <param name="_test">тест</param>
        /// <param name="_theoryTest">описание теоретического теста</param>
        public void SaveTest(Test _test, TheoryTest _theoryTest)
        {
            _test.IsAnswered = false;
            _test.TestResult = false;
            var contentFolder = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), ContentFolderName);
            if (!Directory.Exists(contentFolder))
                Directory.CreateDirectory(contentFolder);
            var moduleFolder = Path.Combine(contentFolder, _theoryTest.ModuleId.ToString());
            if (!Directory.Exists(moduleFolder))
                Directory.CreateDirectory(moduleFolder);
            var lessonFolder = Path.Combine(moduleFolder, _theoryTest.LessonId.ToString());
            if (!Directory.Exists(lessonFolder))
                Directory.CreateDirectory(lessonFolder);
            var theoryTestFolder = Path.Combine(lessonFolder, TheoryTestFolderName);

            if (!Directory.Exists(theoryTestFolder))
                Directory.CreateDirectory(theoryTestFolder);
            
            //НОВОЕ ДЛЯ КАРТИНКИ В ТЕОРЕТИЧЕСКОМ ТЕСТЕ

            if (_test.PicturePath != "")
            {
                /*
                var picturePath = Path.Combine(ContentFolderName, _theoryTest.ModuleId.ToString(),
                        _theoryTest.LessonId.ToString(), TheoryTestFolderName, _test.ID.ToString() + Path.GetExtension(_test.PicturePath));
                 * */
                var picturePath = Path.Combine(_theoryTest.ModuleId.ToString(),
                        _theoryTest.LessonId.ToString(), TheoryTestFolderName, _test.ID.ToString() + Path.GetExtension(_test.PicturePath));
                if (_test.PicturePath != picturePath)
                {
                    var filePath = Path.Combine(theoryTestFolder, _test.ID.ToString() + Path.GetExtension(_test.PicturePath));
                    if (File.Exists(filePath))
                        File.Delete(filePath);
                    File.Copy(_test.PicturePath, filePath);

                    _test.PicturePath = picturePath;
                }
            }
            //-------КОНЕЦ ДОБАВЛЕНИЯ-----------------

            var testPath = Path.Combine(theoryTestFolder, _test.ID.ToString() + FileExtension);
            XMLHelper.WriteXMLEncrypt<Test>(_test, testPath);
        }

        /// <summary>
        /// Сохранить практический тест
        /// </summary>
        /// <param name="_test">практический тест</param>
        /// <param name="_practiceTest">описание практического теста</param>
        public PictureTest SavePictureTest(PictureTest _test, PracticeTest _practiceTest)
        {
            var contentFolder = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), ContentFolderName);
            if (!Directory.Exists(contentFolder))
                Directory.CreateDirectory(contentFolder);
            var moduleFolder = Path.Combine(contentFolder, _practiceTest.ModuleId.ToString());
            if (!Directory.Exists(moduleFolder))
                Directory.CreateDirectory(moduleFolder);
            var lessonFolder = Path.Combine(moduleFolder, _practiceTest.LessonId.ToString());
            if (!Directory.Exists(lessonFolder))
                Directory.CreateDirectory(lessonFolder);
            var practiceTestFolder = Path.Combine(lessonFolder, PracticeTestFolderName);

            if (!Directory.Exists(practiceTestFolder))
                Directory.CreateDirectory(practiceTestFolder);

            /*
            var picturePath = Path.Combine(ContentFolderName, _practiceTest.ModuleId.ToString(),
                    _practiceTest.LessonId.ToString(), PracticeTestFolderName, _test.ID.ToString() + Path.GetExtension(_test.PicturePath));
             * */
            var picturePath = Path.Combine(_practiceTest.ModuleId.ToString(),
                    _practiceTest.LessonId.ToString(), PracticeTestFolderName, _test.ID.ToString() + Path.GetExtension(_test.PicturePath));
            if (_test.PicturePath != picturePath)
            {
                var filePath = Path.Combine(practiceTestFolder, _test.ID.ToString() + Path.GetExtension(_test.PicturePath));
                if (File.Exists(filePath))
                    File.Delete(filePath);
                File.Copy(_test.PicturePath, filePath);
                
                _test.PicturePath = picturePath;
            }

            var testPath = Path.Combine(practiceTestFolder, _test.ID.ToString() + FileExtension);
            XMLHelper.WriteXMLEncrypt<PictureTest>(_test, testPath);

            return _test;
        }

        /// <summary>
        /// Удалить модуль
        /// </summary>
        /// <param name="_module">модуль</param>
        public void DeleteModule(Module _module)
        {
            try
            {
                var contentFolder = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), ContentFolderName);
                if (!Directory.Exists(contentFolder))
                    return;

                var moduleFolder = Path.Combine(contentFolder, _module.ID.ToString());
                if (!Directory.Exists(moduleFolder))
                    return;

                Directory.Delete(moduleFolder, true);
            }
            catch (Exception ex)
            {
                LogController.Log.Error(ex);
            }
        }

        /// <summary>
        /// Удалить урок
        /// </summary>
        /// <param name="_lesson">урок</param>
        public void DeleteLesson(Lesson _lesson)
        {
            try
            {
                var contentFolder = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), ContentFolderName);
                if (!Directory.Exists(contentFolder))
                    return;

                var moduleFolder = Path.Combine(contentFolder, _lesson.ModuleID.ToString());
                if (!Directory.Exists(moduleFolder))
                    return;

                var lessonFolder = Path.Combine(moduleFolder, _lesson.ID.ToString());
                if (!Directory.Exists(lessonFolder))
                    return;

                Directory.Delete(lessonFolder, true);
            }
            catch (Exception ex)
            {
                LogController.Log.Error(ex);
            }
        }

        /// <summary>
        /// Удалить тест
        /// </summary>
        /// <param name="_test">тест</param>
        /// <param name="_theoryTest">описание теоретического теста</param>
        public void DeleteTest(Test _test, TheoryTest _theoryTest)
        {
            var contentFolder = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), ContentFolderName);
            if (!Directory.Exists(contentFolder))
                return;
            var moduleFolder = Path.Combine(contentFolder, _theoryTest.ModuleId.ToString());
            if (!Directory.Exists(moduleFolder))
                return;
            var lessonFolder = Path.Combine(moduleFolder, _theoryTest.LessonId.ToString());
            if (!Directory.Exists(lessonFolder))
                return;
            var theoryTestFolder = Path.Combine(lessonFolder, TheoryTestFolderName);

            if (!Directory.Exists(theoryTestFolder))
                return;

            var testPath = Path.Combine(theoryTestFolder, _test.ID.ToString() + FileExtension);
            if (!File.Exists(testPath))
                return;
            File.Delete(testPath);
        }

        /// <summary>
        /// Удалить практический тест
        /// </summary>
        /// <param name="_test">тест</param>
        /// <param name="_practiceTest">описание практического теста</param>
        public void DeletePictureTest(PictureTest _test, PracticeTest _practiceTest)
        {
            var contentFolder = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), ContentFolderName);
            if (!Directory.Exists(contentFolder))
                return;
            var moduleFolder = Path.Combine(contentFolder, _practiceTest.ModuleId.ToString());
            if (!Directory.Exists(moduleFolder))
                return;
            var lessonFolder = Path.Combine(moduleFolder, _practiceTest.LessonId.ToString());
            if (!Directory.Exists(lessonFolder))
                return;
            var practiceTestFolder = Path.Combine(lessonFolder, PracticeTestFolderName);

            if (!Directory.Exists(practiceTestFolder))
                return;

            var testPath = Path.Combine(practiceTestFolder, _test.ID.ToString() + FileExtension);
            if (!File.Exists(testPath))
                return;
            try
            {
                File.Delete(testPath);
            }
            catch(Exception ex)
            {
                LogController.Log.Error(ex);
            }
        }

        /// <summary>
        /// Загрузить все имеющиеся модули
        /// </summary>
        /// <returns>список модулей</returns>
        public List<Module> LoadModules()
        {
            var modules = new List<Module>();

            var contentFolder = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), ContentFolderName);
            if (!Directory.Exists(contentFolder))
                return modules;
            var directorys = Directory.GetDirectories(contentFolder);
            foreach (var dir in directorys)
            {
                var configPath = Path.Combine(contentFolder,dir, ConfigFileName);
                if (File.Exists(configPath))
                {
                    try
                    {
                        var module = XMLHelper.ReadXML<Module>(configPath);
                        modules.Add(module);
                    }
                    catch (Exception ex)
                    {
                        LogController.Log.Error(ex);
                    }
                }
            }

            return modules.OrderBy(x => x.Number).ToList();
        }

        /// <summary>
        /// Загрузить уроки для модуля
        /// </summary>
        /// <param name="_moduleId">идентификатор модуля</param>
        /// <returns>список уроков</returns>
        public List<Lesson> LoadLessons(Guid _moduleId)
        {
            var lessons = new List<Lesson>();

            var contentFolder = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), ContentFolderName);
            if (!Directory.Exists(contentFolder))
                return lessons;
            var moduleFolder = Path.Combine(contentFolder, _moduleId.ToString());
            if (!Directory.Exists(moduleFolder))
                return lessons;
            var directorys = Directory.GetDirectories(moduleFolder);
            foreach (var dir in directorys)
            {
                var configPath = Path.Combine(moduleFolder, dir, ConfigFileName);
                if (File.Exists(configPath))
                {
                    try
                    {
                        var lesson = XMLHelper.ReadXML<Lesson>(configPath);
                        lessons.Add(lesson);
                    }
                    catch (Exception ex)
                    {
                        LogController.Log.Error(ex);
                    }
                }
            }

            return lessons.OrderBy(x => x.Number).ToList();
        }

        /// <summary>
        /// Загрузить видео по уроку
        /// </summary>
        /// <param name="_lesson">урок</param>
        /// <returns>видео</returns>
        public Video LoadVideo(Lesson _lesson)
        {
            var video = Video.GetDefault(_lesson.ID);

            var contentFolder = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), ContentFolderName);
            if (!Directory.Exists(contentFolder))
                return video;
            var moduleFolder = Path.Combine(contentFolder, _lesson.ModuleID.ToString());
            if (!Directory.Exists(moduleFolder))
                return video;
            var lessonFolder = Path.Combine(moduleFolder, _lesson.ID.ToString());
            if (!Directory.Exists(lessonFolder))
                return video;
            var configPath = Path.Combine(lessonFolder, VideoFolderName, ConfigFileName);

            if (File.Exists(configPath))
            {
                try
                {
                    var vd = XMLHelper.ReadXML<Video>(configPath);
                    video = vd;
                }
                catch (Exception ex)
                {
                    LogController.Log.Error(ex);
                }
            }

            return video;
        }

        /// <summary>
        /// Загрузить описание практического теста
        /// </summary>
        /// <param name="_lesson">урок</param>
        /// <returns>описание практического теста</returns>
        public PracticeTest LoadPracticeTest(Lesson _lesson)
        {
            var practiceTest = PracticeTest.GetDefault(_lesson);

            var contentFolder = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), ContentFolderName);
            if (!Directory.Exists(contentFolder))
                return practiceTest;
            var moduleFolder = Path.Combine(contentFolder, _lesson.ModuleID.ToString());
            if (!Directory.Exists(moduleFolder))
                return practiceTest;
            var lessonFolder = Path.Combine(moduleFolder, _lesson.ID.ToString());
            if (!Directory.Exists(lessonFolder))
                return practiceTest;
            var configPath = Path.Combine(lessonFolder, PracticeTestFolderName, ConfigFileName);

            if (File.Exists(configPath))
            {
                try
                {
                    var pt = XMLHelper.ReadXML<PracticeTest>(configPath);
                    practiceTest = pt;
                }
                catch (Exception ex)
                {
                    LogController.Log.Error(ex);
                }
            }

            return practiceTest;
        }

        /// <summary>
        /// Загрузить описание теоретического теста
        /// </summary>
        /// <param name="_lesson">урок</param>
        /// <returns>описание теоретического теста</returns>
        public TheoryTest LoadTheoryTest(Lesson _lesson)
        {
            var theoryTest = TheoryTest.GetDefault(_lesson);

            var contentFolder = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), ContentFolderName);
            if (!Directory.Exists(contentFolder))
                return theoryTest;
            var moduleFolder = Path.Combine(contentFolder, _lesson.ModuleID.ToString());
            if (!Directory.Exists(moduleFolder))
                return theoryTest;
            var lessonFolder = Path.Combine(moduleFolder, _lesson.ID.ToString());
            if (!Directory.Exists(lessonFolder))
                return theoryTest;
            var configPath = Path.Combine(lessonFolder, TheoryTestFolderName, ConfigFileName);

            if (File.Exists(configPath))
            {
                try
                {
                    var tt = XMLHelper.ReadXML<TheoryTest>(configPath);
                    theoryTest = tt;
                }
                catch (Exception ex)
                {
                    LogController.Log.Error(ex);
                }
            }

            return theoryTest;
        }

        /// <summary>
        /// Загрузить тесты
        /// </summary>
        /// <param name="_theoryTest">теоретический тест</param>
        /// <returns>тесты</returns>
        public List<Test> LoadTests(TheoryTest _theoryTest)
        {
            var tests = new List<Test>();

            var contentFolder = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), ContentFolderName);
            if (!Directory.Exists(contentFolder))
                return tests;
            var moduleFolder = Path.Combine(contentFolder, _theoryTest.ModuleId.ToString());
            if (!Directory.Exists(moduleFolder))
                return tests;
            var lessonFolder = Path.Combine(moduleFolder, _theoryTest.LessonId.ToString());
            if (!Directory.Exists(lessonFolder))
                return tests;
            var theoryTestFolder = Path.Combine(lessonFolder, TheoryTestFolderName);
            if (!Directory.Exists(theoryTestFolder))
                return tests;

            var files = Directory.GetFiles(theoryTestFolder).Where(x => Path.GetFileName(x) != ConfigFileName).ToList();

            foreach (var file in files)
            {
                var testPath = Path.Combine(theoryTestFolder, file);
                try
                {
                    var test = XMLHelper.ReadXMLEncrypt<Test>(testPath);
                    tests.Add(test);
                }
                catch (Exception ex)
                {
                    LogController.Log.Error(ex);
                }
            }

            return tests.OrderBy(x => x.Number).ToList();
        }
        
        /// <summary>
        /// Загрузить практические тесты
        /// </summary>
        /// <param name="_practiceTest">описание практическиго теста</param>
        /// <returns>практические тесты</returns>
        public List<PictureTest> LoadPictureTests(PracticeTest _practiceTest)
        {
            var tests = new List<PictureTest>();

            var contentFolder = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), ContentFolderName);
            if (!Directory.Exists(contentFolder))
                return tests;
            var moduleFolder = Path.Combine(contentFolder, _practiceTest.ModuleId.ToString());
            if (!Directory.Exists(moduleFolder))
                return tests;
            var lessonFolder = Path.Combine(moduleFolder, _practiceTest.LessonId.ToString());
            if (!Directory.Exists(lessonFolder))
                return tests;
            var practiceFolder = Path.Combine(lessonFolder, PracticeTestFolderName);
            if (!Directory.Exists(practiceFolder))
                return tests;

            var files = Directory.GetFiles(practiceFolder).Where(x => Path.GetFileName(x) != ConfigFileName && Path.GetExtension(x) == FileExtension).ToList();

            foreach (var file in files)
            {
                var testPath = Path.Combine(practiceFolder, file);
                try
                {
                    var test = XMLHelper.ReadXMLEncrypt<PictureTest>(testPath);
                    tests.Add(test);
                }
                catch (Exception ex)
                {
                    LogController.Log.Error(ex);
                }
            }

            return tests.OrderBy(x => x.Number).ToList();
        }

        public void Dispose()
        {

        }

        /// <summary>
        /// Сохранить статистику локально
        /// </summary>
        /// <param name="_statistic">Статистика</param>
        public void SaveStatistic(TestStatistic _statistic)
        {
            var existStatistic = LoadAllStatistic();

            var appFolder = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            if (!Directory.Exists(appFolder))
                Directory.CreateDirectory(appFolder);
            var statFile = Path.Combine(appFolder, StatisticFileName);

            existStatistic.Add(_statistic);
            if (existStatistic.Count > MAX_LOCAL_STATISTICS_COUNT)
                existStatistic.RemoveAt(0);

            XMLHelper.WriteXMLEncrypt<List<TestStatistic>>(existStatistic, statFile);
        }

        /// <summary>
        /// Загрузить всю имеющуюся локальную статистику
        /// </summary>
        public List<TestStatistic> LoadAllStatistic()
        {
            var statistics = new List<TestStatistic>();

            var appFolder = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            if (!Directory.Exists(appFolder))
                Directory.CreateDirectory(appFolder);
            var statFile = Path.Combine(appFolder, StatisticFileName);
            if (File.Exists(statFile))
            {
                try
                {
                    var stats = XMLHelper.ReadXMLEncrypt<List<TestStatistic>>(statFile);
                    statistics = stats;
                }
                catch (Exception ex)
                {
                    LogController.Log.Error(ex);
                }
            }

            return statistics;
        }

        /// <summary>
        /// Обновить статистику
        /// </summary>
        /// <param name="_statistic">Статистика</param>
        public void UpdateStatistic(TestStatistic _statistic)
        {
            var existStatistic = LoadAllStatistic();

            var appFolder = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            if (!Directory.Exists(appFolder))
                Directory.CreateDirectory(appFolder);
            var statFile = Path.Combine(appFolder, StatisticFileName);

            existStatistic.RemoveAll(x => x.ID == _statistic.ID);
            existStatistic.Add(_statistic);

            XMLHelper.WriteXMLEncrypt<List<TestStatistic>>(existStatistic, statFile);
        }

        /// <summary>
        /// Обистить папку практических тестов от удаленных тестов
        /// </summary>
        /// <param name="_practiceTest">описание практического теста</param>
        public void ClearPictureTests(PracticeTest _practiceTest)
        {
            var tests = new List<PictureTest>();

            var contentFolder = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), ContentFolderName);
            if (!Directory.Exists(contentFolder))
                return;
            var moduleFolder = Path.Combine(contentFolder, _practiceTest.ModuleId.ToString());
            if (!Directory.Exists(moduleFolder))
                return;
            var lessonFolder = Path.Combine(moduleFolder, _practiceTest.LessonId.ToString());
            if (!Directory.Exists(lessonFolder))
                return;
            var practiceFolder = Path.Combine(lessonFolder, PracticeTestFolderName);
            if (!Directory.Exists(practiceFolder))
                return;

            var d = new Guid();
            var files = Directory.GetFiles(practiceFolder)
                .Where(x => Path.GetFileName(x) != ConfigFileName && Guid.TryParse(Path.GetFileNameWithoutExtension(x), out d));
            files = files.Where(x => !files.Any(y => Path.GetFileNameWithoutExtension(x) == Path.GetFileNameWithoutExtension(y) && Path.GetExtension(y) == FileExtension))
                .ToList();

            foreach (var file in files)
            {
                try
                {
                    File.Delete(file);
                }
                catch (Exception ex)
                {
                    LogController.Log.Error(ex);
                }
            }
        }
    }
}
