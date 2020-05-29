using DefinitionExtractionWeb.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace DefinitionExtractionWeb.Controllers
{
    public class HomeController : Controller
    {
        DEDatabaseEntities db = new DEDatabaseEntities();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult DescriptorView()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Search(string like="")
        {
            return RedirectToAction("Index", "Descriptors", new { like = like });
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}