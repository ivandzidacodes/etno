using EtnoSelo.App.Models;
using EtnoSelo.App.Shared;
using EtnoSelo.DAL;
using EtnoSelo.DAL.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EtnoSelo.App.Services
{
    public class PonudaServis
    {
        PonudaDao _ponudaDao;
        JedinicaDao _jedinicaDao;
        public PonudaServis()
        {
            _ponudaDao = new PonudaDao();
            _jedinicaDao = new JedinicaDao();
        }
        /// <summary>
        /// Dobavi sve aktivne ponude i mapiraj u ponuda model
        /// </summary>
        /// <returns>lista ponuda</returns>
        public List<PonudaModel> DobaviAktivnePonude()
        {
            try
            {
                //dobavi ponude iz baze
                var tempPonude = _ponudaDao.DobaviAktivnePonude();

                //mapiraj u view model
                return tempPonude.Select(x => new PonudaModel()
                {
                    Id = x.Id,
                    DatumKreiranja = x.DatumKreiranja.ToShortDateString(),
                    Popust = (int)x.Popust,
                    VaziDo = x.DatumDo.ToShortDateString(),
                    VaziOd = x.DatumOd.ToShortDateString(),
                    Naziv = x.Naziv
                }).ToList();
            }
            catch (Exception ex)
            {
                LoggingHelper.Greska(ex, nameof(PonudaServis), nameof(DobaviAktivnePonude));
                return null;
            }
        }

        /// <summary>
        /// Dobavi tipove smjestaja iz baze i mapiraj u select list model
        /// Iz liste su izbaceni tipovi koji nemaju niti jednu jedinicu
        /// </summary>
        /// <returns>Select item list model</returns>
        public List<SelectListItem> DobaviTipoveSmjestaja()
        {
            try
            {
                //dobavi tipove smjestaja iz baze
                var tempTipovi = _ponudaDao.DobaviTipoveSmjestaja();

                //Dobavi tipove i izbaci one koji nemaju niti jednu jedinicu i 
                //mapiraj tipove smjestaja u select list item model
                return tempTipovi.Where(x=>x.Jedinice.Count > 0).Select(x =>
                    new SelectListItem()
                    {
                        Text = x.Naziv,
                        Value = x.Id.ToString()
                    }).ToList();
            }
            catch (Exception ex)
            {
                LoggingHelper.Greska(ex, nameof(PonudaServis), nameof(DobaviTipoveSmjestaja));
                return null;
            }
        }

        /// <summary>
        /// Dobavi jedinice iz baze filtrirane po tipu smjestaja
        /// </summary>
        /// <returns>Select item list model</returns>
        public List<JedinicaModel> DobaviJediniceFiltriranePoTipuSmjestaja(int tipSmjestajaId)
        {
            try
            {
                //dobavi jedinice iz baze
                var tempTipovi = _ponudaDao.DobaviJedinicePoTipu(tipSmjestajaId);

                return tempTipovi.Select(x =>
                    new JedinicaModel()
                    {
                        Naziv = x.Naziv,
                        Id = x.Id,
                        Cijena = x.JediniceCijenici.FirstOrDefault() == null ? -1 : x.JediniceCijenici.FirstOrDefault().Cijena
                    }).ToList();
            }
            catch (Exception ex)
            {
                LoggingHelper.Greska(ex, nameof(PonudaServis), nameof(DobaviJediniceFiltriranePoTipuSmjestaja));
                return null;
            }
        }

        /// <summary>
        /// Dobavi Listu aktivnosti iz baze
        /// </summary>
        /// <returns>Select item list model</returns>
        public List<SelectListItem> DobaviSveAktivnosti()
        {
            try
            {  
                //dobavi aktivnosti iz baze
                var tempAktivnosti = _ponudaDao.DobaviSveAktivnosti();

                //Mapiraj aktivnosti u select list item model
                return tempAktivnosti.Select(x =>
                    new SelectListItem()
                    {
                        Text = $"{x.Naziv} (Oprema: {x.Oprema})",
                        Value = x.Id.ToString()
                    }).ToList();
            }
            catch (Exception ex)
            {
                LoggingHelper.Greska(ex, nameof(PonudaServis), nameof(DobaviSveAktivnosti));
                return null;
            }
        }

        public bool DodajPonudu(PonudaModel ponuda)
        {
            try
            {
                var tempPonuda = new Ponuda()
                {
                    DatumDo = DateTime.Parse(ponuda.VaziDo),
                    DatumOd = DateTime.Parse(ponuda.VaziOd),
                    DatumKreiranja = DateTime.Now,
                    KorisnikId = SessionFacade.Korisnik.Id,
                    Naziv = ponuda.Naziv,
                    Popust = ponuda.Popust,
                    Id = ponuda.Id
                };

                var novaPonuda = _ponudaDao.DodajPonudu(tempPonuda);
                Helpers.Helpers.SnimiSlikeUFolder(novaPonuda.Id.ToString(), ponuda.Slike);

                _ponudaDao.ObrisiJedinicaPonudaPoPonudaId(novaPonuda.Id);
                _ponudaDao.ObrisiPonudaAktivnostPoPonudaId(novaPonuda.Id);

                var tempJedinicePonude = new List<JedinicaPonuda>();

                foreach (var item in ponuda.PonudeJedinice)
                {
                    tempJedinicePonude.Add(new JedinicaPonuda()
                    {
                        Cijena = item.CijenaBezPopusta,
                        JedinicaId = item.StanbenaJedinica,
                        PonudaId = novaPonuda.Id,
                        PoreznaStopa = 17,
                    });
                }

                var hasErrors = false;

                hasErrors = !_ponudaDao.DodajJedinice(tempJedinicePonude);


                var tempAktivnosti = new List<PonudaAktivnost>();

                foreach (var item in ponuda.Aktivnosti)
                {
                    tempAktivnosti.Add(new PonudaAktivnost()
                    {
                        AktivnostId = item.Aktivnost,
                        PonudaId = novaPonuda.Id
                    });
                }

                hasErrors = !_ponudaDao.DodajAktivnosti(tempAktivnosti);

                if (hasErrors)
                    throw new Exception("Greska prilikom dodavanja nove ponude!!!");

            }
            catch (Exception ex)
            {
                LoggingHelper.Greska(ex, nameof(PonudaServis), nameof(DodajPonudu));
                return false;
            }
            return true;
        }

        public PonudaModel DobaviPonuduPoId(int id)
        {
            try
            {
                var ponuda = _ponudaDao.DobaviPonuduPoId(id);

                var ponudaModel = new PonudaModel
                {
                    DatumKreiranja = ponuda.DatumKreiranja.ToShortDateString(),
                    VaziOd = ponuda.DatumOd.ToShortDateString(),
                    VaziDo = ponuda.DatumDo.ToShortDateString(),
                    Id = ponuda.Id,
                    Naziv = ponuda.Naziv,
                    Popust = (int)ponuda.Popust,
                };

                ponudaModel.Aktivnosti = ponuda.PonudeAktivnosti.Select(x => new PonudeAktivnostiModel
                {
                    Aktivnost = x.AktivnostId
                }).ToList();

                ponudaModel.PonudeJedinice = ponuda.JedinicePonude.Select(x => new PonudaJedinicaModel
                {
                    CijenaBezPopusta = x.Cijena,
                    CijenaSaPopustom = (100 - ponuda.Popust) * x.Cijena,
                    StanbenaJedinica = x.JedinicaId,
                    TipStanbeneJedinice = _jedinicaDao.DobaviJedinicuPoId(x.JedinicaId).TipSmjestajaId
                }).ToList();


                return ponudaModel;
            }
            catch (Exception ex)
            {
                LoggingHelper.Greska(ex, nameof(PonudaServis), nameof(DobaviPonuduPoId));
            }
            return null;
        }

        public bool ObrisiPonuduPoId(int id)
        {
            try
            {
                _ponudaDao.ObrisiJedinicaPonudaPoPonudaId(id);
                _ponudaDao.ObrisiPonudaAktivnostPoPonudaId(id);
                _ponudaDao.ObrisiPonuduPoId(id);                
            }
            catch (Exception ex)
            {
                LoggingHelper.Greska(ex, nameof(PonudaServis), nameof(ObrisiPonuduPoId));
            }
            return true;
        }
    }
}