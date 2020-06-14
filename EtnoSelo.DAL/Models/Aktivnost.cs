using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EtnoSelo.DAL.Models
{
    public class Aktivnost
    {
        public int Id { get; set; }
        public string Naziv { get; set; }
        public bool Vodic { get; set; }
        public string Oprema { get; set; }

        public int TipNaplateId { get; set; }
        public virtual TipNaplate TipNaplate { get; set; }

        public virtual ICollection<OdrzavanjeAktivnosti> OdrzavanjaAktivnosti { get; set; }
        public virtual ICollection<PonudaAktivnost> PonudeAktivnosti { get; set; }
    }
}
