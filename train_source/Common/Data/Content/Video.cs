using System;

namespace Common.Data.Content
{
    /// <summary>
    /// Описание Видео
    /// </summary>
    public class Video : IMetro
    {
        /// <summary>
        /// Идентификатор видео
        /// </summary>
        public Guid ID { get; set; }

        /// <summary>
        /// ID Урока
        /// </summary>
        public Guid LessonId { get; set; }

        /// <summary>
        /// Название
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Описание
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Путь к файлу видео
        /// </summary>
        public string FilePath { get; set; }

        /// <summary>
        /// Получить описание отсутствующего видео
        /// </summary>
        /// <param name="_lessonId">ID урока</param>
        /// <returns>Описание видео</returns>
        public static Video GetDefault(Guid _lessonId)
        {
            return new Video
            {
                Name = "Видео",
                Description = "Видео недоступно",
                LessonId = _lessonId,
                ID = Guid.Empty
            };
        }

        /// <summary>
        /// Номер
        /// </summary>
        public int Number
        {
            get
            {
                return 0;
            }
            set
            {
            }
        }
    }
}
