using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Common.I18n.Tests
{
    using System.Globalization;

    [TestClass]
    public class RussianPluralTests : PluralTests
    {
        public RussianPluralTests()
            : base(CultureInfo.GetCultureInfo("ru-RU"), "{0:#год;года;лет;года}")
        {
        }

        [TestMethod]
        public void OneCategory()
        {
            var list = new List<int> { 1, 21, 31, 41, 51, 61 };
            AssertList(list, "год");
        }

        [TestMethod]
        public void FewCategory()
        {
            var list = new List<int> { 2, 3, 4, 22, 24, 32, 34 };
            AssertList(list, "года");
        }

        [TestMethod]
        public void ManyCategory()
        {
            AssertList(new List<int> { 0, 6, 10, 15, 27, 30, 38 }, "лет");
        }

        [TestMethod]
        public void OtherCategory()
        {
            AssertList(new List<double> { 1.2, 2.07, 5.94 }, "года");
        }

        
    }
}
