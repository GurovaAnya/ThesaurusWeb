using DefinitionExtractionWeb.Models;
using DefinitionExtractionWeb.Queries;
using DefinitionExtractionWeb.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace DefinitionExtractionWeb.Controllers
{
    public class ChartsController : Controller
    {
        DEDatabaseEntities db = new DEDatabaseEntities();


        // GET: Charts
        public ActionResult Index()
        {
            DEQueries deq = new DEQueries();
            var model = deq.GetChartForUsers();
            ViewBag.Model = model;
            //return PartialView("_ChartViewPartial", model);

            return View(new DateTime[]{ DateTime.Now, DateTime.Now }) ;
        }

        [HttpPost]
        public ActionResult Submit(DateTime [] dates)
        {
            DEQueries deq = new DEQueries();
            var model = deq.GetChartForUsers(dates[0], dates[1]);
            ViewBag.Model = model;
            //List<string> emails = model.Emails.Distinct().ToList();
            //List<int> quantities = 
            //var experiment = db.Definitions.Where(def => def.Insert_date >= dates[0] && def.Insert_date <= dates[1]).GroupBy(def=>def.User_ID)
            //    .Select(group => new { Email = group.FirstOrDefault().User, Count = group.Count() }).ToList();
            var experiment = db.Users.Select(user => new UserTableViewModel{ FullName = user.First_name + " " + user.Last_name, 
                Email = user.Email, Count = user.Definitions.Count }).OrderByDescending(user=>user.Count).ToList();
            //GenerateChartImage(model);
            var myChart = new Chart(width: 600, height: 400)
                .AddSeries("Default", chartType: "Pie",
                    xValue: experiment, xField: "Email",
                    yValues: experiment, yFields: "Count")
                .Save("UsersDiagram.jpeg", "jpeg");
            ViewBag.Info = experiment;
            Export.WordReport word = new Export.WordReport();
            word.Apply("Я", "kcmakd", experiment);
            return PartialView("_ChartViewPartial", model);
        }

        public void GenerateChartImage(ChartViewModel info)
        {
            var myChart = new Chart(width: 600, height: 400)
                .AddTitle(info.Title)
                .AddSeries("Default", chartType: "Pie",
                    xValue: info.Data, xField: "FullName")
                    //yValues: info.Data, yFields: "DefinitionID")
                .Save("UsersDiagram.jpeg", "jpeg");
        }
    }
}