using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EtnoSelo.App.Models
{
    public class KorisniciIzvjestajModel
    {
        [DisplayName("Ime i prezime")]
        public string ImeIPrezime { get; set; }

        [DisplayName("Korisničko ime")]
        public string KorisnickoIme { get; set; }

        [DisplayName("Adresa")]
        public string Adresa { get; set; }

        [DisplayName("Datum Zaposlenja")]
        public DateTime DatumZaposlenja { get; set; }

        [Required(ErrorMessage = "Polje je obavezno")]
        [RegularExpression(@"^(\d{1,2}\/\d{1,2}\/2\d{3})|(\d{1,2}\.\d{1,2}\.2\d{3}\.)$")]
        public string DatumOd { get; set; }

        [Required(ErrorMessage = "Polje je obavezno")]
        [RegularExpression(@"^(\d{1,2}\/\d{1,2}\/2\d{3})|(\d{1,2}\.\d{1,2}\.2\d{3}\.)$")]
        public string DatumDo { get; set; }
    }
}