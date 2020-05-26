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
        public async Task<ActionResult> Index()
        {
            IEnumerable<Descriptor> desc = await db.Descriptors.ToListAsync(); ;
            ViewBag.Descriptors = desc;
            return View();
        }

        public async Task<ActionResult> DescriptorView(int id)
        {
            var desc = await db.Descriptors.FindAsync(id);
            return View(desc);
        }

        [HttpGet]
        public ActionResult Search(string like="")
        {
            return RedirectToAction("Index", "Descriptors", new { like = like });
        }
    }
}