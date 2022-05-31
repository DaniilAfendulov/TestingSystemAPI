using System;

namespace Common.Data.Content
{
    /// <summary>
    /// Учебный модуль
    /// </summary>
    public class Module : IMetro
    {
        /// <summary>
        /// ID Модуля
        /// </summary>
        public Guid ID { get; set; }

        /// <summary>
        /// Название модуля
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Описание модуля
        /// </summary>
        public string Description { get; set; }

        public override string ToString()
        {
            return Name;
        }

        /// <summary>
        /// Номер модуля
        /// </summary>
        public int Number { get; set; }
    }
}
