using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EtnoSelo.DAL.Models
{
    public class StrucnaOsoba
    {
        public int Id { get; set; }
        public string ImePrezime { get; set; }
        public string Zvanje { get; set; }
        public string Adresa { get; set; }
        public string Telefon { get; set; }
        public string Email { get; set; }
        public string CertificiraneOblasti { get; set; }

        public virtual ICollection<OdrzavanjeAktivnosti> OdrzavanjaAktivnosti { get; set; }

    }
}
