using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EtnoSelo.App.Models
{
    public class PonudeAktivnostiModel
    {
        public PonudeAktivnostiModel()
        {
            MyGuid = Guid.NewGuid().ToString();
        }
        public string MyGuid { get; set; }
        public int Aktivnost { get; set; }
        public List<SelectListItem> Aktivnosti { get; set; }
    }
}