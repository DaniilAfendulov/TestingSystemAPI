using Common.Data.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestingSystemAPI.Models
{
    public class LessonModel
    {
        public Guid ID { get; set; }

        /// <summary>
        /// ID Модуля
        /// </summary>
        public Guid ModuleID { get; set; }

        /// <summary>
        /// Название урока
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Описание урока
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Номер урока
        /// </summary>
        public int Number { get; set; }

        public LessonModel(Lesson lesson)
        {
            ID = lesson.ID;
            ModuleID = lesson.ModuleID;
            Title = lesson.Name;
            Description = lesson.Description;
            Number = lesson.Number;
        }
    }
}
