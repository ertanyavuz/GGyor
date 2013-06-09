using System;
using GGLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void ProductServiceTest()
        {
            var ps = new GGProductService();
            ps.GetProducts();

            Assert.IsTrue(true);

        }

        [TestMethod]
        public void CategoryServiceTest()
        {
            var cs = new GGCategoryService();
            cs.getCategories();

            Assert.IsTrue(true);

        }

        [TestMethod]
        public void XmlParserTest()
        {
            var cs = new GGCategoryService();
            cs.XmlParser();

            Assert.IsTrue(true);
        }
    }
}
