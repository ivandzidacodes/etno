using EtnoSelo.App.Models;
using EtnoSelo.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EtnoSelo.App.Services
{
    public class IzvjestajServis
    {
        private JedinicaDao _jedinicaDao;
        private RezervacijaDao _rezervacijaDao;
        private UslugaDao _uslugaDao;
        private PonudaDao _ponudaDao;
        private KorisnikDao _korisniciDao;
        public IzvjestajServis()
        {
            _jedinicaDao = new JedinicaDao();
            _rezervacijaDao = new RezervacijaDao();
            _uslugaDao = new UslugaDao();
            _ponudaDao = new PonudaDao();
            _korisniciDao = new KorisnikDao();
        }

        public List<JedinicaModel> DobaviJedinicePoDatumuRezervacije(DateTime datumOd, DateTime datumDo)
        {
            try
            {
                return _jedinicaDao.DobaviSveJedinice(datumOd, datumDo).Select(x => new JedinicaModel
                {
                    Id = x.Id,
                    BrojIzdavanja = x.Rezervacije.Count,
                    Kapacitet = x.Kapacitet,
                    Naziv = x.Naziv,
                    TipSmjestaja = x.TipSmjestaja.Naziv
                }).ToList();

            }
            catch (Exception ex)
            {
                LoggingHelper.Greska(ex, nameof(IzvjestajServis), nameof(DobaviJedinicePoDatumuRezervacije));
                return null;
            }
        }

        public List<RezervacijaModel> DobaviPotvrdjeneRezervacijePoDatumu(DateTime datumOd, DateTime datumDo)
        {
            try
            {
                return _rezervacijaDao.DobaviPotvrdjeneRezervacijePoDatumu(datumOd, datumDo).Select(x =>
                    new RezervacijaModel
                    {
                        BrojOsoba = x.BrojOsoba,
                        DatumDolaska = x.DatumDolaska.ToShortDateString(),
                        DatumOdlaska = x.DatumOdlaska.ToShortDateString(),
                        Korisnik = x.Korisnik.KorisnickoIme,
                        Ocjena = x.Ocjena,
                        Popust = x.Popust,
                        Potvrdjena = x.Potvrdjena
                    }).ToList();
            }
            catch (Exception ex)
            {
                LoggingHelper.Greska(ex, nameof(IzvjestajServis), nameof(DobaviPotvrdjeneRezervacijePoDatumu));
                return null;
            }
        }

        public List<UslugaModel> DobaviUslugePoDatumu(DateTime datumOd, DateTime datumDo)
        {
            try
            {
                return _uslugaDao.DobaviUslugePoDatumuRezervacije(datumOd, datumDo).Select(x =>
                    new UslugaModel
                    {
                        Cijena = x.Usluga.Cijena,
                        DatumDolaska = x.Rezervacija.DatumDolaska.ToShortDateString(),
                        DatumOdlaska = x.Rezervacija.DatumOdlaska.ToShortDateString(),
                        Kolicina = (int)x.Kolicina,
                        Naziv = x.Usluga.Naziv,
                        TipNaplate = x.Usluga.TipNaplate.Naziv,
                        Ukupno = x.Kolicina * x.Cijena
                    }).ToList();
            }
            catch (Exception ex)
            {
                LoggingHelper.Greska(ex, nameof(IzvjestajServis), nameof(DobaviPotvrdjeneRezervacijePoDatumu));
                return null;
            }
        }

        public List<PonudaIzvjestajModel> DobaviPonudePoDatumuRezervacije(DateTime datumOd, DateTime datumDo)
        {
            try
            {
                return _ponudaDao.DobaviPonudePoDatumuRezervacije(datumOd, datumDo).Select(x =>
                    new PonudaIzvjestajModel
                    {
                        BrojRezervacija = x.Rezervacije.Count,
                        DatumKreiranja = x.DatumKreiranja.ToShortDateString(),
                        Naziv = x.Naziv,
                        Popust = (int)x.Popust,
                        VaziDo = x.DatumDo.ToShortDateString(),
                        VaziOd = x.DatumOd.ToShortDateString()
                    }).ToList();
            }
            catch (Exception ex)
            {
                LoggingHelper.Greska(ex, nameof(IzvjestajServis), nameof(DobaviPonudePoDatumuRezervacije));
                return null;
            }
        }

        //Ivan Džida
        public List<KorisniciIzvjestajModel> DobaviNoveKorisnikePoDatumu(DateTime datumOd, DateTime datumDo)
        {
            try
            {
                return _korisniciDao.DobaviNoveKorisnikePoDatumu(datumOd, datumDo).Select(x =>
                new KorisniciIzvjestajModel
                {
                    ImeIPrezime = x.Ime + " " + x.Prezime,
                    KorisnickoIme = x.KorisnickoIme,
                    Adresa = x.Adresa,
                    DatumZaposlenja = x.DatumZaposlenja

                }).ToList();
                
            }
            catch (Exception ex)
            {
                LoggingHelper.Greska(ex, nameof(IzvjestajServis), nameof(DobaviPonudePoDatumuRezervacije));
                return null;
            }
        }



    }
}