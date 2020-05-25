namespace DefinitionExtractionWeb.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

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

        [Required]
        [StringLength(100)]
        public string Descriptor_content { get; set; }

        public int Start_line { get; set; }

        public int Start_char { get; set; }

        public int End_line { get; set; }

        public int End_char { get; set; }

        [StringLength(50)]
        public string Relator { get; set; }

        public int ID { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Ascriptor> Ascriptors { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DefinitionLink> DefinitionLinks { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Definition> Definitions { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Relation> Relations { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Relation> Relations1 { get; set; }
    }
}
