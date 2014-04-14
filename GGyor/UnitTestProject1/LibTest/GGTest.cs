using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject1.LibTest
{
    [TestClass]
    public class GGTest
    {
        //[TestMethod]
        public void GetProductsTest()
        {
            Assert.Fail();
            return;

            if (TestClassBase.checkDoTest())
                return;

            var service = new GGLib.GGProductService();

            service.GetAllProducts();

            Assert.IsTrue(true);
        }

        //[TestMethod]
        public void UpdateProductsTest()
        {
            Assert.Fail();
            return; ;

            if (TestClassBase.checkDoTest())
                return;

            var service = new GGLib.GGProductService();

            service.UpdateProducts();

            Assert.IsTrue(true);

        }

        //[TestMethod]
        public void TestTest()
        {
            Assert.Fail();
            return;
            if (TestClassBase.checkDoTest())
                return;

            var service = new GGLib.GGProductService();

            var product = service.GetProduct("aralgm_8692750070192");

            Assert.IsTrue(true);

        }
    }
}
