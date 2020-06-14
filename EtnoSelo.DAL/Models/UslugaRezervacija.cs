using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EtnoSelo.DAL.Models
{
    public class UslugaRezervacija
    {
        public int Id { get; set; }
        public float Kolicina { get; set; }
        public float Cijena { get; set; }
        public float PoreznaStopa { get; set; }

        public int RezervacijaId { get; set; }
        public virtual Rezervacija Rezervacija { get; set; }

        public int UslugaId { get; set; }
        public virtual Usluga Usluga { get; set; }

    }
}
