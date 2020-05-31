using DefinitionExtractionWeb.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DefinitionExtractionWeb.ViewModels
{
    public class QueryViewModel
    {
        public QueryViewModel()
        {
            BegDate = DateTime.Today;
            EndDate = DateTime.Today;
        }

        //[ForeignKey("Descriptor")]
        [Display(Name = "Связанный дескриптор")]
        public virtual int Descriptor_ID { get; set; }

        public virtual Descriptor ConnectedDescriptor { get; set; }

        [Display(Name = "Вид связи")]
        public virtual int RelationTypeID { get; set; }
        public virtual Relation_types RelationType { get; set; }

        [Display(Name = "Начало")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd'/'MM'/'yyyy}", ApplyFormatInEditMode = true)]
        public virtual DateTime BegDate { get; set; } 

        [Display(Name = "Конец")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd'/'MM'/'yyyy}", ApplyFormatInEditMode = true)]
        public virtual DateTime EndDate { get; set; } 

    }
}