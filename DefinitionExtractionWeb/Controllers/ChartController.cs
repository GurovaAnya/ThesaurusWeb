using DefinitionExtractionWeb.Charts;
using DefinitionExtractionWeb.Models;
using DefinitionExtractionWeb.Queries;
using DefinitionExtractionWeb.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Web;
using System.Web.Caching;
using System.Web.Helpers;
using System.Web.Mvc;

namespace DefinitionExtractionWeb.Controllers
{
    public class ChartController : Controller
    {
        DEDatabaseEntities db = new DEDatabaseEntities();
        List<UserTableViewModel> table;
        // GET: Charts
        public ActionResult Index()
        {
            return View(new DateTime[] { DateTime.Now, DateTime.Now });
        }


        [HttpPost]
        public ActionResult Submit(DateTime [] dates)
        {
            DEQueries deq = new DEQueries();
            var model = deq.GetChartForUsers(dates[0], dates[1]);
            ViewBag.Model = model;
            var info = db.Users.Select(user => new UserTableViewModel{ FullName = user.First_name + " " + user.Last_name, 
                Email = user.Email, Count = user.Definitions.Count
                //(def=>def.Insert_date>=dates[0]&&def.Insert_date<=dates[1]) 
            })
                .OrderByDescending(user=>user.Count).ToList();
            table = info;
            var myChart = GenerateChartImage(info);
            ViewBag.Info = info;
            ViewBag.Name = CreateStats(info);
            return PartialView("_ChartViewPartial", info);
        }

        public Chart GenerateChartImage(List<UserTableViewModel> info)
        {
            return new Chart(width: 600, height: 400)
                .AddSeries("Default", chartType: "Pie",
                    xValue: info, xField: "Email",
                    yValues: info, yFields: "Count")
                .Save("~/Charts/UsersDiagram.jpeg", "jpeg");
        }

       
        public string CreateStats(List<UserTableViewModel> info)
        {
            string name = $"Отчет от {DateTime.Now.ToString("dd-MM-yyyy hh-mm-ss")}.docx";
            Export.WordReport word = new Export.WordReport(Server.MapPath("~/Charts/" + name));
            word.Apply("Я", "kcmakd", info);
            return name;
        }

        //[HttpPost]
        public ActionResult DownloadStats(string name)
        {
            Response.ContentType = "Application/msword";
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + name);
            Response.TransmitFile(Server.MapPath("~/Charts/" + name));
            Response.End();
            return new EmptyResult();

        }


    }
}