using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StorMan.Business;

namespace StorMan.Web.Controllers
{
    public class HomeController : Controller
    {
        private CategoryService _service = new CategoryService();

        public ActionResult Index(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        public ActionResult Categories()
        {

            var list = _service.GetLocalCategoryList();
            var catTable = _service.GetCategoryTable();

            list.ForEach(x =>
            {
                if (!String.IsNullOrWhiteSpace(x.Code))
                {
                    x.RemoteCategory = catTable[x.Code];
                    catTable[x.Code].LocalCategoryModel = x;
                }
            });

            ViewBag.LocalCategories = list;
            ViewBag.CategoryTable = catTable;

            return View();
        }
    }
}