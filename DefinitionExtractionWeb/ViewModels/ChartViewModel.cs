using DefinitionExtractionWeb.Models;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace DefinitionExtractionWeb.ViewModels
{
    public class ChartViewModel
    {
        public ChartViewModel(List<StatsViewModel> stats)
        {
            Title = "Статистика по пользователям за все время";
            Data = stats;
        }

        public ChartViewModel(DateTime beg, DateTime end, List<StatsViewModel> stats)
        {
            Title = $"Статистика по пользователям с {beg} по {end}";
            Data = stats;
        }

        public string Title { get; set; }
        public List<StatsViewModel> Data { get; set; }  

        public List<int> UserIDs
        {
            get
            {
                return Data.Select(data => data.UserID).ToList();
            }
        }

        public List<string> FullNames
        {
            get
            {
                return Data.Select(data => data.FullName).ToList();
            }
        }

        public List<string> Emails
        {
            get
            {
                return Data.Select(data => data.Email).ToList();
            }
        }

        public List<int> DefinitionIDs
        {
            get
            {
                return Data.Select(data => data.DefinitionID).ToList();

            }
        }
    }
}