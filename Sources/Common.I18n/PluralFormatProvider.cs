namespace Common.I18n
{
    using System;
    using System.Globalization;

    /// <summary>
    /// Форматирует строку путем выбора нужной множественной формы слова из строки-формата.
    /// Правила выбора множественной формы зависят от указанного языка.
    /// </summary>
    /// <remarks>Правила выбора форм: http://unicode.org/repos/cldr-tmp/trunk/diff/supplemental/language_plural_rules.html
    /// </remarks>
    /// <example>
    /// <code>
    ///   var pluralFormatter = new PluralFormatProvider(CultureInfo.GetCulture("ru-RU");
    ///   var formatted = String.Format(pluralFormatter, "{0} {0:#год:года;лет;года}", 5);
    ///   Assert.AreEqual("5 лет", formatted);
    /// 
    ///   formatted = String.Format(pluralFormatter, "{0} {0:#огурец;огурца;огурцов;огурца}", 3);
    ///   Assert.AreEqual("3 огурца", formatted);
    /// </code>
    /// </example>
    public class PluralFormatProvider : IFormatProvider, ICustomFormatter
    {
        private readonly PluralRules _rules;

        /// <summary>
        /// Создает экземпляр PluralFormatProvider с заданным языком.
        /// </summary>
        /// <param name="cultureInfo">Нужный языковой стандарт</param>
        public PluralFormatProvider(CultureInfo cultureInfo)
        {
            _rules = new PluralRules(cultureInfo);
        }

        /// <summary>
        /// Создает экземпляр PluralFormatProvider. В качестве языка используется 
        /// значение <see cref="CultureInfo.CurrentUICulture"/>
        /// </summary>
        public PluralFormatProvider()
            : this(CultureInfo.CurrentUICulture)
        {
        }

        public object GetFormat(Type formatType)
        {
            return this;
        }

        /// <summary>
        /// Возвращает отформатированную строку.
        /// Если строка формата начиналась с символа '#', то к ней применяются
        /// правила выбора множественной формы в соответствие с указанным аргументом.
        /// В противном случае используется формат по-умолчанию.
        /// </summary>
        /// <param name="format">Форматируемая строка</param>
        /// <param name="arg">Значение аргумента</param>
        /// <param name="formatProvider"></param>
        /// <returns>Форматированная строка</returns>
        public string Format(string format, object arg, IFormatProvider formatProvider)
        {
            if (String.IsNullOrEmpty(format))
            {
                return String.Format(CultureInfo.CurrentCulture, "{0}", arg);
            }

            // Нужно ли применить стандартный форматтер?
            if (!format.StartsWith("#", StringComparison.Ordinal))
            {
                var formattableArg = arg as IFormattable;

                if (formattableArg != null)
                {
                    return formattableArg.ToString(format, formatProvider);
                }

                return arg != null ? arg.ToString() : null;
            }

            var forms = format.Remove(0, 1).Split(';');

            if (forms.Length != _rules.RulesCount)
            {
                throw new FormatException(String.Format(
                    CultureInfo.InvariantCulture, 
                    "Incorrect number of plural forms in format string \"{0}\". Given: {1}, needed: {2}", 
                    format,
                    forms.Length,
                    _rules.RulesCount));
            }

            var index = _rules.GetIndex(arg);

            return forms[index];
        }
    }
}