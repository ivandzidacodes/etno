using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EtnoSelo.DAL.Models
{
    public class Rezervacija
    {
        public int Id { get; set; }
        public DateTime DatumDolaska { get; set; }
        public DateTime DatumOdlaska { get; set; }
        public short BrojOsoba { get; set; }
        public bool Potvrdjena { get; set; }
        public DateTime DatumPotvrdjivanja { get; set; }
        public string Komentar { get; set; }
        public short Ocjena { get; set; }
        public float Popust { get; set; }


        public int KorisnikId { get; set; }
        public virtual Korisnik Korisnik { get; set; }

        public int JedinicaId { get; set; }
        public virtual Jedinica Jedinica { get; set; }

        public int PonudaId { get; set; }
        public virtual Ponuda Ponuda { get; set; }

        public virtual ICollection<UslugaRezervacija> UslugaRezervacija { get; set; }
        public virtual ICollection<Ucesnici> Ucesnici { get; set; }

    }
}
