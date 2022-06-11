using Common.Data.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Controllers;

namespace TestingSystemAPI.Models
{
    public class LessonInfoDTO
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

        public bool IsVideoDisabled { get; set; }
        public bool IsPracticeDisabled { get; set; }
        public bool IsTheoryDisabled { get; set; }
    }
}
