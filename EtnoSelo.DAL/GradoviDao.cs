using EtnoSelo.DAL.Models;
using EtnoSelo.DAL.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EtnoSelo.DAL
{
    public class GradoviDao
    {
        public List<Grad> DobaviSveGradove()
        {
            try
            {
                using (var ctx = new ApplicationDbContext(Constants.CONNECTION_STRING))
                {
                    //Dobavi sve gradove 
                    var grad = ctx.Grad.Include("Drzava").ToList();
                    return grad;
                }
            }
            catch (Exception ex)
            {
                LoggingHelper.Greska(ex, nameof(GradoviDao), nameof(DobaviSveGradove));
                return null;
            }
        }

        public Grad ProvjeriNazivGrada(string naziv)
        {
            try
            {
                using (var ctx = new ApplicationDbContext(Constants.CONNECTION_STRING))
                {
                    var grad = ctx.Grad.FirstOrDefault(x =>
                        string.Compare(x.Naziv, naziv, true) == 0);
                    return grad;
                }
            }
            catch (Exception ex)
            {
                LoggingHelper.Greska(ex, nameof(GradoviDao), nameof(ProvjeriNazivGrada));
                return null;
            }
        }

        public int DodajGrad(Grad grad)
        {
            try
            {
                using (var ctx = new ApplicationDbContext(Constants.CONNECTION_STRING))
                {
                    ctx.Grad.Add(grad);
                    return ctx.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                LoggingHelper.Greska(ex, nameof(GradoviDao), nameof(DodajGrad));
                return 0;
            }
        }

        public int IzmjeniGrad(Grad grad)
        {
            try
            {
                using (var ctx = new ApplicationDbContext(Constants.CONNECTION_STRING))
                {
                    Grad g = ctx.Grad.Where(x => x.Id == grad.Id).FirstOrDefault();

                    g.Naziv = grad.Naziv;
                    g.DrzavaId = grad.DrzavaId;


                    return ctx.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                LoggingHelper.Greska(ex, nameof(GradoviDao), nameof(IzmjeniGrad));
                return 0;
            }
        }

        public Grad DobaviGradPoId(int id)
        {
            try
            {
                using (var ctx = new ApplicationDbContext(Constants.CONNECTION_STRING))
                {
                    var grad = ctx.Grad.FirstOrDefault(x => x.Id == id);
                    return grad;
                }
            }
            catch (Exception ex)
            {
                LoggingHelper.Greska(ex, nameof(GradoviDao), nameof(DobaviGradPoId));
                return null;
            }
        }



    }
}
