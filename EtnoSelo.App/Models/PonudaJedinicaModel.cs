using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EtnoSelo.App.Models
{
    public class PonudaJedinicaModel
    {
        public PonudaJedinicaModel()
        {
            MyGuid = Guid.NewGuid().ToString();
        }
        public string MyGuid { get; set; }
        [Display(Name = "Tip Stanbene Jedinice")]
        public int TipStanbeneJedinice { get; set; }
        public List<SelectListItem> TipoviStanbenihJedinica { get; set; }
        [Display(Name = "Naziv Stanbene Jedinice")]
        public int StanbenaJedinica { get; set; }
        public List<JedinicaModel> StanbeneJedinice { get; set; }
        [Display(Name = "Cijena Bez Popusta")]
        public float CijenaBezPopusta { get; set; }
        [Display(Name = "Cijena Sa Popustom")]
        public float CijenaSaPopustom { get; set; }
    }
}