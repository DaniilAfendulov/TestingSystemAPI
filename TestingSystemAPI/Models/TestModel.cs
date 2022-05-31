using Common.Data.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestingSystemAPI.Models
{
    public class TestModel
    {

        /// <summary>
        /// Идентификатор теста
        /// </summary>
        public Guid ID { get; set; }

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
        
        public string[] Answers { get; set; }

        public TestModel(Test test)
        {
            if (test.Type == TheoryTestType.InputTest)
            {
                Answers = null;
            }
            else Answers = MixAnswers(test.CorrectAnswers.Concat(test.WrongAnswers).ToList());

            ID = test.ID;
            Number = test.Number;
            Type = test.Type;
            Question = test.Question;
            PicturePath = test.PicturePath;

        }

        private static string[] MixAnswers(List<string> answers)
        {
            Random random = new Random();
            int length = answers.Count();
            string[] result = new string[length];
            for (; length > 0; length--)
            {
                int index = random.Next(length-1);
                result[length-1] = answers[index];
                answers.RemoveAt(index);
            }
            return result;
        }
    }
}
