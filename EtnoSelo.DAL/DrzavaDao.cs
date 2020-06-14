using EtnoSelo.DAL.Models;
using EtnoSelo.DAL.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EtnoSelo.DAL
{
    public class DrzavaDao
    {
        public int DodajDrzavu(Drzava drzava) {
            try
            {
                using (var ctx = new ApplicationDbContext(Constants.CONNECTION_STRING))
                {
                    ctx.Drzava.Add(drzava);
                    return ctx.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                LoggingHelper.Greska(ex, nameof(DrzavaDao), nameof(DodajDrzavu));
                return 0;
            }
        }

        public List<Drzava> DobaviSveDrzave()
        {
            try
            {
                using (var ctx = new ApplicationDbContext(Constants.CONNECTION_STRING))
                {
                    //Dobavi sve drzave 
                    var drzave = ctx.Drzava.ToList();
                    return drzave;
                }
            }
            catch (Exception ex)
            {
                LoggingHelper.Greska(ex, nameof(DrzavaDao), nameof(DobaviSveDrzave));
                return null;
            }
        }

        public int IzmjeniDrzavu(Drzava drzava)
        {
            try
            {
                using (var ctx = new ApplicationDbContext(Constants.CONNECTION_STRING))
                {
                    Drzava d = ctx.Drzava.Where(x => x.Id == drzava.Id).FirstOrDefault();

                    d.Naziv = drzava.Naziv;
                    

                    return ctx.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                LoggingHelper.Greska(ex, nameof(DrzavaDao), nameof(IzmjeniDrzavu));
                return 0;
            }
        }

        public Drzava ProvjeriNazivDrzave(string naziv)
        {
            try
            {
                using (var ctx = new ApplicationDbContext(Constants.CONNECTION_STRING))
                {
                    var drzava = ctx.Drzava.FirstOrDefault(x =>
                        string.Compare(x.Naziv, naziv, true) == 0);
                    return drzava;
                }
            }
            catch (Exception ex)
            {
                LoggingHelper.Greska(ex, nameof(DrzavaDao), nameof(ProvjeriNazivDrzave));
                return null;
            }
        }

        public Drzava DobaviDrzavuPoId(int id)
        {
            try
            {
                using (var ctx = new ApplicationDbContext(Constants.CONNECTION_STRING))
                {
                    var drzava = ctx.Drzava.FirstOrDefault(x => x.Id == id);
                    return drzava;
                }
            }
            catch (Exception ex)
            {
                LoggingHelper.Greska(ex, nameof(DrzavaDao), nameof(DobaviDrzavuPoId));
                return null;
            }
        }

    }
    
}
