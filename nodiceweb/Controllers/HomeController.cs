using nodiceweb.Models;
using nodiceweb.parser;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace nodiceweb.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Upload()
        {
            ViewBag.Message = "Select PRT file from SOM Game that contains stats";

            return View();
        }

        [HttpPost]
        public ActionResult DoUpload(HttpPostedFileBase file)
        {
            if (file.ContentLength > 0)
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    file.InputStream.CopyTo(ms);
                    StratOResultFile stratFile = new StratOResultFile(ms);
                    Dictionary<String, Season> results = stratFile.getPrimaryTotals();
                }

            }

            return RedirectToAction("Index");
        }


        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}