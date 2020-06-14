using EtnoSelo.App.Models;
using EtnoSelo.App.Services;
using EtnoSelo.App.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace EtnoSelo.App.Controllers
{
    public class KorisniciController : Controller
    {
        private readonly KorisnikServis _korisnikServis;
        public KorisniciController()
        {
            _korisnikServis = new KorisnikServis();
        }

        // GET: Korisnici
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            var model = new KorisniciIndexVM();
            model.listaKorisnika = _korisnikServis.DobaviSveKorisnike();

            return View(model);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult Uredi(int Id)
        {
            var model = new KorisniciUrediVM();
            model.korisnik = _korisnikServis.DobaviKorinikaPoId(Id);

            return View(model);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult Uredi(KorisniciUrediVM model)
        {
            if (!ModelState.IsValid)
                return View(model);

            _korisnikServis.IzmjeniKorisnika(model);

            var vmodel = new KorisniciIndexVM();
            vmodel.listaKorisnika = _korisnikServis.DobaviSveKorisnike();

            return PartialView("KorisniciPartial", vmodel);
        }

        [Authorize]
        [HttpGet]
        public ActionResult UrediMojRacun()
        {
            var Id = SessionFacade.KorisnikId;
            if (Id > 0)
            {
                var model = new KorisniciUrediVM();
                model.korisnik = _korisnikServis.DobaviKorinikaPoId(Id);
                ViewBag.Notifikacija = TempData["ProsljedjenaPoruka"]?.ToString();

                return View("Uredi", model);
            }

            return RedirectToAction("Index","Login");
        }

        [Authorize]
        [HttpPost]
        public ActionResult UrediMojRacun(KorisniciUrediVM model)
        {
            if (!ModelState.IsValid)
                return View("Uredi", model);


            var izmjenjenZapis = _korisnikServis.IzmjeniKorisnika(model);

            //Dino: Primitivna notifikacija
            if (izmjenjenZapis < 1)
                TempData["ProsljedjenaPoruka"] = "Izmjena podataka nije uspjela!";
            else
                TempData["ProsljedjenaPoruka"] = "Podaci uspješno izmjenjeni";

            return RedirectToAction("UrediMojRacun", "Korisnici");
        }




        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult PromjeniStatus(int Id)
        {
            var korisnikServis = new KorisnikServis();

            korisnikServis.PromjeniStatus(Id);

            return RedirectToAction("Index");

        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult Dodaj()
        {
            var korisnikServis = new KorisnikServis();

            var model = new KorisniciDodajVM();

            model.listaGradova = korisnikServis.DobaviSveGradove().Select(x=> new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Naziv
            }).ToList();

            model.listaRola = korisnikServis.DobaviRole().Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Naziv
            }).ToList();

            return View(model);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult Dodaj(KorisniciDodajVM model)
        {
            var korisnikServis = new KorisnikServis();
            if (!ModelState.IsValid)
            {
                model.listaGradova = korisnikServis.DobaviSveGradove().Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.Naziv
                }).ToList();

                model.listaRola = korisnikServis.DobaviRole().Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.Naziv
                }).ToList();

                return View(model);
            }
            korisnikServis.DodajKorisnika(model);

            return RedirectToAction("Index");
        }

        


    }
}