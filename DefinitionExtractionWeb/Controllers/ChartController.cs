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

        // GET: Charts
        public ActionResult Index()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
                return View(new DateTime[] { DateTime.Now, DateTime.Now });
            else
                return RedirectToAction("Login", "Authentification");
        }


        /// <summary>
        /// Получаем информацию по запросу 
        /// </summary>
        /// <param name="dates">Границы добавления определений</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Submit(DateTime [] dates)
        {
            DEQueries deq = new DEQueries();
            var info = deq.GetUsersStatistics(dates[0], dates[1]);
            var myChart = GenerateChartImage(info);
            ViewBag.Info = info;
            ViewBag.Beg = dates[0];
            ViewBag.End = dates[1];
            return PartialView("_ChartViewPartial", info);
        }

        /// <summary>
        /// Создаем изображение графика по информации
        /// </summary>
        /// <param name="info">Список с информацией по каждому пользователю</param>
        /// <returns></returns>
        public Chart GenerateChartImage(List<UserTableViewModel> info)
        {
            return new Chart(width: 600, height: 400)
                .AddSeries("Default", chartType: "Pie",
                    xValue: info, xField: "Email",
                    yValues: info, yFields: "Count")
                .Save("~/Charts/UsersDiagram.jpeg", "jpeg");
        }


        /// <summary>
        /// Составление отчета по пользователям
        /// </summary>
        /// <param name="beg">Начало</param>
        /// <param name="end">Конец</param>
        /// <returns></returns>
        public ActionResult DownloadStats(DateTime beg, DateTime end)
        {
            var deq = new DEQueries();
            //Получение информации
            var info = deq.GetUsersStatistics(beg, end);
            //Название документа
            string name = $"Отчет от {DateTime.Now.ToString("dd-MM-yyyy hh-mm-ss")}.docx";
            Export.WordReport word = new Export.WordReport(Server.MapPath("~/Charts/" + name));
            //Создаем документ
            word.Apply(User.Identity.Name, $"c {beg.ToString("dd.MM.yyyy")} по {end.ToString("dd.MM.yyyy")}", info);
            Response.ContentType = "Application/msword";
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + name);
            //Передаем документ пользователю
            Response.TransmitFile(Server.MapPath("~/Charts/" + name));
            Response.End();
            //Удаляем файл после возвращения
            System.IO.File.Delete(Server.MapPath("~/Charts/" + name));
            return new EmptyResult();
        }
    }
}