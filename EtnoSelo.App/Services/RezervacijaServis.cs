using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EtnoSelo.DAL;
using EtnoSelo.App.Models;
using System.ComponentModel.DataAnnotations;
using EtnoSelo.DAL.Models;
using System.Web.Mvc;

namespace EtnoSelo.App.Services
{
    public class RezervacijaServis
    {
        private RezervacijaDao _rezervacijaDAO;
        private JedinicaDao _jedinicaDAO;
        public RezervacijaServis()
        {
            _rezervacijaDAO = new RezervacijaDao();
            _jedinicaDAO = new JedinicaDao();
        }

        public List<RezervacijaStavkaVM> DobaviSveRezervacije(DateTime datumOd,DateTime datumDo) {

            try
            {
                //dobavi ponude iz baze
                var rezrvacije = _rezervacijaDAO.DobaviSveRezervacijePoDatumu(datumOd,datumDo);


                //mapiraj u view model
                return rezrvacije.Select(x => new RezervacijaStavkaVM()
                {
                    BrojOsoba = x.BrojOsoba,
                    DatumDolaska = x.DatumDolaska,
                    DatumOdlaska = x.DatumOdlaska,
                    DatumPotvrdjivanja = x.DatumPotvrdjivanja,
                    Id = x.Id,
                    Jedinica = x.Jedinica,
                    NosiocRezrvacijie = x.Korisnik,
                    Ponuda = x.Ponuda,
                    Popust = x.Popust,
                    Potvrdjena = x.Potvrdjena
                }).ToList();
            }
            catch (Exception ex)
            {
                LoggingHelper.Greska(ex, nameof(PonudaServis), nameof(DobaviSveRezervacije));
                return null;
            }
        }

        public RezervacijaIzmjeniVM IzmjeniRezervaciju(int id)
        {
            var rez = _rezervacijaDAO.DobaviRezervaciju(id);
            var jedinice = _jedinicaDAO.DobaviSveJedinice();

            return new RezervacijaIzmjeniVM
            {
                Aktivnosti = rez.Ucesnici.ToList(),
                UslugeURezervaciji = rez.UslugaRezervacija
                .Select(x => new UslugaRezervacijeStavkaVM {
                    Id = x.Id,
                    UslugaId = x.UslugaId,
                    Cijena = x.Usluga.Cijena,
                    Kolicina = x.Kolicina,
                    PoreznaStopa = x.PoreznaStopa,
                    NazivUsluge = x.Usluga.Naziv,
                    TipNaplate = x.Usluga.TipNaplate.Naziv,
                    TipNaplateId = x.Usluga.TipNaplateId
                }).ToList(),

                Korisnik = rez.Korisnik,
                Ponuda = rez.Ponuda,

                JedinicaId = rez.JedinicaId,
                Jedinice = jedinice
                    .Select(x => 
                        new SelectListItem {
                            Text = x.Naziv,
                            Value = x.Id.ToString()
                        }).ToList(),

                BrojOsoba = rez.BrojOsoba,
                DatumDolaska = rez.DatumDolaska,
                DatumOdlaska = rez.DatumOdlaska,
                DatumPotvrdjivanja = rez.DatumPotvrdjivanja,
                Id = rez.Id,
                Komentar = rez.Komentar,
                Ocjena = rez.Ocjena,
                Popust = rez.Popust,
                Potvrdjena = rez.Potvrdjena  
            };
        }
    }
}