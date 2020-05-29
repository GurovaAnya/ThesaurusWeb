using DefinitionExtractionWeb.Models;
using DefinitionExtractionWeb.Queries;
using DefinitionExtractionWeb.ViewModels;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Caching;
using System.Web.Helpers;
using System.Web.Mvc;

namespace DefinitionExtractionWeb.Controllers
{
    public class ChartController : Controller
    {
        //DEDatabaseEntities db = new DEDatabaseEntities();

        // GET: Charts
        public ActionResult Index()
        {
            if (!HttpContext.User.Identity.IsAuthenticated)
                ViewBag.ShowModal = true;
                return View(new DateTime[] { DateTime.Now, DateTime.Now });

        }


        /// <summary>
        /// Получаем информацию по запросу 
        /// </summary>
        /// <param name="dates">Границы добавления определений</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> Submit(DateTime [] dates)
        {
            if (dates[0] == new DateTime() || dates[1] == new DateTime())
                throw new Exception("Мы собираемся работать, м????");
            try
            {
                DEQueries deq = new DEQueries();
                var info = await deq.GetUsersStatistics(dates[0], dates[1]);
                //ViewBag.Info = info;
                ViewBag.Beg = dates[0];
                ViewBag.End = dates[1];
                //TempData["Beg"] = dates[0];
                //TempData["End"] = dates[1];
                ViewBag.Chart = GenerateChartImage(info);
                return PartialView("_ChartViewPartial", info);
            }catch(Exception e)
            {
                return new HttpNotFoundResult(e.Message);
            }
                
        }

        /// <summary>
        /// Создаем изображение графика по информации
        /// </summary>
        /// <param name="info">Список с информацией по каждому пользователю</param>
        /// <returns></returns>
        public byte[] GenerateChartImage(List<UserTableViewModel> info)
        {
            return new Chart(width: 600, height: 400)
                .AddSeries("Default", chartType: "Pie",
                    xValue: info, xField: "Email",
                    yValues: info, yFields: "Count").GetBytes();
        }


        /// <summary>
        /// Составление отчета по пользователям
        /// </summary>
        /// <param name="beg">Начало</param>
        /// <param name="end">Конец</param>
        /// <returns></returns>
        public async Task<ActionResult> DownloadStats (DateTime beg, DateTime end)
        {
            //DateTime beg = (DateTime)TempData["Beg"];
            //DateTime end = (DateTime)TempData["End"];
            //beg = DateTime.Now.AddYears(-10);
            //end = DateTime.Now;
            var deq = new DEQueries();
            //Получение информации
            var info = await deq.GetUsersStatistics(beg, end);
            //Название документа
            string name = $"Отчет от {DateTime.Now.ToString("dd-MM-yyyy hh-mm-ss")}.docx";
            //Создаем документ
            Export.WordReport.SearchAndReplace(Server.MapPath("~/Charts/" + "template.docx"), Server.MapPath("~/Charts/" + name), User.Identity.Name,
                $"c {beg.ToString("dd.MM.yyyy")} по {end.ToString("dd.MM.yyyy")}", info);
            Response.ContentType = "Application/msword";
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + name);
            //Передаем документ пользователю
            Response.TransmitFile(Server.MapPath("~/Charts/" + name));
            Response.End();
            //Удаляем файл после возвращения
            System.IO.File.Delete(Server.MapPath("~/Charts/" + name));
            return new EmptyResult();
        }

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}