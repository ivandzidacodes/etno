using EtnoSelo.App.Models;
using EtnoSelo.DAL;
using EtnoSelo.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EtnoSelo.App.Services
{
    public class DrzavaServis
    {
        DrzavaDao _drzavaDao;

        public DrzavaServis()
        {
            _drzavaDao = new DrzavaDao();
        }

        public int DodajDrzavu(DrzavaDodajVM drzava)
        {
            //Provjeri da li je naziv drzave zauzet
            var postojecaDrzava = _drzavaDao.ProvjeriNazivDrzave(drzava.Naziv);

            //Ako je naziv drzave zauzet vrati -1
            if (postojecaDrzava != null)
                return -1;

            //Premapiraj db model
            Drzava model = new Drzava()
            {
                Naziv = drzava.Naziv


            };

            //Dodaj drzavu u bazu
            return _drzavaDao.DodajDrzavu(model);
        }

        public List<Drzava> DobaviSveDrzave() {

            return _drzavaDao.DobaviSveDrzave().ToList();
        }

        public Drzava DobaviDrzavuPoId(int id)
        {
            return _drzavaDao.DobaviDrzavuPoId(id);
           
        }

        public int IzmjeniDrzavu(DrzavaUrediVM drzava)
        {
            Drzava model = _drzavaDao.DobaviDrzavuPoId(drzava.drzava.Id);
            model.Naziv = drzava.drzava.Naziv;
            
            _drzavaDao.IzmjeniDrzavu(model);
            

            return 0;
        }

    }
}