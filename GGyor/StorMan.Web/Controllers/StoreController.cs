using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StorMan.Business;

namespace StorMan.Web.Controllers
{
    public class StoreController : Controller
    {
        //
        // GET: /Store/

        protected ConvertedDataSetService _service = new StorMan.Business.ConvertedDataSetService();

        public ActionResult Index()
        {
            var cdsList = _service.getConvertedDataSets();
            var cds = cdsList.FirstOrDefault();
            if (cds != null)
            {
                cds.Transforms = _service.getTransforms(cds.ID);
            }

            //ViewBag.ConvertedDataSet = cds;

            return View(cds);
        }

    }
}
