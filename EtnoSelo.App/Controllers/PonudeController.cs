using EtnoSelo.App.Models;
using EtnoSelo.App.Services;
using EtnoSelo.App.Shared;
using EtnoSelo.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EtnoSelo.App.Controllers
{
    public class PonudeController : Controller
    {
        PonudaServis _ponudaServis;
        public PonudeController()
        {
            _ponudaServis = new PonudaServis();
        }
        // GET: Ponude
        public ActionResult Index()
        {
            var model = _ponudaServis.DobaviAktivnePonude();

            return View(model);
        }

        public ActionResult Uredi(int id)
        {
            var ponuda = InicijalizujPonuduPoId(id);

            SessionFacade.AktivnaPonuda = ponuda;

            return View("KreirajPonudu", ponuda);
        }

        public ActionResult Detalji(int id)
        {
            var ponuda = InicijalizujPonuduPoId(id);

            SessionFacade.AktivnaPonuda = ponuda;

            ViewData["PreusmjeriNa"] = "Index";

            return View("PregledPonude", ponuda);
        }

        public ActionResult Obrisi(int id)
        {
            _ponudaServis.ObrisiPonuduPoId(id);
            return RedirectToAction("Index");
        }

        public ActionResult KreirajPonudu(bool kreirajNovu = true)
        {
            PonudaModel ponudaModel = null;

            if (!kreirajNovu)
                ponudaModel = SessionFacade.AktivnaPonuda;

            if (ponudaModel == null)
            {
                ponudaModel = new PonudaModel()
                {
                    VaziOd = DateTime.Now.Date.ToShortDateString(),//.ToString("dd/MM/yyyy").Replace('.', '/'),
                    VaziDo = DateTime.Now.Date.ToShortDateString(),//.ToString("dd/MM/yyyy").Replace('.', '/'),
                    Popust = 20,
                    PonudeJedinice = new List<PonudaJedinicaModel>()
                {
                    InicijalizujPonudaJedinicaModel()
                },
                    Aktivnosti = new List<PonudeAktivnostiModel>()
                {
                    new PonudeAktivnostiModel()
                    {
                        Aktivnost = int.Parse(_ponudaServis.DobaviSveAktivnosti().FirstOrDefault().Value),
                        Aktivnosti = _ponudaServis.DobaviSveAktivnosti()
                    }
                }
                };
            }

            SessionFacade.AktivnaPonuda = ponudaModel;

            return View(ponudaModel);
        }

        public ActionResult DodajPonudu(PonudaModel model)
        {
            //Inicijalizuj listu ako je null
            if (model.PonudeJedinice == null)
                model.PonudeJedinice = new List<PonudaJedinicaModel>();

            //Inicijalizuj listu ako je null
            if (model.Aktivnosti == null)
                model.Aktivnosti = new List<PonudeAktivnostiModel>();

            //dodaj Jedinicu
            if (model.Dodaj == 1)
                model.PonudeJedinice.Add(InicijalizujPonudaJedinicaModel());

            //else if model.dodaj == 2 dodaj aktivnost
            else
                model.Aktivnosti.Add(new PonudeAktivnostiModel()
                {
                    Aktivnost = int.Parse(_ponudaServis.DobaviSveAktivnosti().FirstOrDefault().Value),
                    Aktivnosti = _ponudaServis.DobaviSveAktivnosti()
                });

            //Populisi drodown liste za stanbene jedinice za svaku red
            foreach (var item in model.PonudeJedinice)
            {
                item.StanbeneJedinice = _ponudaServis.DobaviJediniceFiltriranePoTipuSmjestaja(item.TipStanbeneJedinice);
                item.TipoviStanbenihJedinica = _ponudaServis.DobaviTipoveSmjestaja();
            }

            //Populisti dropdwon listu aktivnosti za svaki red
            foreach (var item in model.Aktivnosti)
                item.Aktivnosti = _ponudaServis.DobaviSveAktivnosti();

            SessionFacade.AktivnaPonuda = model;

            return View("KreirajPonudu", model);
        }

        public ActionResult ObrisiStavkuIzPonude(PonudaModel model)
        {
            //Inicijalizuj listu
            if (model.PonudeJedinice == null)
                model.PonudeJedinice = new List<PonudaJedinicaModel>();
            //Inicijalizuj listu
            if (model.Aktivnosti == null)
                model.Aktivnosti = new List<PonudeAktivnostiModel>();

            //Pronadji jedinicu sa prosljedjenim Guidom i izbrisi je ako postoji
            var tempModel = model.PonudeJedinice.FirstOrDefault(x => x.MyGuid == model.Guid);
            if (tempModel != null)
                model.PonudeJedinice.Remove(tempModel);
            
            //Pronadji aktivnosti sa porsljedjenim Guidom i izbrisi je ako postoji
            var tempModel2 = model.Aktivnosti.FirstOrDefault(x => x.MyGuid == model.Guid);
            if(tempModel2 != null)
                    model.Aktivnosti.Remove(tempModel2);


            //Populisi drodown liste za stanbene jedinice za svaku red
            foreach (var item in model.PonudeJedinice)
            {
                item.StanbeneJedinice = _ponudaServis.DobaviJediniceFiltriranePoTipuSmjestaja(item.TipStanbeneJedinice);
                item.TipoviStanbenihJedinica = _ponudaServis.DobaviTipoveSmjestaja();
            }

            //Populisti dropdwon listu aktivnosti za svaki red
            foreach (var item in model.Aktivnosti)
                item.Aktivnosti = _ponudaServis.DobaviSveAktivnosti();

            SessionFacade.AktivnaPonuda = model;

            return View("KreirajPonudu", model);
        }

        public ActionResult PregledPonude(PonudaModel model)
        {
            //Inicijalizuj listu
            if (model.PonudeJedinice == null)
                model.PonudeJedinice = new List<PonudaJedinicaModel>();
            //Inicijalizuj listu
            if (model.Aktivnosti == null)
                model.Aktivnosti = new List<PonudeAktivnostiModel>();

            //Populisi drodown liste za stanbene jedinice za svaku red
            foreach (var item in model.PonudeJedinice)
            {
                item.StanbeneJedinice = _ponudaServis.DobaviJediniceFiltriranePoTipuSmjestaja(item.TipStanbeneJedinice);
                item.TipoviStanbenihJedinica = _ponudaServis.DobaviTipoveSmjestaja();
                float tempVal = 1 - (float)model.Popust / 100;
                item.CijenaSaPopustom = tempVal * item.CijenaBezPopusta;
            }

            //Populisti dropdwon listu aktivnosti za svaki red
            foreach (var item in model.Aktivnosti)
                item.Aktivnosti = _ponudaServis.DobaviSveAktivnosti();

            //Ako model nije validan vrati poruku nazad
            if(!ModelState.IsValid)
                return View("KreirajPonudu", model);

            //Ako model ne sadrzi niti jednu jedinicu onda nije validan i prikazi poruku
            if (model.PonudeJedinice.Count < 1)
            {
                ModelState.AddModelError("", "Ponuda mora sadrzavati najmanje jednu jedinicu!!!");
                return View("KreirajPonudu", model);
            }

            SessionFacade.AktivnaPonuda = model;

            ViewData["PreusmjeriNa"] = "KreirajPonudu";

            return View(model);
        }

        public ActionResult Snimi()
        {
            _ponudaServis.DodajPonudu(SessionFacade.AktivnaPonuda);

            return RedirectToAction("Index");
        }

        public JsonResult DobaviStanbeneJedinice(int tipStanbeneJedinice)
        {
            var jedinice = _ponudaServis
                    .DobaviJediniceFiltriranePoTipuSmjestaja(tipStanbeneJedinice);

            return Json(jedinice, JsonRequestBehavior.AllowGet);
        }

        #region privatne metode

        /// <summary>
        /// Inicijalizuj model sa pocetnim vrijednostima
        /// </summary>
        /// <returns>Inicijalizovani model</returns>
        private PonudaJedinicaModel InicijalizujPonudaJedinicaModel()
        {
            try
            {
                var tipoviSmjestaja = _ponudaServis.DobaviTipoveSmjestaja();

                var jedinice = _ponudaServis
                    .DobaviJediniceFiltriranePoTipuSmjestaja(int.Parse(tipoviSmjestaja.FirstOrDefault().Value));

                var tempModel = new PonudaJedinicaModel()
                {
                    TipStanbeneJedinice = int.Parse(tipoviSmjestaja.FirstOrDefault().Value),
                    TipoviStanbenihJedinica = tipoviSmjestaja,
                    StanbeneJedinice = jedinice,
                    StanbenaJedinica = jedinice.FirstOrDefault().Id,
                    CijenaBezPopusta = jedinice.FirstOrDefault().Cijena,
                    //Uracunaj inicijalni popust od 20%
                    CijenaSaPopustom = (float)(jedinice.FirstOrDefault().Cijena * 0.8)
                };

                return tempModel;
            }
            catch (Exception ex)
            {
                LoggingHelper.Greska(ex, nameof(PonudeController), nameof(InicijalizujPonudaJedinicaModel));
                return null;
            }
        }

        private PonudaModel InicijalizujPonuduPoId(int id)
        {
            var ponuda = _ponudaServis.DobaviPonuduPoId(id);

            //Populisi drodown liste za stanbene jedinice za svaku red
            foreach (var item in ponuda.PonudeJedinice)
            {
                item.StanbeneJedinice = _ponudaServis.DobaviJediniceFiltriranePoTipuSmjestaja(item.TipStanbeneJedinice);
                item.TipoviStanbenihJedinica = _ponudaServis.DobaviTipoveSmjestaja();
            }

            //Populisti dropdwon listu aktivnosti za svaki red
            foreach (var item in ponuda.Aktivnosti)
                item.Aktivnosti = _ponudaServis.DobaviSveAktivnosti();

            ponuda.NaziviSlika = Helpers.Helpers.DobaviImenaNaziveSlikaZaPonudu(id);

            return ponuda;
        }

        #endregion
    }
}