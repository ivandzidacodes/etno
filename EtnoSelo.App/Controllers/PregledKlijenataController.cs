using EtnoSelo.App.Models;
using EtnoSelo.App.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EtnoSelo.App.Controllers
{
    public class PregledKlijenataController : Controller
    {
        // GET: PregledKlijenta
        public ActionResult Index()
        {
            var model = new PretragaKlijenataModel();

            var korisnikServis = new KorisnikServis();

            model.Klijenti = korisnikServis.DobaviSveKlijente();

            return View(model);
        }

        public ActionResult Pretraga(string imePrezime)
        {
            var model = new PretragaKlijenataModel();
            model.ImePrezime = imePrezime;

            var korisnikServis = new KorisnikServis();

            //Ako ime ili prezime nije uneseno vrati sve rezultate
            if(string.IsNullOrWhiteSpace(imePrezime))
                model.Klijenti = korisnikServis.DobaviSveKlijente();
            //Inace filtriraj po imenu i/ili prezimenu
            else
                model.Klijenti = korisnikServis.DobaviKlijentePoImenuIPrezimenu(imePrezime);

            return View("Index", model);
        }

        public ActionResult Detalji(int klijentId)
        {
            var korisnikServis = new KorisnikServis();

            var model = korisnikServis.DobaviKlijentaPoId(klijentId);

            return View(model);
        }
    }
}