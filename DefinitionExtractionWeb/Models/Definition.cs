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

        [Display(Name = "Дескриптор")]
        public int Descriptor_ID { get; set; }

        [Required]
        [StringLength(300)]
        [Display(Name = "Опеределение")]
        public string Definition_content { get; set; }

        [Display(Name = "Начальная строка")]
        public int Start_line { get; set; }

        [Display(Name = "Начальный символ")]
        public int Start_char { get; set; }

        [Display(Name = "Конечная строка")]
        public int End_line { get; set; }

        [Display(Name = "Конечный символ")]
        public int End_char { get; set; }

        [Display(Name = "Добавил пользователь")]
        public int User_ID { get; set; }

        [Column(TypeName = "date")]
        [Display(Name = "Дата добавления")]
        public DateTime Insert_date { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DefinitionLink> DefinitionLinks { get; set; }

        public virtual Descriptor Descriptor { get; set; }

        public virtual User User { get; set; }
    }
}
