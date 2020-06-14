using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace EtnoSelo.App.Models
{
    public class RezervacijaModel
    {
        [DisplayName("Datum Dolaska")]
        public string DatumDolaska { get; set; }
        [DisplayName("Datum Odlaska")]
        public string DatumOdlaska { get; set; }
        [DisplayName("Broj Osoba")]
        public short BrojOsoba { get; set; }
        public bool Potvrdjena { get; set; }
        public short Ocjena { get; set; }
        public float Popust { get; set; }
        public string Korisnik { get; set; }
    }
}