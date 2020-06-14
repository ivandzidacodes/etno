using EtnoSelo.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EtnoSelo.App.Models
{
    public class GradoviDodajVM
    {

        public Grad grad { get; set; }

        public int DrzavaId { get; set; }
        public List<SelectListItem> listaDrzava { get; set; }
    }
}