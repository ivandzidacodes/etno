﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EtnoSelo.DAL.Models
{
    public class Drzava
    {
        public int Id { get; set; }
        public string Naziv { get; set; }

        public virtual ICollection<Grad> Gradovi { get; set; }
    }
}
