using DefinitionExtractionWeb.Models;
using DefinitionExtractionWeb.ViewModels;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Windows.Forms;


namespace DefinitionExtractionWeb.Export
{
    public static class WordReport
    {
        public static void SearchAndReplace(string template, string path, string currentUser, string period, List<ViewModels.UserTableViewModel> stats)
        {
            WordprocessingDocument wordprocessingDocument = WordprocessingDocument.Open(template, false);
            var newDoc =  wordprocessingDocument.Clone();
            File.Copy(template, path);
            using (WordprocessingDocument wordDocument = WordprocessingDocument.Open(path, true))
            {
                string docText = null;

                using (StreamReader sr = new StreamReader(wordDocument.MainDocumentPart.GetStream()))
                {
                    docText = sr.ReadToEnd();
                }

                docText = ReplaceByWord(docText, "currentDateTempl", DateTime.Now.ToString("dd.MM.yyyy"));
                docText = ReplaceByWord(docText, "currentUser", currentUser);
                docText = ReplaceByWord(docText, "period", period);

                using (StreamWriter sw = new StreamWriter(wordDocument.MainDocumentPart.GetStream(FileMode.OpenOrCreate)))
                {
                    sw.Write(docText);
                }
                EditTable(wordDocument, stats);
                wordDocument.Save();
            }

        }


        static string ReplaceByWord (string docText, string template, string replacement)
        {
            Regex regexText = new Regex(template);
            return regexText.Replace(docText, replacement);
        }


        static Table EditTable(WordprocessingDocument doc, List<UserTableViewModel> info)
        {
            Table tbl =doc.MainDocumentPart.Document.Body.Elements<Table>().First();
            
            for (int i = 0; i < info.Count; i++)
                tbl.AppendChild(CreateRow(i + 1, info[i]));

            return tbl;
        }

        static TableRow CreateRow (int i, UserTableViewModel userTable)
        {
            // Create 1 row to the table.
            TableRow tr1 = new TableRow();

            // Add a cell to each column in the row.
            TableCell tc1 = new TableCell(new Paragraph(new Run(new Text(i.ToString()))));
            TableCell tc2 = new TableCell(new Paragraph(new Run(new Text(userTable.FullName))));
            TableCell tc3 = new TableCell(new Paragraph(new Run(new Text(userTable.Email))));
            TableCell tc4 = new TableCell(new Paragraph(new Run(new Text(userTable.Count.ToString()))));
            tr1.Append(tc1, tc2, tc3, tc4);
            return tr1;
        }

        
    }
}