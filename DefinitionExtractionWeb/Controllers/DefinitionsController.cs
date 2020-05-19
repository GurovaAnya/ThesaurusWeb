using DefinitionExtractionWeb.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DefinitionExtractionWeb.Controllers
{
    public class DefinitionsController : Controller
    {
        DEModel db = new DEModel();
        // GET: Definitions
        //public ActionResult Index()
        //{
        //    return View();
        //}

        public ActionResult Index(int descID)
        {
            Descriptor desc = db.Descriptors.Find(descID); 
            return View(desc);
        }

        // GET: Definitions/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Definitions/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Definitions/Create
        //[HttpPost]
        //public ActionResult Create(FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add insert logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: Definitions/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            Descriptor desc = db.Descriptors.Find(id);

            if (desc == null)
            {
                return HttpNotFound();
            }
            return View(desc);
        }

        [HttpPost]
        public ActionResult Edit(Descriptor descriptor)
        {
            db.Entry(descriptor).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index", new {descID = descriptor.ID });
        }

        // POST: Definitions/Edit/5
        //[HttpPost]
        //public ActionResult Edit(int id, FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add update logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: Definitions/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        //// POST: Definitions/Delete/5
        //[HttpPost]
        //public ActionResult Delete(int id, FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add delete logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}
