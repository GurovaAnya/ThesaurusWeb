using DefinitionExtractionWeb.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DefinitionExtractionWeb.Controllers
{
    public class DefinitionsController : Controller
    {
        DEModel db = new DEModel();
        // GET: Definitions
        public ActionResult Index()
        {
            ViewBag.Definition = db.Definitions.Find(6);
            return View();
        }
    }
}