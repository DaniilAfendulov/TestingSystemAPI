using System;
using System.Collections.Generic;

namespace Common.Data.Content
{
    /// <summary>
    /// Класс описывающий отдельный вопрос теоретического теста
    /// </summary>
    public class Test
    {
        public Test()
        {
            CorrectAnswers = new List<string>();
            WrongAnswers = new List<string>();
        }

        /// <summary>
        /// Идентификатор теста
        /// </summary>
        public Guid ID { get; set; }

        /// <summary>
        /// Идентификатор теоретического теста, к которому относится этот вопрос
        /// </summary>
        public Guid TheoryTestID { get; set; }

        /// <summary>
        /// Номер вопроса
        /// </summary>
        public int Number { get; set; }

        /// <summary>
        /// Тип теста
        /// </summary>
        public TheoryTestType Type { get; set; }

        /// <summary>
        /// Вопрос
        /// </summary>
        public string Question { get; set; }

        /// <summary>
        /// Путь к файлу картинки
        /// </summary>
        public string PicturePath { get; set; }

        /// <summary>
        /// Правильные ответы
        /// </summary>
        public List<string> CorrectAnswers { get; set; }

        /// <summary>
        /// Неправильные ответы
        /// </summary>
        public List<string> WrongAnswers { get; set; }

        /// <summary>
        /// Признак того что ответ был дан
        /// </summary>
        public bool IsAnswered { get; set; }

        /// <summary>
        /// Правильно ли был дан ответ на тест
        /// </summary>
        public bool TestResult { get; set; }
    }
}
