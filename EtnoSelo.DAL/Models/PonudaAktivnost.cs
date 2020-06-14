using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EtnoSelo.DAL.Models
{
    public class PonudaAktivnost
    {
        public int Id { get; set; }
        public int PonudaId { get; set; }
        public Ponuda Ponuda { get; set; }
        public int AktivnostId { get; set; }
        public Aktivnost Aktivnost { get; set; }

    }
}
