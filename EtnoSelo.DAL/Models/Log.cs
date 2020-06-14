using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EtnoSelo.DAL.Models
{
    public class Log
    {
        public int Id { get; set; }
        public string Nivo { get; set; }
        public string Poruka { get; set; }
        public string Izuzetak { get; set; }
        public string Klasa { get; set; }
        public string Metoda { get; set; }

    }
}
