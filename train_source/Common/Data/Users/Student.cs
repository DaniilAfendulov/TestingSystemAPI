using System;
using System.Runtime.Serialization;

namespace Common.Data.Users
{
    /// <summary>
    /// Описание студента
    /// </summary>
    [DataContract]
    public class Student
    {
        /// <summary>
        /// Идентификатор студента
        /// </summary>
        [DataMember]
        public virtual Guid ID { get; set; }

        /// <summary>
        /// ФИО
        /// </summary>
        [DataMember]
        public virtual string Name { get; set; }

        /// <summary>
        /// Идентификатор группы
        /// </summary>
        [DataMember]
        public virtual Guid GroupId { get; set; }

        /// <summary>
        /// Признак активности
        /// </summary>
        [DataMember]
        public virtual bool IsOnline { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
