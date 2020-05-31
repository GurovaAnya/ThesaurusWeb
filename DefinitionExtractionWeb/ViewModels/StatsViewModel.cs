using DefinitionExtractionWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DefinitionExtractionWeb.ViewModels
{
    public class StatsViewModel
    {
        public Definition Definition
        { 
            set 
            {
                DefinitionID = value.ID;
            } 
        }
        public User User 
        {
            set
            {
                UserID = value.ID;
                Email = value.Email;
                FirstName = value.First_name;
                LastName = value.Last_name;
            }
        }
        public int DefinitionID { get; set; }
        public int UserID { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }
    }
}