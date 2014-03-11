using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using N11Lib;

namespace UnitTestProject1.LibTest
{
    [TestClass]
    public class N11LibTest
    {
        [TestMethod]
        public void GetCategoriesTest()
        {
            var target = new N11Service();

            target.GetCategories();
            
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void GetProductsTest()
        {
            var target = new N11Service();

            target.GetProducts();

            Assert.IsTrue(true);
        }


        [TestMethod]
        public void GetProductsJsonTest()
        {
            var target = new N11Service();

            target.GetProductsJson();

            Assert.IsTrue(true);
        }

        [TestMethod]
        public void GetSourceProductsXmlTest()
        {
            var target = new N11Service();

            target.GetSourceProductsXml();

            Assert.IsTrue(true);
        }

        [TestMethod]
        public void UpdateProductsTest()
        {
            var target = new N11Service();

            target.UpdateProducts();

            Assert.IsTrue(true);
        }

        [TestMethod]
        public void CallPostTest()
        {
            var target = new N11Service();

            //target.CallPost();

            Assert.IsTrue(true);
        }
    }
}
