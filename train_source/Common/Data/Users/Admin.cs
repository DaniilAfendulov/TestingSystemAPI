using System;

namespace Common.Data.Users
{
    /// <summary>
    /// Описание администратора
    /// </summary>
    public class Admin
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public virtual Guid ID { get; set; }

        /// <summary>
        /// Логин
        /// </summary>
        public virtual string Login { get; set; }

        /// <summary>
        /// Пароль (хранится в виде хэша)
        /// </summary>
        public virtual string Password { get; set; }

        /// <summary>
        /// Признак активности
        /// </summary>
        public virtual bool IsOnline { get; set; }

        public override string ToString()
        {
            return Login;
        }
    }
}
