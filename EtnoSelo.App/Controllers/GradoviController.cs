using EtnoSelo.App.Models;
using EtnoSelo.App.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EtnoSelo.App.Controllers
{
    public class GradoviController : Controller
    {
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            var gradoviServis = new GradoviServis();
            

            var model = new GradoviIndexVM();
            model.gradoviLista = gradoviServis.DobaviSveGradove();
           
            return View(model);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult Dodaj()
        {
            
            var drzavaServis = new DrzavaServis();

            var model = new GradoviDodajVM();

            model.listaDrzava = drzavaServis.DobaviSveDrzave().Select(x=> new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Naziv
            }).ToList();

            return View(model);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult Dodaj(GradoviDodajVM model)
        {
            var gradoviServis = new GradoviServis();
            var drzavaServis = new DrzavaServis();

            if (!ModelState.IsValid)
            {
                model.listaDrzava = drzavaServis.DobaviSveDrzave().Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.Naziv
                }).ToList();

                return View(model);
            }

            gradoviServis.DodajGrad(model);
            
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult Uredi(int Id)
        {

            var gradServis = new GradoviServis();

            var model = new GradUrediVM();

            model.grad = gradServis.DobaviGradPoId(Id);

            return View(model);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult Uredi(GradUrediVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var gradServis = new GradoviServis();

            gradServis.IzmjeniGrad(model);

            return RedirectToAction("Index");
        }

    }
}