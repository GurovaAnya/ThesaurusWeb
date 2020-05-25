namespace DefinitionExtractionWeb.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Definition
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Definition()
        {
            DefinitionLinks = new HashSet<DefinitionLink>();
        }

        public int ID { get; set; }

        public int Descriptor_ID { get; set; }

        [Required]
        [StringLength(300)]
        public string Definition_content { get; set; }

        public int Start_line { get; set; }

        public int Start_char { get; set; }

        public int End_line { get; set; }

        public int End_char { get; set; }

        public int User_ID { get; set; }

        [Column(TypeName = "date")]
        public DateTime Insert_date { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DefinitionLink> DefinitionLinks { get; set; }

        public virtual Descriptor Descriptor { get; set; }

        public virtual User User { get; set; }
    }
}
