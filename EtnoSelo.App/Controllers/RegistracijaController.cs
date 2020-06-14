using EtnoSelo.App.Models;
using EtnoSelo.App.Services;
using EtnoSelo.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EtnoSelo.App.Controllers
{
    [AllowAnonymous]
    public class RegistracijaController : Controller
    {
        // GET: Registracija
        public ActionResult Index()
        {
            var korisnikServis = new KorisnikServis();

            var model = korisnikServis.InicijalizujModel();

            return View("Registracija", model);
        }

        public ActionResult Registracija(KorisnikModel korisnik)
        {
            var korisnikServis = new KorisnikServis();

            //Registruj korisnika
            var noviKorisnikId = korisnikServis.RegistrujKorisnika(korisnik);

            //Ako je korisnicko ime zauzeto prikazi error
            if(noviKorisnikId == -1)
            {
                //Populisi model sa vrijednostima za dropdown liste
                korisnik.Drzave = korisnikServis.DobaviDrzave();
                korisnik.Gradovi = korisnikServis.DobaviSelectListuGradovaPoDrzavi(korisnik.Drzava);

                ModelState.AddModelError("", "Korisnicko ime je zauzeto!");
                return View(korisnik);
            }

            return View("PotvrdaRegistracije");
        }

        public ActionResult PotvrdaRegistracije()
        {
            return View();
        }

        public JsonResult DobaviGradove(int drzavaId)
        {
            var korisnikServis = new KorisnikServis();

            var gradovi = korisnikServis.DobaviGradovePoDrzavi(drzavaId);

            return Json(gradovi, JsonRequestBehavior.AllowGet);
        }
    }
}