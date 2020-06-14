using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace EtnoSelo.App.Models
{
    public class UslugaModel
    {
        public string Naziv { get; set; }
        public string DatumDolaska { get; set; }
        [DisplayName("Datum Odlaska")]
        public string DatumOdlaska { get; set; }
        public float Cijena { get; set; }
        [DisplayName("Datum Dolaska")]
        public int Kolicina { get; set; }
        public float Ukupno { get; set; }
        [DisplayName("Tip Naplate")]
        public string TipNaplate { get; set; }
    }
}