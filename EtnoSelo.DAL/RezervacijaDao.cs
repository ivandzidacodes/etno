using EtnoSelo.DAL.Models;
using EtnoSelo.DAL.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace EtnoSelo.DAL
{
    public class RezervacijaDao
    {

        public List<Rezervacija> DobaviPotvrdjeneRezervacijePoDatumu(DateTime datumOd, DateTime datumDo)
        {
            try
            {
                using (var ctx = new ApplicationDbContext(Constants.CONNECTION_STRING))
                {
                    var dOd = datumOd.Date.AddDays(-1);
                    var dDo = datumDo.Date.AddDays(1);

                    return ctx.Rezervacija.Include(x => x.Korisnik).Where(x =>
                      x.Potvrdjena &&
                      x.DatumDolaska > dOd &&
                      x.DatumOdlaska < dDo).ToList();
                }
            }
            catch (Exception ex)
            {
                LoggingHelper.Greska(ex, nameof(RezervacijaDao), nameof(DobaviPotvrdjeneRezervacijePoDatumu));
                return null;
            }
        }

        public List<Rezervacija> DobaviSveRezervacijePoDatumu(DateTime datumOd, DateTime datumDo)
        {
            try
            {
                using (var ctx = new ApplicationDbContext(Constants.CONNECTION_STRING))
                {
                    var dOd = datumOd.Date;
                    var dDo = datumDo.Date.AddDays(1);

                    return ctx.Rezervacija
                        .Include(x => x.Korisnik)
                        .Include(x => x.Ponuda)
                        .Include(x => x.Jedinica.Objekt)
                        .Where( x => x.DatumDolaska > dOd &&
                                x.DatumOdlaska < dDo)
                        .ToList();
                }
            }
            catch (Exception ex)
            {
                LoggingHelper.Greska(ex, nameof(RezervacijaDao), nameof(DobaviSveRezervacijePoDatumu));
                return null;
            }
        }

        public Rezervacija DobaviRezervaciju(int id) {
            using (var ctx = new ApplicationDbContext(Constants.CONNECTION_STRING)) {
                var test = ctx.Rezervacija
                    .Include(x => x.Korisnik)
                    .Include(x => x.Jedinica)
                    .Include(x => x.Ponuda)
                    .Include(x => x.UslugaRezervacija)
                    .Include(x => x.Ucesnici)
                    .FirstOrDefault(x => x.Id == id);
                return test;
            }
        }
    }
}
