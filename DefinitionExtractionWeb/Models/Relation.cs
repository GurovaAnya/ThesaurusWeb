namespace DefinitionExtractionWeb.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Relation
    {
        public int ID { get; set; }

        [Display(Name = "Главный дескриптор")]
        public int Descriptor1_ID { get; set; }

        [Display(Name = "Зависимый дескриптор")]
        public int Descriptor2_ID { get; set; }

        [Display(Name = "Вид связи")]
        public int Relation_type_ID { get; set; }

        public virtual Descriptor Descriptor { get; set; }

        public virtual Descriptor Descriptor1 { get; set; }

        public virtual Relation_types Relation_types { get; set; }
    }
}
