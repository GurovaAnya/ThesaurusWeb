using DefinitionExtractionWeb.Models;
using DefinitionExtractionWeb.Queries;
using DefinitionExtractionWeb.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace DefinitionExtractionWeb.Controllers
{
    public class QueryController : Controller
    {
        DEDatabaseEntities db = new DEDatabaseEntities();
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Index()
        { 
            QueryViewModel que = new QueryViewModel(); // ViewModel
            ViewBag.Descriptor_ID = new SelectList(db.Descriptors, "ID", "Descriptor_content");
            ViewBag.Type_ID = new SelectList(db.Relation_types, "ID", "Type_name");
            return View(que); 
        }

        [HttpPost]
        public ActionResult DefinitionSearch(QueryViewModel que)
        {
            DEQueries dq = new DEQueries(); 
            object result = dq.GetDefinitions(que.BegDate, que.EndDate, que.RelationTypeID, que.Descriptor_ID);
            ViewBag.Descriptor_ID = new SelectList(db.Descriptors, "ID", "Descriptor_content", que.Descriptor_ID);
            ViewBag.Type_ID = new SelectList(db.Relation_types, "ID", "Type_name", que.RelationTypeID);
            return PartialView("_DisplayDescriptorsPartial", result);
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
