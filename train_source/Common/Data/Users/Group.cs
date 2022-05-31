using System;
using System.Runtime.Serialization;

namespace Common.Data.Users
{
    /// <summary>
    /// Описание группы
    /// </summary>
    [DataContract]
    public class Group
    {
        /// <summary>
        /// Идентификатор группы
        /// </summary>
        [DataMember]
        public virtual Guid ID { get; set; }

        /// <summary>
        /// Название группы
        /// </summary>
        [DataMember]
        public virtual string Name { get; set; }

        /// <summary>
        /// Идентификатор преподавателя
        /// </summary>
        [DataMember]
        public virtual Guid TeacherId { get; set; }

        public override string ToString()
        {
            return Name;
        }

        /// <summary>
        /// Получить группу для локальных пользователей
        /// </summary>
        public static Group GetLocalGroup()
        {
            return new Group
            {
                Name = "Локальные пользователи"
            };
        }
    }
}
