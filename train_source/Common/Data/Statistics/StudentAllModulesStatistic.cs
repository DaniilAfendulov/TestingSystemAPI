using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Common.Data.Statistics
{
    /// <summary>
    /// Статистика прохождения модулей студентом
    /// </summary>
    [DataContract]
    public class StudentAllModulesStatistic
    {
        public StudentAllModulesStatistic()
        {
            LessonsStatistic = new Dictionary<int, LessonTheoryResultStatistic>();
            /// PracticeStatistic = new ModulePracticeResultStatistic();
            PracticeStatistic = new Dictionary<int, ModulePracticeResultStatistic>();
        }

        /// <summary>
        /// Имя студента
        /// </summary>
        [DataMember]
        public string StudentName { get; set; }

        /// <summary>
        /// Название модуля
        /// </summary>
        [DataMember]
        public string ModuleName { get; set; }

        /// <summary>
        /// Прохождение практического теста
        /// </summary>
        /// [DataMember]
        /// public ModulePracticeResultStatistic PracticeStatistic { get; set; }

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
        /// Результаты по урокам в виде лучшего теоретического результата по каждому из уроков
        /// </summary>
        [DataMember]
        public Dictionary<int, LessonTheoryResultStatistic> LessonsStatistic { get; set; }

        /// <summary>
        /// Результаты по урокам в виде лучшего практического результата по каждому из уроков
        /// </summary>
        [DataMember]
        public Dictionary<int, ModulePracticeResultStatistic> PracticeStatistic { get; set; }

        /// <summary>
        /// Итоговый результат - средний процент
        /// </summary>
        [DataMember]
        public double MeanTotal { get; set; }

        /// <summary>
        /// Итоговый результат в бинарном виде
        /// </summary>
        [DataMember]
        public bool TotalBool { get; set; }
    }
}
