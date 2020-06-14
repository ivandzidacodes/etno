using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EtnoSelo.App.Models
{
    public class JedinicaIzvjestajModel
    {
        [Required(ErrorMessage = "Polje je obavezno")]
        [RegularExpression(@"^(\d{1,2}\/\d{1,2}\/2\d{3})|(\d{1,2}\.\d{1,2}\.2\d{3}\.)$")]
        public string DatumOd { get; set; }

        [Required(ErrorMessage = "Polje je obavezno")]
        [RegularExpression(@"^(\d{1,2}\/\d{1,2}\/2\d{3})|(\d{1,2}\.\d{1,2}\.2\d{3}\.)$")]
        public string DatumDo { get; set; }
        public List<JedinicaModel> Jedinice { get; set; }
    }
}