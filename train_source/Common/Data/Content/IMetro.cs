using System;

namespace Common.Data.Content
{
    /// <summary>
    /// Интерфейс для элементов, которые можно отображать в стиле Metro
    /// </summary>
    public interface IMetro
    {
        /// <summary>
        /// Идентификатор элемента
        /// </summary>
        Guid ID { get; set; }

        /// <summary>
        /// Название
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Описание
        /// </summary>
        string Description { get; set; }

        /// <summary>
        /// Номер
        /// </summary>
        int Number { get; set; }
    }
}
