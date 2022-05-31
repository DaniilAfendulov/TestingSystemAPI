using System;
using System.Runtime.Serialization;

namespace Common.Data.Statistics
{
    /// <summary>
    /// Описание статистики по прохождению студентами тестов
    /// </summary>
    [DataContract]
    public class TestStatistic
    {
        /// <summary>
        /// Идентификатор записи о статистике
        /// </summary>
        [DataMember]
        public virtual Guid ID { get; set; }

        /// <summary>
        /// Идентификатор студента
        /// </summary>
        [DataMember]
        public virtual Guid StudentId { get; set; }

        /// <summary>
        /// ФИО студента
        /// </summary>
        [DataMember]
        public virtual string StudentName { get; set; }

        /// <summary>
        /// Идентификатор теста (как практического так и теоретического)
        /// </summary>
        [DataMember]
        public virtual Guid TestId { get; set; }

        /// <summary>
        /// Результат прохождения теста в процентах (-1 если тест ещё не закончен)
        /// </summary>
        [DataMember]
        public virtual int Result { get; set; }
    }
}
