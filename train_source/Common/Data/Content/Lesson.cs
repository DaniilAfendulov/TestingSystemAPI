using System;
using System.Runtime.Serialization;

namespace Common.Data.Content
{
    /// <summary>
    /// Урок
    /// </summary>
    [DataContract]
    public class Lesson : IMetro
    {
        /// <summary>
        /// ID Урока
        /// </summary>
        [DataMember]
        public Guid ID { get; set; }

        /// <summary>
        /// ID Модуля
        /// </summary>
        [DataMember]
        public Guid ModuleID { get; set; }

        /// <summary>
        /// Название урока
        /// </summary>
        [DataMember]
        public string Name { get; set; }

        /// <summary>
        /// Описание урока
        /// </summary>
        [DataMember]
        public string Description { get; set; }

        /// <summary>
        /// Номер урока
        /// </summary>
        [DataMember]
        public int Number { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
