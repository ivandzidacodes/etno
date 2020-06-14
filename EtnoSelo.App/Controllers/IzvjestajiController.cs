using EtnoSelo.App.Models;
using EtnoSelo.App.Services;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using iTextSharp.tool.xml;

namespace EtnoSelo.App.Controllers
{
    public class IzvjestajiController : Controller
    {
        private IzvjestajServis _izvjestajServis;
        public IzvjestajiController()
        {
            _izvjestajServis = new IzvjestajServis();
        }
        // GET: Izvjestaji
        public ActionResult Jedinice(string datumOd, string datumDo, bool izvjestaj = false)
        {
            ViewBag.Izvjestaj = izvjestaj;

            var DatumDo = DateTime.Now.Date;
            var DatumOd = DateTime.Now.Date.AddMonths(-3);

            if (!string.IsNullOrWhiteSpace(datumOd))
                DateTime.TryParse(datumOd, out DatumOd);

            if (!string.IsNullOrWhiteSpace(datumDo))
                DateTime.TryParse(datumDo, out DatumDo);

            var model = new JedinicaIzvjestajModel
            {
                Jedinice = _izvjestajServis.DobaviJedinicePoDatumuRezervacije(DatumOd, DatumDo),
                DatumDo = DatumDo.ToShortDateString(),
                DatumOd = DatumOd.ToShortDateString(),
            };

            if (!izvjestaj)
                return View(model);

            Helpers.Helpers.ConvertRazorViewToPdfAndreturnAsResponse("Jedinice", model, ControllerContext, ViewData, TempData, Response);

            return null;
        }

        public ActionResult Rezervacije(string datumOd, string datumDo, bool izvjestaj = false)
        {
            ViewBag.Izvjestaj = izvjestaj;
            var DatumDo = DateTime.Now.Date;
            var DatumOd = DateTime.Now.Date.AddMonths(-3);

            if (!string.IsNullOrWhiteSpace(datumOd))
                DateTime.TryParse(datumOd, out DatumOd);

            if (!string.IsNullOrWhiteSpace(datumDo))
                DateTime.TryParse(datumDo, out DatumDo);

            var model = new RezervacijaIzvjestajModel
            {
                Rezervacije = _izvjestajServis.DobaviPotvrdjeneRezervacijePoDatumu(DatumOd, DatumDo),
                DatumDo = DatumDo.ToShortDateString(),
                DatumOd = DatumOd.ToShortDateString()
            };

            if (!izvjestaj)
                return View(model);

            Helpers.Helpers.ConvertRazorViewToPdfAndreturnAsResponse("Rezervacije", model, ControllerContext, ViewData, TempData, Response);
            return null;
        }

        public ActionResult Usluge(string datumOd, string datumDo, bool izvjestaj = false)
        {
            ViewBag.Izvjestaj = izvjestaj;
            var DatumDo = DateTime.Now.Date;
            var DatumOd = DateTime.Now.Date.AddMonths(-3);

            if (!string.IsNullOrWhiteSpace(datumOd))
                DateTime.TryParse(datumOd, out DatumOd);

            if (!string.IsNullOrWhiteSpace(datumDo))
                DateTime.TryParse(datumDo, out DatumDo);

            var model = new UslugaIzvjestajModel
            {
                Usluge = _izvjestajServis.DobaviUslugePoDatumu(DatumOd, DatumDo),
                DatumDo = DatumDo.ToShortDateString(),
                DatumOd = DatumOd.ToShortDateString()
            };

            if (!izvjestaj)
                return View(model);

            Helpers.Helpers.ConvertRazorViewToPdfAndreturnAsResponse("Usluge", model, ControllerContext, ViewData, TempData, Response);
            return null;
        }

        public ActionResult Ponude(string datumOd, string datumDo, bool izvjestaj = false)
        {
            ViewBag.Izvjestaj = izvjestaj;
            var DatumDo = DateTime.Now.Date;
            var DatumOd = DateTime.Now.Date.AddMonths(-3);

            if (!string.IsNullOrWhiteSpace(datumOd))
                DateTime.TryParse(datumOd, out DatumOd);

            if (!string.IsNullOrWhiteSpace(datumDo))
                DateTime.TryParse(datumDo, out DatumDo);

            var model = new PonudaIzvjestajListModel
            {
                Ponude = _izvjestajServis.DobaviPonudePoDatumuRezervacije(DatumOd, DatumDo),
                DatumDo = DatumDo.ToShortDateString(),
                DatumOd = DatumOd.ToShortDateString()
            };

            if (!izvjestaj)
                return View(model);

            Helpers.Helpers.ConvertRazorViewToPdfAndreturnAsResponse("Ponude", model, ControllerContext, ViewData, TempData, Response);
            return null;
        }

        //Ivan Džida
        public ActionResult NoviKorisnici(string datumOd, string datumDo, bool izvjestaj = false) {
         {
                ViewBag.Izvjestaj = izvjestaj;
                var DatumDo = DateTime.Now.Date;
                var DatumOd = DateTime.Now.Date.AddMonths(-3);

                if (!string.IsNullOrWhiteSpace(datumOd))
                    DateTime.TryParse(datumOd, out DatumOd);

                if (!string.IsNullOrWhiteSpace(datumDo))
                    DateTime.TryParse(datumDo, out DatumDo);
                
                var model = new KorisniciIzvjestajListModel
                {
                    Korisnici = _izvjestajServis.DobaviNoveKorisnikePoDatumu(DatumOd, DatumDo),
                    DatumDo = DatumDo.ToShortDateString(),
                    DatumOd = DatumOd.ToShortDateString()
                };

                if (!izvjestaj)
                    return View(model);

                Helpers.Helpers.ConvertRazorViewToPdfAndreturnAsResponse("NoviKorisnici", model, ControllerContext, ViewData, TempData, Response);
                return null;

    
            }
        }
        
    }
}