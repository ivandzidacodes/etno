using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EtnoSelo.App.Models
{
    public class PretragaKlijenataModel
    {
        [Display(Name = "Ime i/ili prezime")]
        public string ImePrezime { get; set; }
        public List<KlijentModel> Klijenti { get; set; }
    }
}