using System.Runtime.Serialization;

namespace Common.Data.Statistics
{
    /// <summary>
    /// Статистика по практическому тесту для студента
    /// </summary>
    [DataContract]
    public class ModulePracticeResultStatistic
    {
        /// <summary>
        /// Результат прохождения практического теста
        /// </summary>
        [DataMember]
        public int PracticeResult { get; set; }

        /// <summary>
        /// Флаг наличия статистики по практическому тесту урока
        /// </summary>
        [DataMember]
        public bool HasPracticeStatistic { get; set; }

        public override string ToString()
        {
            return (HasPracticeStatistic && PracticeResult > 0) ? "+" : "-";     // было HasPracticeStatistic ? PracticeResult.ToString() : "-",
        }
    }
}
