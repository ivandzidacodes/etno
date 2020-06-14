using EtnoSelo.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EtnoSelo.App.Models
{
    public class KorisniciDodajVM
    {
        public Korisnik korisnik { get; set; }

        public int GradId { get; set; }
        public List<SelectListItem> listaGradova { get; set; }

        public int RolaId { get; set; }
        public List<SelectListItem> listaRola { get; set; }
    }
}