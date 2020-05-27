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
    public class Definitions1Controller : Controller
    {
        private DEDatabaseEntities db = new DEDatabaseEntities();

        // GET: Definitions1
        public ActionResult Index()
        {
            var definitions = db.Definitions.Include(d => d.Descriptor).Include(d => d.User);
            return View(definitions.ToList());
        }

        // GET: Definitions1/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Definition definition = db.Definitions.Find(id);
            if (definition == null)
            {
                return HttpNotFound();
            }
            return View(definition);
        }

        // GET: Definitions1/Create
        public ActionResult Create()
        {
            if (User.Identity.IsAuthenticated)
            {
                ViewBag.Descriptor_ID = new SelectList(db.Descriptors, "ID", "Descriptor_content");
                ViewBag.User_ID = new SelectList(db.Users, "ID", "First_name");
                return View();
            }
            else
                return RedirectToAction("Login", "Authentification");
        }

        // POST: Definitions1/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Descriptor_ID,Definition_content,Start_line,Start_char,End_line,End_char,User_ID,Insert_date")] Definition definition)
        {
            if (ModelState.IsValid)
            {
                db.Definitions.Add(definition);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Descriptor_ID = new SelectList(db.Descriptors, "ID", "Descriptor_content", definition.Descriptor_ID);
            ViewBag.User_ID = new SelectList(db.Users, "ID", "First_name", definition.User_ID);
            return View(definition);
        }

        // GET: Definitions1/Edit/5
        public ActionResult Edit(int? id)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Definition definition = db.Definitions.Find(id);
                if (definition == null)
                {
                    return HttpNotFound();
                }
                ViewBag.Descriptor_ID = new SelectList(db.Descriptors, "ID", "Descriptor_content", definition.Descriptor_ID);
                ViewBag.User_ID = new SelectList(db.Users, "ID", "First_name", definition.User_ID);
                return View(definition);
            }
            else
                return RedirectToAction("Login", "Authentification");
        }

        // POST: Definitions1/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Descriptor_ID,Definition_content,Start_line,Start_char,End_line,End_char,User_ID,Insert_date")] Definition definition)
        {
            if (ModelState.IsValid)
            {
                db.Entry(definition).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Descriptor_ID = new SelectList(db.Descriptors, "ID", "Descriptor_content", definition.Descriptor_ID);
            ViewBag.User_ID = new SelectList(db.Users, "ID", "First_name", definition.User_ID);
            return View(definition);
        }

        // GET: Definitions1/Delete/5
        public ActionResult Delete(int? id)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Definition definition = db.Definitions.Find(id);
                if (definition == null)
                {
                    return HttpNotFound();
                }
                return View(definition);
            }
            else
                return RedirectToAction("Login", "Authentification");
        }

        // POST: Definitions1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Definition definition = db.Definitions.Find(id);
            db.Definitions.Remove(definition);
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
