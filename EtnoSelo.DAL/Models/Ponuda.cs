using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EtnoSelo.DAL.Models
{
    public class Ponuda
    {
        public int Id { get; set; }
        public string Naziv { get; set; }
        public DateTime DatumOd { get; set; }
        public DateTime DatumDo { get; set; }
        public float Popust { get; set; }
        public DateTime DatumKreiranja { get; set; }

        public int KorisnikId { get; set; }
        public virtual Korisnik Korisnik { get; set; }

        public virtual ICollection<Rezervacija> Rezervacije { get; set; }

        public virtual ICollection<JedinicaPonuda> JedinicePonude { get; set; }

        public virtual ICollection<PonudaAktivnost> PonudeAktivnosti { get; set; }

    }
}
