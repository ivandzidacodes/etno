using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EtnoSelo.DAL.Models
{
    public class Jedinica
    {
        public int Id { get; set; }
        public string Naziv { get; set; }
        public short Kapacitet { get; set; }
        public string Oznaka { get; set; }


        public int TipSmjestajaId { get; set; }
        public virtual TipSmjestaja TipSmjestaja { get; set; }

        public int ObjektId { get; set; }
        public virtual Objekt Objekt { get; set; }


        public virtual ICollection<Rezervacija> Rezervacije { get; set; }
        public virtual ICollection<Kvar> Kvarovi { get; set; }
        public virtual ICollection<JedinicaPonuda> JedinicePonude { get; set; }
        public virtual ICollection<JedinicaCijenik> JediniceCijenici { get; set; }
    }
}
