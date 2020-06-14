using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace EtnoSelo.App.Models
{
    public class PonudaIzvjestajModel
    {
        public string Naziv { get; set; }
        [DisplayName("Datum Kreiranja")]
        public string DatumKreiranja { get; set; }
        [DisplayName("Vazi Od")]
        public string VaziOd { get; set; }
        [DisplayName("Vazi Do")]
        public string VaziDo { get; set; }
        public int Popust { get; set; }
        [DisplayName("Broj Rezervacija")]
        public int BrojRezervacija { get; set; }
    }
}