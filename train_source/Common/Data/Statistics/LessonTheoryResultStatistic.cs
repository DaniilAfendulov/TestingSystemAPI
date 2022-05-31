using System.Runtime.Serialization;

namespace Common.Data.Statistics
{
    /// <summary>
    /// Статистика по теоретическому тесту для студента
    /// </summary>
    [DataContract]
    public class LessonTheoryResultStatistic
    {
        /// <summary>
        /// Лучший результат
        /// </summary>
        [DataMember]
        public int BestTheoryResult { get; set; }

        ///// <summary>
        ///// Количество попыток
        ///// </summary>
        //[DataMember]
        //public int AttemptsCount { get; set; }

        /// <summary>
        /// Флаг наличия статистики по теоретическому тесту урока
        /// </summary>
        [DataMember]
        public bool HasTheoryStatistic { get; set; }

        public override string ToString()
        {
            return HasTheoryStatistic ? BestTheoryResult.ToString() : "-";
        }
    }
}
