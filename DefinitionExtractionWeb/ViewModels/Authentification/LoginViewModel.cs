using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace DefinitionExtractionWeb.ViewModels.Authentification
{
    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Логин (email)")]
        public string Email { get; set; }


        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

    }
}