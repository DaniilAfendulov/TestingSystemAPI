using System.Windows;

namespace Common.Data.Content
{
    /// <summary>
    /// Описание выделенной области на картинке практического теста
    /// </summary>
    public class Selection
    {
        /// <summary>
        /// Координаты верхнего левого угла. Смещение относительно ширины и высоты основной картинки в % (от 0 до 1)
        /// </summary>
        public Point StartPos { get; set; }

        /// <summary>
        /// Размер выделения. Относительно ширины и высоты основной картинки в % (от 0 до 1)
        /// </summary>
        public Size RectSize { get; set; }

        /// <summary>
        /// Тип выделенной области
        /// </summary>
        public SelectionType Type { get; set; }

        /// <summary>
        /// Текст, который необходимо ввести в текстбокс
        /// </summary>
        public string InputText { get; set; }
    }
}
