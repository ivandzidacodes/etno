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
    public class UslugaDao
    {

        public List<UslugaRezervacija> DobaviUslugePoDatumuRezervacije(DateTime datumOd, DateTime datumDo)
        {
            try
            {
                using (var ctx = new ApplicationDbContext(Constants.CONNECTION_STRING))
                {
                    var dOd = datumOd.Date.AddDays(-1);
                    var dDo = datumDo.Date.AddDays(1);

                    return ctx.UslugaRezervacija
                        .Include(x => x.Rezervacija)
                        .Include(x => x.Usluga)
                        .Include(x => x.Usluga.TipNaplate)
                        .Where(x =>
                                x.Rezervacija.DatumDolaska > dOd &&
                                x.Rezervacija.DatumOdlaska < dDo).ToList();
                }
            }
            catch (Exception ex)
            {
                LoggingHelper.Greska(ex, nameof(UslugaDao), nameof(DobaviUslugePoDatumuRezervacije));
                return null;
            }
        }
    }
}
