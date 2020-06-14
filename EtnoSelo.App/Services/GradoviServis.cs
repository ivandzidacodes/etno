using EtnoSelo.App.Models;
using EtnoSelo.DAL;
using EtnoSelo.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EtnoSelo.App.Services
{
    
    public class GradoviServis
    {
        GradoviDao _gradoviDao;

        public GradoviServis()
        {
            _gradoviDao = new GradoviDao();
        }

        public List<Grad> DobaviSveGradove()
        {

            return _gradoviDao.DobaviSveGradove().ToList();
        }

        public Grad DobaviGradPoId(int id)
        {
            return _gradoviDao.DobaviGradPoId(id);

        }

        public int DodajGrad(GradoviDodajVM grad)
        {
            //Provjeri da li je naziv grada zauzet
            var postojeciGrad = _gradoviDao.ProvjeriNazivGrada(grad.grad.Naziv);

            //Ako je naziv grada zauzet vrati -1
            if (postojeciGrad != null)
                return -1;

            //Premapiraj db model
            Grad model = new Grad()
            {
                Naziv = grad.grad.Naziv,
                DrzavaId = grad.grad.DrzavaId


            };

            //Dodaj grad u bazu
            return _gradoviDao.DodajGrad(model);
        }


        
        public int IzmjeniGrad(GradUrediVM grad)
        {
            Grad model = _gradoviDao.DobaviGradPoId(grad.grad.Id);
            model.Naziv = grad.grad.Naziv;
            
            _gradoviDao.IzmjeniGrad(model);

            return 0;
        }
        


    }
}