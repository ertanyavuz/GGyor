using System;
using EntegrasyonServiceBase;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using N11Lib;

namespace UnitTestProject1.LibTest
{
    [TestClass]
    public class N11LibTest
    {
        //[TestMethod]
        public void GetCategoriesTest()
        {
            var target = new N11Service();

            target.GetCategories();
            
            Assert.IsTrue(true);
        }

        //[TestMethod]
        public void GetProductsTest()
        {
            var target = new N11Service();

            target.GetProducts();

            Assert.IsTrue(true);
        }


        //[TestMethod]
        public void GetProductsJsonTest()
        {
            var target = new N11Service();

            target.GetProductsJson();

            Assert.IsTrue(true);
        }

        //[TestMethod]
        public void UpdateProductsTest()
        {
            var target = new N11Service();

            target.UpdateProducts();

            Assert.IsTrue(true);
        }

        //[TestMethod]
        public void CreateProductTest()
        {
            var target = new N11Service();

            //target.GetShipmentTemplates();


            var prod = new ProductModel
                       {
                           stockCode = "91.000015",
                           title = "TIMBERLAND 18617 KADIN AYAKKABI",
                           label = "TIMBERLAND 18617 KADIN AYAKKABI",
                           details = "TIMBERLAND 18617 KADIN AYAKKABI<br />",
                           displayPrice = 209,
                           picture1Path = "http://www.elektrostil.com/modules/catalog/products/pr_01_3448119_max.jpg?rev=1382934834",
                           stockAmount = 23
                       };

            target.CreateProduct(prod, 1520, null);

            Assert.IsTrue(true);
        }

        //[TestMethod]
        public void CallPostTest()
        {
            var target = new N11Service();

            //target.CallPost();

            Assert.IsTrue(true);
        }
    }
}
