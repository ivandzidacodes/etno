using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EtnoSelo.DAL.Models
{
    public class TipNaplate
    {
        public int Id { get; set; }
        public string Naziv { get; set; }
        public string Oznaka { get; set; }

        public virtual ICollection<Usluga> Usluge { get; set; }
        public virtual ICollection<Aktivnost> Aktivnosti { get; set; }
    }
}
