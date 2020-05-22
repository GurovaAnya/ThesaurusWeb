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
        [ForeignKey("Descriptor")]
        public virtual int Descriptor_ID { get; set; }
        public virtual Descriptor ConnectedDescriptor { get; set; }
        public virtual int RelationTypeID { get; set; } 
        public virtual Relation_types RelationType { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd'/'MM'/'yyyy}", ApplyFormatInEditMode = true)]
        public virtual DateTime BegDate { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd'/'MM'/'yyyy}", ApplyFormatInEditMode = true)]
        public virtual DateTime EndDate { get; set; }

    }
}