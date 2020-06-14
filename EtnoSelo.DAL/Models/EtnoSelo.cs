using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EtnoSelo.DAL.Models
{
    public class EtnoSelo
    {
        public int Id { get; set; }
        public string Naziv { get; set; }
        public string Adresa { get; set; }
        public string Kontakt { get; set; }
        public string RadnoVrijeme { get; set; }
        public string ZiroRacun { get; set; }

        public int GradId { get; set; }
        public virtual Grad Grad { get; set; }

        public virtual ICollection<Objekt> Objekti { get; set; }
    }
}
