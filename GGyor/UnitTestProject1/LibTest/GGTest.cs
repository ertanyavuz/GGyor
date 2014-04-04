using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject1.LibTest
{
    [TestClass]
    public class GGTest
    {
        [TestMethod]
        public void GetProductsTest()
        {
            if (TestClassBase.checkDoTest())
                return;

            var service = new GGLib.GGProductService();

            service.GetAllProducts();

            Assert.IsTrue(true);
        }

        [TestMethod]
        public void UpdateProductsTest()
        {
            if (TestClassBase.checkDoTest())
                return;

            var service = new GGLib.GGProductService();

            service.UpdateProducts();

            Assert.IsTrue(true);
        }
    }
}
