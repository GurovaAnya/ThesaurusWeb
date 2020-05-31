using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DefinitionExtractionWeb.Models;

namespace DefinitionExtractionWeb.Controllers
{
    public class DescriptorsController : Controller
    {
        private DEDatabaseEntities db = new DEDatabaseEntities();

        // GET: Descriptors
        public ActionResult Index(string like = "")
        {
            var descriptors = db.Descriptors.Where(desc => desc.Descriptor_content.StartsWith(like)).Include(d=>d.Relator1);
            return View(descriptors);
        }

        // GET: Descriptors/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Descriptor descriptor = await db.Descriptors.FindAsync(id);
            if (descriptor == null)
            {
                return HttpNotFound();
            }
            ViewBag.Links = descriptor.DefinitionLinks.OrderBy(link => link.Start_char).ToList();
            return View(descriptor);
        }

        // GET: Descriptors/Create
        public ActionResult Create()
        {
            if (!User.Identity.IsAuthenticated)
                ViewBag.ShowModal = true;

            
                ViewBag.RelatorID = new SelectList(db.Relators, "ID", "Content");
                return View();
            
        }

        // POST: Descriptors/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,Descriptor_content,Start_line,Start_char,End_line,End_char,Relator,RelatorID")] Descriptor descriptor)
        {
            if (ModelState.IsValid)
            {
                db.Descriptors.Add(descriptor);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.RelatorID = new SelectList(db.Relators, "ID", "Content", descriptor.RelatorID);
            return View(descriptor);
        }

        // GET: Descriptors/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (!User.Identity.IsAuthenticated)
                ViewBag.ShowModal = true;

            if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Descriptor descriptor = await db.Descriptors.FindAsync(id);
                if (descriptor == null)
                {
                    return HttpNotFound();
                }
                ViewBag.RelatorID = new SelectList(db.Relators, "ID", "Content", descriptor.RelatorID);
                return View(descriptor);
           
        }

        // POST: Descriptors/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,Descriptor_content,Start_line,Start_char,End_line,End_char,Relator,RelatorID")] Descriptor descriptor)
        {
            if (ModelState.IsValid)
            {
                db.Entry(descriptor).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.RelatorID = new SelectList(db.Relators, "ID", "Content", descriptor.RelatorID);
            return View(descriptor);
        }

        // GET: Descriptors/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (!User.Identity.IsAuthenticated)
                ViewBag.ShowModal = true;

            if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Descriptor descriptor = await db.Descriptors.FindAsync(id);
                if (descriptor == null)
                {
                    return HttpNotFound();
                }
                return View(descriptor);
            
        }

        // POST: Descriptors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Descriptor descriptor = await db.Descriptors.FindAsync(id);
            db.Descriptors.Remove(descriptor);
            await db.SaveChangesAsync();
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
