using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DefinitionExtractionWeb.ViewModels
{
    public class UserTableViewModel
    {
        public int UserID { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public int Count { get; set; }
    }
}