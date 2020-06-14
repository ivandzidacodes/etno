using EtnoSelo.App.Models;
using EtnoSelo.App.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EtnoSelo.App.Controllers
{
    public class DrzaveController : Controller
    {
        // GET: Drzave
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            var drzavaServis = new DrzavaServis();

            var model = new DrzaveIndexVM();
            model.listaDrzava = drzavaServis.DobaviSveDrzave();
            
            return View(model);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult Dodaj()
        {
            var korisnikServis = new KorisnikServis();

            var model = new DrzavaDodajVM();

            return View(model);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult Dodaj(DrzavaDodajVM model)
        {
            var drzavaServis = new DrzavaServis();
            if (!ModelState.IsValid)
            {
             
                return View(model);
            }

            drzavaServis.DodajDrzavu(model);
          

            return RedirectToAction("Index");
        }


        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult Uredi(int Id)
        {
            var drzavaServis = new DrzavaServis();

            var model = new DrzavaUrediVM();

            model.drzava = drzavaServis.DobaviDrzavuPoId(Id);
           
            return View(model);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult Uredi(DrzavaUrediVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var drzavaServis = new DrzavaServis();

            drzavaServis.IzmjeniDrzavu(model);

            return RedirectToAction("Index");
        }
    }
}