using System.Collections.Generic;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Common.I18n.Tests
{
    using System.Globalization;

    [TestClass]
    public class EnglishPluralTests : PluralTests
    {
        public EnglishPluralTests()
            : base(CultureInfo.GetCultureInfo("en-US"), "{0:#book;books}")
        {
        }

        [TestMethod]
        public void OneCategory()
        {
            AssertList(new List<int> { 1 }, "book");
        }

        [TestMethod]
        public void OtherCategory()
        {
            AssertList(new List<int> { 0, 2, 500 }, "books");
            AssertList(new List<double> { 1.2, 2.07 }, "books");
        }
    }
}
