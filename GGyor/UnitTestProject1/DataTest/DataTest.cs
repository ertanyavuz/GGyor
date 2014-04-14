using GGLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StorMan.Data.Repositories;

namespace UnitTestProject1.DataTest
{
    [TestClass]
    public class DataTest
    {
        //[TestMethod]
        public void ProductServiceTest()
        {
            if (TestClassBase.checkDoTest())
                return;

            var ps = new GGProductService();
            ps.GetActiveProducts();
            Assert.IsTrue(true);
        }
        //[TestMethod]
        public void CategoryServiceTest()
        {
            TestClassBase.checkDoTest();            var cs = new GGCategoryService();            var list = cs.getCategories();            Assert.IsTrue(list.Count > 0);        }

        //[TestMethod]
        public void XmlParserTest()        {
            TestClassBase.checkDoTest();            var cs = new GGCategoryService();            cs.XmlParser();            Assert.IsTrue(true);        }

        //[TestMethod]
        public void FillCategories()
        {
            TestClassBase.checkDoTest();

            var cs = new GGCategoryService();
            var list = cs.getCategories();

            var repo = new CategoryRepository();
            var result = repo.SyncCategories(list);
            Assert.IsTrue(result);
        }

    }
}
