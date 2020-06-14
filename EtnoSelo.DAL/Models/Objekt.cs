using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EtnoSelo.DAL.Models
{
    public class Objekt
    {
        public int Id { get; set; }
        public string Naziv { get; set; }

        public int EtnoSeloId { get; set; }
        public virtual EtnoSelo EtnoSelo { get; set; }

        public virtual ICollection<Jedinica> Jedinice { get; set; }
    }
}
