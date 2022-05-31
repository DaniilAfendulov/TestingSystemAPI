using System;
using System.Runtime.Serialization;

namespace Common.Data.Users
{
    /// <summary>
    /// Описание преподавателя
    /// </summary>
    [DataContract]
    public class Teacher
    {
        /// <summary>
        /// Идентификатор преподавателя
        /// </summary>
        [DataMember]
        public virtual Guid ID { get; set; }
        
        /// <summary>
        /// ФИО преподавателя
        /// </summary>
        [DataMember]
        public virtual string Name { get; set; }

        /// <summary>
        /// Логин
        /// </summary>
        [DataMember]
        public virtual string Login { get; set; }

        /// <summary>
        /// Пароль (хранится в виде хэша)
        /// </summary>
        [DataMember]
        public virtual string Password { get; set; }

        /// <summary>
        /// Признак активности
        /// </summary>
        [DataMember]
        public virtual bool IsOnline { get; set; }

        public override string ToString()
        {
            return Login;
        }
    }
}
