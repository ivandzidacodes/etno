using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EtnoSelo.DAL.Models
{
    public class JedinicaCijenik
    {
        public int Id { get; set; }
        public float Cijena { get; set; }
        public float PoreznaStopa { get; set; }

        public int JedinicaId { get; set; }
        public virtual Jedinica Jedinica { get; set; }

        public int CijenikId { get; set; }
        public virtual Cijenik Cijenik { get; set; }
    }
}
