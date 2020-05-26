namespace DefinitionExtractionWeb.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web.Mvc;

    public partial class Descriptor
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Descriptor()
        {
            Ascriptors = new HashSet<Ascriptor>();
            DefinitionLinks = new HashSet<DefinitionLink>();
            Definitions = new HashSet<Definition>();
            Relations = new HashSet<Relation>();
            Relations1 = new HashSet<Relation>();
        }

        public int ID { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Дескриптор")]
        public string Descriptor_content { get; set; }

        [Display(Name = "Начальная строка")]
        public int Start_line { get; set; }

        [Display(Name = "Начальный символ")]
        public int Start_char { get; set; }

        [Display(Name = "Конечная строка")]
        public int End_line { get; set; }

        [Display(Name = "Конечный символ")]
        public int End_char { get; set; }

        [StringLength(50)]
        [HiddenInput]
        public string Relator { get; set; }

        [Display(Name = "Релятор")]
        public int? RelatorID { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Ascriptor> Ascriptors { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DefinitionLink> DefinitionLinks { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Definition> Definitions { get; set; }

        public virtual Relator Relator1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Relation> Relations { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Relation> Relations1 { get; set; }
    }
}
