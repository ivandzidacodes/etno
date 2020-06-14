using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EtnoSelo.App.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Polje je obavezno")]
        [Display(Name = "Korisnicko Ime")]
        public string KorisnickoIme { get; set; }
        [Required(ErrorMessage = "Polje je obavezno")]
        [DataType(DataType.Password)]
        public string Lozinka { get; set; }
    }
}