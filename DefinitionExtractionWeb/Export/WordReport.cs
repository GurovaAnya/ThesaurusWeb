using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Windows.Forms;
using Word = Microsoft.Office.Interop.Word;


namespace DefinitionExtractionWeb.Export
{
    public class WordReport
    {
        Word.Application app;
        Word.Document document;
        string path;

        public WordReport(string path, string templateName = "_template.docx")
        {
            app = new Word.Application();
            app.Visible = false;
            //name = $"Report{DateTime.Now.Ticks}.docx";
            //path = AppDomain.CurrentDomain.BaseDirectory + name;
            this.path = path;
            File.Copy(AppDomain.CurrentDomain.BaseDirectory + templateName, path);
        }
        public string Apply(string currentUser, string period, List<ViewModels.UserTableViewModel> stats)
        {
            try
            {
                document = app.Documents.Open(path);
                ReplaceByWord("{currentDate}", DateTime.Now.ToString("dd.MM.yyyy"));
                ReplaceByWord("{currentUser}", currentUser);
                ReplaceByWord("{period}", period);
                AddTable(stats);
                //string name = "Report" + DateTime.Now.Ticks.ToString();
                //path = AppDomain.CurrentDomain.BaseDirectory + name;
                //File.Create(path);
                //document.Save();
                //document.SaveAs()
                //document.SaveAs(FileName: name);
                return document.Name;
            }
            finally
            {
                document.Close(SaveChanges:true);
                app.Quit();
            }
        }



        private void ReplaceByWord(string template, string replacement)
        {
            var range = document.Content;
            range.Find.ClearFormatting();
            range.Find.Execute(FindText: template, ReplaceWith: replacement);
        }

        private void AddTable(List<ViewModels.UserTableViewModel> stats)
        {
            var range = document.Content;
            var tables = document.Tables;
            Word.Table table = document.Tables[1];
            for (int i = 0;i<stats.Count; i++)
            {
                Word.Row row = table.Rows.Add();
                row.Cells[1].Range.Text = (i + 1).ToString();
                row.Cells[2].Range.Text = stats[i].FullName;
                row.Cells[3].Range.Text = stats[i].Email;
                row.Cells[4].Range.Text = stats[i].Count.ToString();

            }
        }

        //public async Task<ActionResult> Download()
        //{
        //    var memory = new MemoryStream();
        //    using (var stream = new FileStream(path, FileMode.Open))
        //    {
        //        await stream.CopyToAsync(memory);
        //    }
        //    memory.Position = 0;
        //    return File(memory, "application/vnd.ms-word", Path.GetFileName(path));

        //    Response.ContentType = "application/pdf";
        //    Response.AppendHeader("Content-Disposition", "attachment; filename=MyFile.pdf");
        //    Response.TransmitFile(Server.MapPath("~/Files/MyFile.pdf"));
        //    Response.End();
        //}
    }
}