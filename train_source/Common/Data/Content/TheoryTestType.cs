
namespace Common.Data.Content
{
    /// <summary>
    /// Тип теоретического теста
    /// </summary>
    public enum TheoryTestType
    {
        /// <summary>
        /// Тест где надо вводить ответ
        /// </summary>
        InputTest,

        /// <summary>
        /// Тест 1 из 4-х
        /// </summary>
        RadioTest,

        /// <summary>
        /// Тест где может быть много правильных ответов
        /// </summary>
        CheckBoxTest,

        /// <summary>
        /// Тест 1 из списка
        /// </summary>
        ComboBoxTest
    }
}
