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
    public class DefinitionLinksController : Controller
    {
        private DEDatabaseEntities db = new DEDatabaseEntities();

        //// GET: DefinitionLinks
        //public async Task<ActionResult> Index()
        //{
        //    var definitionLinks = db.DefinitionLinks.Include(d => d.Definition).Include(d => d.Descriptor);
        //    return View(await definitionLinks.ToListAsync());
        //}

        public async Task<ActionResult> Index(int id)
        {
            if (User.Identity.IsAuthenticated)
            {
                var definitionLinks = db.DefinitionLinks.Where(link => link.Definition_ID == id).Include(d => d.Definition).Include(d => d.Descriptor);
                return View(await definitionLinks.ToListAsync());
            }
            else
                return RedirectToAction("Login", "Authentification");
        }

        // GET: DefinitionLinks/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DefinitionLink definitionLink = await db.DefinitionLinks.FindAsync(id);
            if (definitionLink == null)
            {
                return HttpNotFound();
            }
            return View(definitionLink);
        }

        // GET: DefinitionLinks/Create
        public async Task<ActionResult> Create(int id)
        {
            if (User.Identity.IsAuthenticated)
            {
                ViewBag.Definition_ID = new SelectList(db.Definitions, "ID", "Definition_content");
                ViewBag.Descriptor_ID = new SelectList(db.Descriptors, "ID", "Descriptor_content");
                Definition def = await db.Definitions.FindAsync(id);
                DefinitionLink dl = new DefinitionLink { Definition = def, Definition_ID = id };
                return View(dl);
            }
            else
                return RedirectToAction("Login", "Authentification");
        }

        // POST: DefinitionLinks/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,Descriptor_ID,Start_char,Length,Definition_ID")] DefinitionLink definitionLink)
        {
            if (ModelState.IsValid)
            {
                db.DefinitionLinks.Add(definitionLink);
                await db.SaveChangesAsync();
                return RedirectToAction("Index", new { id = definitionLink.Definition_ID});
            }

            ViewBag.Definition_ID = new SelectList(db.Definitions, "ID", "Definition_content", definitionLink.Definition_ID);
            ViewBag.Descriptor_ID = new SelectList(db.Descriptors, "ID", "Descriptor_content", definitionLink.Descriptor_ID);
            return View(definitionLink);
        }

        // GET: DefinitionLinks/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                DefinitionLink definitionLink = await db.DefinitionLinks.FindAsync(id);
                if (definitionLink == null)
                {
                    return HttpNotFound();
                }
                ViewBag.Definition_ID = new SelectList(db.Definitions, "ID", "Definition_content", definitionLink.Definition_ID);
                ViewBag.Descriptor_ID = new SelectList(db.Descriptors, "ID", "Descriptor_content", definitionLink.Descriptor_ID);
                return View(definitionLink);
            }
            else
                return RedirectToAction("Login", "Authentification");
        }

        // POST: DefinitionLinks/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,Descriptor_ID,Start_char,Length,Definition_ID")] DefinitionLink definitionLink)
        {
            if (ModelState.IsValid)
            {
                db.Entry(definitionLink).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index", new { id = definitionLink.Definition_ID});
            }
            ViewBag.Definition_ID = new SelectList(db.Definitions, "ID", "Definition_content", definitionLink.Definition_ID);
            ViewBag.Descriptor_ID = new SelectList(db.Descriptors, "ID", "Descriptor_content", definitionLink.Descriptor_ID);
            return View(definitionLink);
        }

        // GET: DefinitionLinks/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (!User.Identity.IsAuthenticated)
                ViewBag.ShowModal = true;

                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                DefinitionLink definitionLink = await db.DefinitionLinks.FindAsync(id);
                if (definitionLink == null)
                {
                    return HttpNotFound();
                }
                return View(definitionLink);

        }

        // POST: DefinitionLinks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            DefinitionLink definitionLink = await db.DefinitionLinks.FindAsync(id);
            int defID = definitionLink.Definition_ID;
            db.DefinitionLinks.Remove(definitionLink);
            await db.SaveChangesAsync();
            return RedirectToAction("Index", new { id = defID});
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
