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
    public class JedinicaDao
    {
        public Jedinica DobaviJedinicuPoId(int id)
        {
            try
            {
                using (var ctx = new ApplicationDbContext(Constants.CONNECTION_STRING))
                {
                    return ctx.Jedinica.FirstOrDefault(x => x.Id == id);
                }
            }
            catch (Exception ex)
            {
                LoggingHelper.Greska(ex, nameof(JedinicaDao), nameof(DobaviJedinicuPoId));
                return null;
            }
        }

        public List<Jedinica> DobaviSveJedinice(DateTime datumOd, DateTime datumDo)
        {
            try
            {
                using (var ctx = new ApplicationDbContext(Constants.CONNECTION_STRING))
                {
                    var dOd = datumOd.Date.AddDays(-1);
                    var dDo = datumDo.Date.AddDays(1);

                    return ctx.Jedinica.Include(y => y.Rezervacije)
                        .Include(x => x.TipSmjestaja)
                        .OrderBy(x => x.Rezervacije
                            .Count(y => 
                                y.DatumDolaska > dOd &&
                                y.DatumOdlaska < dDo))
                            .Where(z => 
                                z.Rezervacije.Count(y => 
                                    y.DatumDolaska > dOd &&
                                    y.DatumOdlaska < dDo) > 0)
                        .ToList();
                }
            }
            catch (Exception ex)
            {
                LoggingHelper.Greska(ex, nameof(JedinicaDao), nameof(DobaviSveJedinice));
                return null;
            }
        }

        public List<Jedinica> DobaviSveJedinice()
        {
            try
            {
                using (var ctx = new ApplicationDbContext(Constants.CONNECTION_STRING))
                {
                    return ctx.Jedinica.ToList();
                }
            }
            catch (Exception ex)
            {
                LoggingHelper.Greska(ex, nameof(JedinicaDao), nameof(DobaviSveJedinice));
                return null;
            }
        }
    }
}
