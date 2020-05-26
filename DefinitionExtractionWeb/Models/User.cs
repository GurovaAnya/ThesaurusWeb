namespace DefinitionExtractionWeb.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web.Mvc;

    public partial class User
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public User()
        {
            Definitions = new HashSet<Definition>();
        }

        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Имя")]
        public string First_name { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Фамилия")]
        public string Last_name { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Адрес электронной почты")]
        public string Email { get; set; }

        [Required]
        [StringLength(100)]
        [HiddenInput]
        public string Password_hash { get; set; }

        [Column(TypeName = "date")]
        [HiddenInput]
        public DateTime registration_date { get; set; }=DateTime.Now;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Definition> Definitions { get; set; }
    }
}
