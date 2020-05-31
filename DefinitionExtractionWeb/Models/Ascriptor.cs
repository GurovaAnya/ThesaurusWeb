namespace DefinitionExtractionWeb.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Ascriptor
    {
        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Аскриптор")]
        public string Ascriptor_content { get; set; }

        [Display(Name = "Связанный дескриптор")]
        public int Descriptor_ID { get; set; }

        public virtual Descriptor Descriptor { get; set; }
    }
}
