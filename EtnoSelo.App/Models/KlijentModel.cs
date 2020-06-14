using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EtnoSelo.App.Models
{
    public class KlijentModel
    {
        public int Id { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string Email { get; set; }
        public string Telefon { get; set; }
        public string Adresa { get; set; }
        public string Grad { get; set; }
        public string Drzava { get; set; }
        [Display(Name = "Datum Zaposlenja")]
        public string DatumZaposlenja { get; set; }
        [Display(Name = "Datum Rodjenja")]
        public string DatumRodjenja { get; set; }
        public bool Aktivan { get; set; }

    }
}