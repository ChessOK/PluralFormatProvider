namespace Common.I18n.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using System;
    using System.Globalization;

    [TestClass]
    public class CommonPluralTests
    {
        private readonly CultureInfo _culture = CultureInfo.GetCultureInfo("en-US");

        [TestMethod]
        public void ShouldAcceptNegativeNumbers()
        {
            Assert.AreEqual("book", Plural.Format(_culture, "{0:#book;books}", -1));
        }

        [TestMethod]
        public void ShouldReturnNotFormattableStringAsIs()
        {
            const string sampleString = "Sample string";

            Assert.AreEqual(sampleString, Plural.Format(sampleString));
        }

        [TestMethod]
        public void ShouldNotCauseExceptionWithEmptyForms()
        {
            Assert.AreEqual(String.Empty, Plural.Format("{0:;}", 0));
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void ShouldThrowExceptionWhenTooManyForms()
        {
            Plural.Format(_culture, "{0:#book;books;boooks}", 0);
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void ShouldThrowExceptionWhenNotEnoughForms()
        {
            Plural.Format(_culture, "{0:#book}", 0);
        }
    }
}
