using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Common.Data.Statistics
{
    /// <summary>
    /// Статистика прохождения модуля студентом
    /// </summary>
    [DataContract]
    public class StudentModuleStatistic
    {
        public StudentModuleStatistic()
        {
            LessonsStatistic = new Dictionary<Guid, LessonResultStatistic>();
        }

        /// <summary>
        /// Имя студента
        /// </summary>
        [DataMember]
        public string StudentName { get; set; }

        /// <summary>
        /// ФИО студента без группы
        /// </summary>
        [DataMember]
        public virtual string StudentNameWOGroup { get; set; }

        /// <summary>
        /// ФИО студента только группа
        /// </summary>
        [DataMember]
        public virtual string StudentNameGroupOnly { get; set; }

        /// <summary>
        /// Результаты по урокам в виде (&lt;лучший результат&gt;/&lt;количество попыток&gt;)
        /// </summary>
        [DataMember]
        public Dictionary<Guid, LessonResultStatistic> LessonsStatistic { get; set; }

        /// <summary>
        /// Итоговый результат
        /// </summary>
        [DataMember]
        public double Total { get; set; }

        /// <summary>
        /// Итоговый результат в бинарном виде
        /// </summary>
        [DataMember]
        public bool TotalBool { get; set; }
    }
}
