namespace Common.I18n
{
    using System;
    using System.Globalization;

    /// <summary>
    /// Класс-обертка для <see cref="PluralFormatProvider"/>.
    /// </summary>
    public static class Plural
    {
        private static PluralFormatProvider GetProvider(CultureInfo culture)
        {
            return new PluralFormatProvider(culture);
        }

        /// <summary>
        /// Форматирует строку, используя <see cref="PluralFormatProvider"/> с указанным языком
        /// </summary>
        /// <param name="culture">Используемый язык</param>
        /// <param name="format">Форматируемая строка</param>
        /// <param name="arg">Аргумент</param>
        /// <returns>Строка с правильной множественной формой</returns>
        public static string Format(CultureInfo culture, String format, object arg)
        {
            return String.Format(GetProvider(culture), format, arg);
        }

        /// <summary>
        /// Форматирует строку, используя <see cref="PluralFormatProvider"/>
        /// </summary>
        /// <param name="format">Форматируемая строка</param>
        /// <param name="arg">Аргумент</param>
        /// <returns>Строка с правильной множественной формой</returns>
        public static string Format(String format, object arg)
        {
            return String.Format(GetProvider(CultureInfo.CurrentUICulture), format, arg);
        }

        /// <summary>
        /// Форматирует строку, используя <see cref="PluralFormatProvider"/>
        /// </summary>
        /// <param name="format">Форматируемая строка</param>
        /// <param name="args">Аргументы</param>
        /// <returns>Строка с правильной множественной формой</returns>
        public static string Format(String format, params object[] args)
        {
            return String.Format(GetProvider(CultureInfo.CurrentUICulture), format, args);
        }
    }
}
