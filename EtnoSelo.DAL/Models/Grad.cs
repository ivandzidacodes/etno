using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EtnoSelo.DAL.Models
{
    public class Grad
    {
        public int Id { get; set; }
        public string Naziv { get; set; }

        public int DrzavaId { get; set; }
        public virtual Drzava Drzava { get; set; }

        public virtual ICollection<Korisnik> Korisnici { get; set; }

        public virtual ICollection<EtnoSelo> EtnoSela { get; set; }
    }
}
