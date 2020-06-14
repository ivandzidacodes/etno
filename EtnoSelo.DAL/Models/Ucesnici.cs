using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EtnoSelo.DAL.Models
{
    public class Ucesnici
    {
        public int Id { get; set; }
        public string OdgovornaOsoba { get; set; }
        public string Oprema { get; set; }
        public short BrojOsoba { get; set; }

        public int RezervacijaId { get; set; }
        public virtual Rezervacija Rezervacija { get; set; }

        public int OdrzavanjeAktivnostiId { get; set; }
        public virtual OdrzavanjeAktivnosti OdrzavanjeAktivnosti { get; set; }

    }
}
