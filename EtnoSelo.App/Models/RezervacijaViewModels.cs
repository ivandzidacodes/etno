using EtnoSelo.App.Models;
using EtnoSelo.DAL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EtnoSelo.App.Models
{
    public class RezervacijaStavkaVM
    {
        public int Id { get; set; }
        public DateTime DatumDolaska { get; set; }
        public DateTime DatumOdlaska { get; set; }
        public short BrojOsoba { get; set; }
        public bool Potvrdjena { get; set; }
        public DateTime DatumPotvrdjivanja { get; set; }
        public float Popust { get; set; }
        [Display(Name = "Rezervirao")]
        public Korisnik NosiocRezrvacijie { get; set; }
        public Jedinica Jedinica { get; set; }
        public Ponuda Ponuda { get; set; }

        [Display(Name = "Dužina boravka")]
        public int BrojNocenja
        {
            get
            {
                return Convert.ToInt32((DatumOdlaska - DatumDolaska).TotalDays);
            }
        }
    }
    public class RezervcijaIndexVM
    {
        public IEnumerable<RezervacijaStavkaVM> Rezervacije { get; set; }
    }
    public class RezervacijaIzmjeniVM
    {
        public int Id { get; set; }
        [Display(Name ="Dolazi")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyy}", ApplyFormatInEditMode = true)]
        public DateTime DatumDolaska { get; set; }

        [Display(Name = "Odlazi")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyy}", ApplyFormatInEditMode = true)]
        public DateTime DatumOdlaska { get; set; }

        [Display(Name = "Broj osoba")]
        public short BrojOsoba { get; set; }
        public bool Potvrdjena { get; set; }

        [Display(Name = "Potvrditi do")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyy}", ApplyFormatInEditMode = true)]
        public DateTime DatumPotvrdjivanja { get; set; }
        public string Komentar { get; set; }
        public short Ocjena { get; set; }
        public float Popust { get; set; }

        public Korisnik Korisnik { get; set; }

        public IEnumerable<SelectListItem> Jedinice { get; set; }
        [Display(Name = "Jedinica")]
        public int JedinicaId { get; set; }

        public virtual Ponuda Ponuda { get; set; }

        [Display(Name = "Usluge")]
        public List<UslugaRezervacijeStavkaVM> UslugeURezervaciji { get; set; }
        public List<Ucesnici> Aktivnosti { get; set; }
    }

    public class UslugaRezervacijeStavkaVM
    {
        public int Id { get; set; }

        public int TipNaplateId { get; set; }
        public string TipNaplate { get; set; }
        public float Kolicina { get; set; }
        public float Cijena { get; set; }
        public float PoreznaStopa { get; set; }
        public int UslugaId { get; set; }
        public string NazivUsluge { get; set; }


    }
}