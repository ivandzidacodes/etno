using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EtnoSelo.App.Models
{
    public class KorisnikModel
    {
        [Required(ErrorMessage = "Polje je obavezno")]
        public string Ime { get; set; }
        [Required(ErrorMessage = "Polje je obavezno")]
        public string Prezime { get; set; }
        [Required(ErrorMessage = "Polje je obavezno")]
        [Display(Name = "Korisnicko Ime")]
        public string KorisnickoIme { get; set; }
        [Required(ErrorMessage = "Polje je obavezno")]
        [DataType(DataType.Password)]
        public string Lozinka { get; set; }
        [Required(ErrorMessage = "Polje je obavezno")]
        [Display(Name = "Ponovi Lozinku")]
        [DataType(DataType.Password)]
        public string Lozinka2 { get; set; }
        [Required(ErrorMessage = "Polje je obavezno")]
        [RegularExpression(@"^[\w\d]+[\w\d-_\.]*@[\w\d]+[\w\d-_\.]*[\w\d]*\.\w{2,}$", ErrorMessage = "Unos nije u ispravnom formatu")]
        public string Email { get; set; }
        public string Adresa { get; set; }
        public string Telefon { get; set; }
        public int Drzava { get; set; } = 1;
        public List<SelectListItem> Drzave { get; set; }
        public int Grad { get; set; } = 1;
        public List<SelectListItem> Gradovi { get; set; }
    }
}