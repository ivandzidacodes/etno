using EtnoSelo.App.Models;
using EtnoSelo.App.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace EtnoSelo.App.Controllers
{
    [Authorize(Roles ="Recepcionar")]
    public class RezervacijeController : Controller
    {
        private RezervacijaServis _rezService;
        public RezervacijeController()
        {
            _rezService = new RezervacijaServis();

        }

        [HttpGet]
        public ActionResult Index()
        {
            var refDate = DateTime.Now;

            var vmodel = new RezervcijaIndexVM();
            vmodel.Rezervacije = _rezService.DobaviSveRezervacije(refDate.AddYears(-2), DateTime.Now.AddDays(30));

            return View(vmodel);
        }

        [HttpGet]
        public PartialViewResult Izmjeni(int? id)
        {
            //za simulaciju loadera
            //System.Threading.Thread.Sleep(3000);
            if (id != null)
            {
                var vmodel = _rezService.IzmjeniRezervaciju(id.Value);
                return PartialView("_RezervacijaEditModal", vmodel);
            }
            

            Response.StatusCode = (int)HttpStatusCode.NotFound;
            Response.StatusDescription = "Nije pronađen zapis za prosljeđeni parametar.";

            return null;
        }
    }
}