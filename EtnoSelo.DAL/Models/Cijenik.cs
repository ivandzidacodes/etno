using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EtnoSelo.DAL.Models
{
    public class Cijenik
    {
        public int Id { get; set; }
        public DateTime DatumOd { get; set; }
        public DateTime DatumDo { get; set; }

        public int KorisnikId { get; set; }
        public virtual Korisnik Korisnik { get; set; }

        public virtual ICollection<JedinicaCijenik> JedinicaCijenik { get; set; }
    }
}
