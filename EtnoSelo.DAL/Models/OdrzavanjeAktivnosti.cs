
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EtnoSelo.DAL.Models
{
    public class OdrzavanjeAktivnosti
    {
        public int Id { get; set; }
        public DateTime Polazak { get; set; }
        public string Vodic { get; set; }
        public TimeSpan Trajanje { get; set; }

        public int AktivnostId { get; set; }
        public virtual Aktivnost Aktivnosti { get; set; }

        public int KorisnikId { get; set; }
        public virtual Korisnik Korisnici { get; set; }

        public int StrucnaOsobaId { get; set; }
        public virtual StrucnaOsoba StrucnaOsoba { get; set; }


        public virtual ICollection<Ucesnici> Ucesnici { get; set; }
    }
}
