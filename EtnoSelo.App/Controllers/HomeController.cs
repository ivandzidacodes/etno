using EtnoSelo.App.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace EtnoSelo.App.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Default()
        {
            //var a = SessionFacade.Korisnik;

            if (User.Identity.IsAuthenticated)
            {
                if (User.IsInRole("Klijent"))
                    return RedirectToAction("Index", "Ponude");
                else if (User.IsInRole("Recepcionar"))
                    return RedirectToAction("Index", "Rezervacije");
                else if (User.IsInRole("Admin"))
                    return RedirectToAction("Index", "Korisnici");
                else
                    return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}