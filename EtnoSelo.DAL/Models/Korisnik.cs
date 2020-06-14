using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EtnoSelo.DAL.Models
{
    public class Korisnik
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Ime je obavezno polje")]
        public string Ime { get; set; }
        [Required(ErrorMessage = "Prezime je obavezno polje")]
        public string Prezime { get; set; }
        [Display(Name ="Korisničko ime")]
        [Required(ErrorMessage ="Korisničko ime je obavezno polje")]
        public string KorisnickoIme { get; set; }
        //
        public string Lozinka { get; set; }
        [Required(ErrorMessage = "Email je obavezno polje")]
        public string Email { get; set; }
        [Display(Name ="Datum rođenja")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DatumRodjenja { get; set; }
        [Display(Name ="Datum zaposljenja")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DatumZaposlenja { get; set; }
        [Required(ErrorMessage = "Adresa je obavezno polje")]
        public string Adresa { get; set; }
        [Required(ErrorMessage = "Telefon je obavezno polje")]
        public string Telefon { get; set; }
        public bool Aktivan { get; set; }

        //public string Ugostiti { get; set; }

        public int RolaId { get; set; }
        public virtual Rola Rola { get; set; }

        public int GradId { get; set; }
        public virtual Grad Grad { get; set; }


        public virtual ICollection<Rezervacija> Rezervacije { get; set; }
        public virtual ICollection<Kvar> Kvarovi { get; set; }
        public virtual ICollection<Ponuda> Ponude { get; set; }
        public virtual ICollection<OdrzavanjeAktivnosti> OdrzavanjaAktivnosti { get; set; }

        public virtual ICollection<Cijenik> Cijenici { get; set; }
    }
}
