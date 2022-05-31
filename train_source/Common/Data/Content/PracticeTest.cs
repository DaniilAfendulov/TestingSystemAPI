using System;
using System.Runtime.Serialization;

namespace Common.Data.Content
{
    /// <summary>
    /// Описание общей структуры практического теста (но не самих тестов)
    /// </summary>
    [DataContract]
    public class PracticeTest : IMetro
    {
        /// <summary>
        /// ID Приктического теста
        /// </summary>
        [DataMember]
        public Guid ID { get; set; }

        /// <summary>
        /// ID Модуля
        /// </summary>
        [DataMember]
        public Guid ModuleId { get; set; }

        /// <summary>
        /// ID Урока
        /// </summary>
        [DataMember]
        public Guid LessonId { get; set; }

        /// <summary>
        /// Название
        /// </summary>
        [DataMember]
        public string Name { get; set; }

        /// <summary>
        /// Описание
        /// </summary>
        [DataMember]
        public string Description { get; set; }

        /// <summary>
        /// Номер
        /// </summary>
        [DataMember]
        public int Number
        {
            get
            {
                return 1;
            }
            set
            {
            }
        }

        /// <summary>
        /// Получить описание отсутствующего практического теста
        /// </summary>
        /// <param name="_lesson">ID урока</param>
        /// <returns>Описание практического теста</returns>
        public static PracticeTest GetDefault(Lesson _lesson)
        {
            return new PracticeTest
            {
                Name = "Практический тест",
                Description = "Практический тест недоступен",
                LessonId = _lesson.ID,
                ModuleId = _lesson.ModuleID,
                ID = Guid.Empty
            };
        }
    }
}
