using System;
using System.Runtime.Serialization;

namespace Common.Data.Content
{
    /// <summary>
    /// Описание общей структуры теоретического теста (но не самих тестов)
    /// </summary>
    [DataContract]
    public class TheoryTest : IMetro
    {
        /// <summary>
        /// ID Теоретического теста
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
                return 2;
            }
            set
            {
            }
        }

        /// <summary>
        /// Получить описание отсутствующего теоретического теста
        /// </summary>
        /// <param name="_lesson">ID урока</param>
        /// <returns>Описание теоретического теста</returns>
        public static TheoryTest GetDefault(Lesson _lesson)
        {
            return new TheoryTest
            {
                Name = "Теоретический тест",
                Description = "Теоретический тест недоступен",
                LessonId = _lesson.ID,
                ModuleId = _lesson.ModuleID,
                ID = Guid.Empty
            };
        }
    }
}
