using System;
using System.Runtime.Serialization;

namespace Common.Data.Statistics
{
    /// <summary>
    /// Описание статистики по сложности урока
    /// </summary>
    [DataContract]
    public class DifficultyStatistic
    {
        /// <summary>
        /// Идентификатор теста
        /// </summary>
        [DataMember]
        public Guid TestId { get; set; }

        /// <summary>
        /// Средний результат прохождения
        /// </summary>
        [DataMember]
        public double MeanResult { get; set; }

        /// <summary>
        /// Среднее количество попыток
        /// </summary>
        [DataMember]
        public double MeanAttemptsCount { get; set; }

        /// <summary>
        /// Количество студентов которые проходили этот тест
        /// </summary>
        [DataMember]
        public int StudentsCount { get; set; }
    }
}
