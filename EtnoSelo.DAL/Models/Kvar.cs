using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EtnoSelo.DAL.Models
{
    public class Kvar
    {
        public int Id { get; set; }
        public string Opis { get; set; }
        public string Naziv { get; set; }
        public bool Rijesen { get; set; }


        public int KorisnikId { get; set; }
        public virtual Korisnik Korisnik { get; set; }

        public int JedinicaId { get; set; }
        public virtual Jedinica Jedinica { get; set; }

    }
}
