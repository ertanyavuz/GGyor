using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StorMan.Business;

namespace StorMan.Web.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        protected ConvertedDataSetService _service = new StorMan.Business.ConvertedDataSetService();

        public ActionResult Index()
        {
            var cdsList = _service.getConvertedDataSets();

            return View(cdsList);
        }

        public ActionResult ConvertedDataSet(int cdsID)
        {
            var cdsList = _service.getConvertedDataSets();
            var cds = cdsList.FirstOrDefault(x => x.ID == cdsID);
            if (cds != null)
            {
                cds.Transforms = _service.getTransforms(cdsID);
            }

            return View(cds);

        }

        public ActionResult Transform(int tID)
        {
            var transform = _service.getTransform(tID);

            return View(transform);

        }

        public ActionResult ApplyTransforms(int cdsID)
        {
            var cdsList = _service.getConvertedDataSets();
            var cds = cdsList.FirstOrDefault(x => x.ID == cdsID);
            if (cds != null)
            {
                cds.Transforms = _service.getTransforms(cdsID);
                cds.LoadXml();
                cds.ApplyTransforms();

                var now = DateTime.Now;
                var xmlFileName = now.Month.ToString() + now.Year.ToString();
                if (xmlFileName.Length < 6)
                    xmlFileName = "0" + xmlFileName;
                xmlFileName = now.Day.ToString() + xmlFileName;
                if (xmlFileName.Length < 8)
                    xmlFileName = "0" + xmlFileName;
                xmlFileName = "urun_" + xmlFileName + ".xml";

                var xmlFilePath = Server.MapPath("/elektrostil/arabulvar");
                xmlFilePath = Path.Combine(xmlFilePath, xmlFileName);

                cds.SaveAsXml(xmlFilePath);

                var outputFileName = Path.Combine(Path.GetDirectoryName(xmlFilePath), "urun.xml");
                System.IO.File.Copy(xmlFilePath, outputFileName, true);

                ViewBag.XmlOutputFileName = outputFileName;

                return View(cds.ResultTable);
            }

            return View();

        }

    }
}
