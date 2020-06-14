using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EtnoSelo.App.Models
{
    public class PonudaModel
    {
        public PonudaModel()
        {
            VaziOd = DateTime.Now.Date.ToShortDateString();//.ToString("dd/MM/yyyy").Replace('.', '/');
            VaziDo = DateTime.Now.Date.ToShortDateString();//.ToString("dd/MM/yyyy").Replace('.', '/');
        }
        public short? Dodaj { get; set; }
        public string Guid { get; set; }
        [Required(ErrorMessage = "Polje je obavezno")]
        public string Naziv { get; set; }
        public int Id { get; set; }
        [Display(Name = "Vazi Od")]
        [Required(ErrorMessage = "Polje je obavezno")]
        //[RegularExpression(@"^[012]{1,2}\/[012]{1,2}\/2\d{3}$")]
        [RegularExpression(@"^(\d{1,2}\/\d{1,2}\/2\d{3})|(\d{1,2}\.\d{1,2}\.2\d{3}\.)$")]
        public string VaziOd { get; set; }
        [Display(Name = "Do")]
        [Required(ErrorMessage = "Polje je obavezno")]
        //[RegularExpression(@"^[012]{1,2}\/[012]{1,2}\/2\d{3}$")]
        //[RegularExpression(@"^\d{1,2}\/\d{1,2}\/2\d{3}$")]
        [RegularExpression(@"^(\d{1,2}\/\d{1,2}\/2\d{3})|(\d{1,2}\.\d{1,2}\.2\d{3}\.)$")]
        public string VaziDo { get; set; }
        public int Popust { get; set; }
        [Display(Name = "Datum Kreiranja")]
        public string DatumKreiranja { get; set; }
        public List<PonudaJedinicaModel> PonudeJedinice { get; set; }
        public List<PonudeAktivnostiModel> Aktivnosti { get; set; }
        public IEnumerable<HttpPostedFileBase> Slike { get; set; }
        public List<string> NaziviSlika { get; set; }
    }
}