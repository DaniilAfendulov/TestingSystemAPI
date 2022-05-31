using System.Runtime.Serialization;

namespace Common.Data.Statistics
{
    /// <summary>
    /// Статистика по уроку для студента
    /// </summary>
    [DataContract]
    public class LessonResultStatistic
    {
        /// <summary>
        /// Лучший результат
        /// </summary>
        [DataMember]
        public int BestTheoryResult { get; set; }

        /// <summary>
        /// Результат прохождения практического теста
        /// </summary>
        [DataMember]
        public int PracticeResult { get; set; }

        /// <summary>
        /// Количество попыток
        /// </summary>
        [DataMember]
        public int AttemptsCount { get; set; }

        /// <summary>
        /// Флаг наличия статистики по теоретическому тесту урока
        /// </summary>
        [DataMember]
        public bool HasTheoryStatistic { get; set; }

        /// <summary>
        /// Флаг наличия статистики по практическому тесту урока
        /// </summary>
        [DataMember]
        public bool HasPracticeStatistic { get; set; }

        public override string ToString()
        {
            return string.Format("{0}/{1}/{2}",
                (HasPracticeStatistic && PracticeResult > 0) ? "+" : "-",     // было HasPracticeStatistic ? PracticeResult.ToString() : "-",
                HasTheoryStatistic ? BestTheoryResult.ToString() : "-",
                HasTheoryStatistic ? AttemptsCount.ToString() : "-");
        }
    }
}
