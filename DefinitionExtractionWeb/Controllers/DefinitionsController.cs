using DefinitionExtractionWeb.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;

namespace DefinitionExtractionWeb.Controllers
{
    public class DefinitionsController : Controller
    {
        DEDatabaseEntities db = new DEDatabaseEntities();

        public async Task<ActionResult> Index(int descID)
        {
            Descriptor desc = await db.Descriptors.FindAsync(descID); 
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

        //POST: Definitions/Create
        [HttpPost]
        public async Task<ActionResult> Create(Descriptor desc)
        {
            try
            {
                db.Descriptors.Add(desc);
                await db.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
            }
            catch
            {
                return View();
            }
        }

        //// GET: Definitions/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        [HttpGet]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Descriptor desc = await db.Descriptors.FindAsync(id);

            if (desc == null)
            {
                return HttpNotFound();
            }
            return View(desc);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(Descriptor descriptor)
        {
            db.Entry(descriptor).State = EntityState.Modified;
            await db.SaveChangesAsync();
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

        // POST: Definitions/Delete/5
        //[HttpPost]
        public async Task<ActionResult> Delete(int? id)
        {
            try
            {
                var desc = await db.Descriptors.FindAsync(id);
                //db.Entry(desc).State = EntityState.Deleted;
                db.Descriptors.Remove(desc);
                await db.SaveChangesAsync();
                var descriptors = db.Descriptors;
                return RedirectToAction("Index", "Home");
            }
            catch
            {
                return View();
            }
        }

        //[HttpGet]
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return HttpNotFound();
        //    }

        //    Descriptor desc = db.Descriptors.Find(id);

        //    if (desc == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(desc);
        //}

        //[HttpPost]
        //public ActionResult Delete(Descriptor descriptor)
        //{
        //    db.Entry(descriptor).State = EntityState.Modified;
        //    db.SaveChanges();
        //    return RedirectToAction("Index", new { descID = descriptor.ID });
        //}
    }
}
