using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DefinitionExtractionWeb.ViewModels
{
    public class PeriodViewModel
    {
        [Required]
        [Display(Name = "Начало")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Beg { get; set; }

        [Required]
        [Display(Name = "Конец")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime End { get; set; }

    }
}