namespace Common.I18n.Tests
{
    using System.Collections.Generic;
    using System.Globalization;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Common.I18n;

    public class PluralTests
    {
        protected readonly CultureInfo _culture;

        protected readonly string _formatString;

        public PluralTests(CultureInfo culture, string formatString)
        {
            _culture = culture;
            _formatString = formatString;
        }

        protected void AssertList(IEnumerable<int> list, string expected)
        {
            foreach (var item in list)
            {
                Assert.AreEqual(expected, Plural.Format(_culture, _formatString, item));
            }
        }

        protected void AssertList(IEnumerable<double> list, string expected)
        {
            foreach (var item in list)
            {
                Assert.AreEqual(expected, Plural.Format(_culture, _formatString, item));
            }
        }
    }
}
