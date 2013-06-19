using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StorMan.Business;

using StorMan.Model;



namespace StorMan.Web.Controllers
{
    public class HomeController : Controller
    {
        private CategoryService _categoryService = new CategoryService();
        private ProductService _productService = new ProductService();

        public ActionResult Index(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        public ActionResult Categories()
        {

            var list = _categoryService.GetLocalCategoryList();
            var catTable = _categoryService.GetCategoryTable();
            list.ForEach(x =>
            {
                if (!String.IsNullOrWhiteSpace(x.Code))
                {
                    x.RemoteCategory = catTable[x.Code];
                    catTable[x.Code].LocalCategoryModel = x;
                }
            });
            foreach (var code in catTable.Keys)
            {
                var category = catTable[code];
                if (!String.IsNullOrWhiteSpace(code) && !String.IsNullOrWhiteSpace(code.Substring(0, code.Length - 1)))
                {
                    var parentCode = code.Substring(0, code.Length - 1);
                    if (catTable.ContainsKey(parentCode))
                        category.Parent = catTable[parentCode];
                }
                if (category.Children == null)
                    category.Children = new List<CategoryModel>();
                category.Children.AddRange(catTable.Values.Where(x => x.Code.StartsWith(code)).ToList());
            }

            ViewBag.LocalCategories = list;
            ViewBag.CategoryTable = catTable;

            return View();
        }

        public ActionResult ProductsFromXml()
        {
            return View();
        }

        public ActionResult ProductsGG()
        {
            
            return View();
        }

        public JsonResult GetProductsFromServer()
        {
            

            return new JsonResult();
        }

    }
}