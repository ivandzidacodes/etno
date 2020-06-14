using EtnoSelo.App.Models;
using EtnoSelo.App.Services;
using EtnoSelo.App.Shared;
using EtnoSelo.DAL;
using EtnoSelo.DAL.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace EtnoSelo.App.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
    {
        KorisnikServis _korisnikServis;

        public LoginController()
        {
            _korisnikServis = new KorisnikServis();
        }

        // GET: Logon
        [HttpGet]
        public ActionResult Index()
        {
            return View("Prijava");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Prijava(LoginModel model)
        {

            //Provjeri korisnika u bazi po korisnickom imenu i lozinci
            var korisnik = _korisnikServis.DobaviKorisnikaPoKredencijalima(model.KorisnickoIme, model.Lozinka);

            //Ako korisnik sa datim podatcima ne postoji ili nije aktivan vrati na login i prikazi error
            if(korisnik?.Aktivan != true)
            {
                ModelState.AddModelError("", "Korisnicko ime i/ili lozinka neispravni!");
                return View(model);
            }

            var ctx = Request.GetOwinContext();
            var auth = ctx.Authentication;

            //Ako korisnik postoji i aktivan je keriraj claims i uradi sign in
            var claims = new ClaimsIdentity(
                new[] {
                    new Claim(ClaimTypes.Name, korisnik.Ime + " " + korisnik.Prezime),
                    new Claim("Id", korisnik.Id.ToString()),
                    new Claim(ClaimTypes.Role, korisnik.Rola.Naziv),
                    new Claim(ClaimTypes.NameIdentifier, korisnik.KorisnickoIme)
                }, 
                "ApplicationCookie");

            //Logiraj korisnika - Dodaj auth cookie
            auth.SignIn(claims);


            //Dodaj korisnika u sesiju za kasnije koristenje
            SessionFacade.Korisnik = korisnik;

            //Prikazi home page za logiranog korisnika
            return RedirectToAction("Default", "Home");
        }

        public ActionResult Odjava()
        {
            var ctx = Request.GetOwinContext();
            var auth = ctx.Authentication;

            //Odjavi korisnika - Pocisti auth cookie
            auth.SignOut("ApplicationCookie");

            //izbrisi sve vrijednosti iz sesije
            SessionFacade.PocistiSesiju();

            //Prikazi login page
            return RedirectToAction("Index", "Login");
        }
    }
}