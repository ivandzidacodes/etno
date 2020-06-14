using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EtnoSelo.DAL.Models
{
    public class Usluga
    {
        public int Id { get; set; }
        public string Naziv { get; set; }
        public float Cijena { get; set; }
        public float PoreznaStopa { get; set; }

        public int TipNaplateId { get; set; }
        public virtual TipNaplate TipNaplate { get; set; }

        public virtual ICollection<UslugaRezervacija> UslugeRezervacije { get; set; }
    }
}
