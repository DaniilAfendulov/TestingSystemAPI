using System;
using System.Collections.Generic;

namespace Common.Data.Content
{
    /// <summary>
    /// Описывает практический тест
    /// </summary>
    public class PictureTest
    {
        /// <summary>
        /// Идентификатор теста
        /// </summary>
        public Guid ID { get; set; }

        /// <summary>
        /// Идентификатор практического теста, к которому относится этот вопрос
        /// </summary>
        public Guid PracticeTestID { get; set; }

        /// <summary>
        /// Номер вопроса
        /// </summary>
        public int Number { get; set; }

        /// <summary>
        /// Описание теста
        /// </summary>
        public string MainQuestion { get; set; }

        /// <summary>
        /// Вопрос
        /// </summary>
        public string Question { get; set; }

        /// <summary>
        /// Путь к файлу картинки
        /// </summary>
        public string PicturePath { get; set; }

        /// <summary>
        /// Выделеные области, куда можно "тыкать"
        /// </summary>
        public List<Selection> Selections { get; set; }
    }
}
