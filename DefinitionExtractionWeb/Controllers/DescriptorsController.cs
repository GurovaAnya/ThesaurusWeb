using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DefinitionExtractionWeb.Models;

namespace DefinitionExtractionWeb.Controllers
{
    public class DescriptorsController : Controller
    {
        private DEDatabaseEntities db = new DEDatabaseEntities();

        //// GET: Descriptors
        //public ActionResult Index()
        //{
        //    return View(db.Descriptors.ToList());
        //}

        public ActionResult Index(string like="")
        {
            return View(db.Descriptors.Where(desc=>desc.Relator.StartsWith(like)).ToList());
        }

        // GET: Descriptors/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Descriptor descriptor = db.Descriptors.Find(id);
            if (descriptor == null)
            {
                return HttpNotFound();
            }
            return View(descriptor);
        }

        // GET: Descriptors/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Descriptors/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Descriptor_content,Start_line,Start_char,End_line,End_char,RelatorID")] Descriptor descriptor)
        {
            if (ModelState.IsValid)
            {
                db.Descriptors.Add(descriptor);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(descriptor);
        }

        // GET: Descriptors/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Descriptor descriptor = db.Descriptors.Find(id);
            if (descriptor == null)
            {
                return HttpNotFound();
            }
            return View(descriptor);
        }

        // POST: Descriptors/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Descriptor_content,Start_line,Start_char,End_line,End_char,RelatorID")] Descriptor descriptor)
        {
            if (ModelState.IsValid)
            {
                db.Entry(descriptor).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(descriptor);
        }

        // GET: Descriptors/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Descriptor descriptor = db.Descriptors.Find(id);
            if (descriptor == null)
            {
                return HttpNotFound();
            }
            return View(descriptor);
        }

        // POST: Descriptors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Descriptor descriptor = db.Descriptors.Find(id);
            db.Descriptors.Remove(descriptor);
            db.SaveChanges();
            return RedirectToAction("Index");
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
