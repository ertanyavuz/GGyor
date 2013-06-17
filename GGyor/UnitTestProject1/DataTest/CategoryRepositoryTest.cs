using Microsoft.VisualStudio.TestTools.UnitTesting;
using StorMan.Data.Repositories;

namespace UnitTestProject1.DataTest
{
    [TestClass]
    public class CategoryRepositoryTest
    {
        [TestMethod]
        public void GetCategoryListTest()
        {
            var repo = new CategoryRepository();
            var list = repo.GetCategoryList();

            Assert.IsTrue(list.Count > 0);
        }
    }
}

