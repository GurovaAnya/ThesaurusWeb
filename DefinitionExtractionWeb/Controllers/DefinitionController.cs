using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace DefinitionExtractionWeb.Controllers
{
    public class DefinitionController : Controller
    {
        // GET: Definition
        public ActionResult Index()
        {
            return View();
        }

        // GET: Definition/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Definition/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Definition/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Definition/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Definition/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Definition/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Definition/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
