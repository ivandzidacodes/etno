using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace EtnoSelo.App.Models
{
    public class JedinicaModel
    {
        public int Id { get; set; }
        public string Naziv { get; set; }
        public int Kapacitet { get; set; }
        [DisplayName("Tip Smjestaja")]
        public string TipSmjestaja { get; set; }

        [DisplayName("Broj Izdavanja")]
        public int BrojIzdavanja { get; set; }
        public float Cijena { get; set; }
    }
}