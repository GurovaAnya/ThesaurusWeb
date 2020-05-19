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

        public ActionResult DescriptorView(int id)
        {
            ViewBag.Definitions = db.Descriptors.Find(id);
            return View();
        }

        [HttpGet]
        public ActionResult Search(string like="")
        {
            ViewBag.Descriptors = db.Descriptors.Where(d => d.Descriptor_content.StartsWith(like));
            return View("~/Views/Definitions/DescriptorsList.cshtml");
        }

        //[HttpPost]
        //public ActionResult Search(string like)
        //{
        //    ViewBag.Descriptors = db.Descriptors.Where(d => d.Descriptor_content.StartsWith(like));
        //    return View("Descriptors/Index");
        //}
    }
}