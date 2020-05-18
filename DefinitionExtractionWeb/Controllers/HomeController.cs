using DefinitionExtractionWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DefinitionExtractionWeb.Controllers
{
    public class HomeController : Controller
    {
        DEModel db = new DEModel();
        public ActionResult Index()
        {
            IEnumerable<Descriptor> desc = db.Descriptors;
            ViewBag.Descriptors = desc;
            return View();
        }

        public ActionResult DefinitionView(int id)
        {

            return View();
        }
    }
}