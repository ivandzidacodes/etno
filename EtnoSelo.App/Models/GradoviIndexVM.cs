using EtnoSelo.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EtnoSelo.App.Models
{
    public class GradoviIndexVM
    {
        public List<Grad> gradoviLista { get; set; }
        public List<GradIndexItem> gradoviList{get;set;}
    }
    public class GradIndexItem
    {
        public int Id { get; set; }
        public string Naziv { get; set; }
        public string Drzava { get; set; }
    }
}