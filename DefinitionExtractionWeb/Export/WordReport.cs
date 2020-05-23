using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Web;
using System.Windows.Forms;
using Word = Microsoft.Office.Interop.Word;

namespace DefinitionExtractionWeb.Export
{
    public class WordReport
    {
        Word.Application app;
        Word.Document document;

        public WordReport(string templateName = "_template.docx")
        {
            app = new Word.Application();
            app.Visible = false;
            string name = $"new{DateTime.Now.Ticks}.docx";
            File.Copy(AppDomain.CurrentDomain.BaseDirectory + templateName, AppDomain.CurrentDomain.BaseDirectory + name);
            document = app.Documents.Open(AppDomain.CurrentDomain.BaseDirectory + name);
        }
        public void Apply(string currentUser, string period, List<ViewModels.UserTableViewModel> stats)
        {
            try
            {
                ReplaceWord("{currentDate}", DateTime.Now.ToString("dd.MM.yyyy"));
                ReplaceWord("{currentUser}", currentUser);
                ReplaceWord("{period}", period);
                AddTable(stats);
                document.SaveAs("Report" + DateTime.Now.Ticks.ToString());
            }
            finally
            {
                document.Close();
                app.Quit();
            }
        }

        private void ReplaceWord(string template, string replacement)
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
    }
}