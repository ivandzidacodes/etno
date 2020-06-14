using EtnoSelo.DAL.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EtnoSelo.DAL
{
    public static class LoggingHelper
    {
        public static void Info(string poruka, string klasa, string metoda)
        {
            using (var ctx = new ApplicationDbContext(Constants.CONNECTION_STRING))
            {
                ctx.Logovi.Add(new Models.Log()
                {
                    Nivo = LogNivo.Info.ToString(),
                    Poruka = poruka,
                    Klasa = klasa,
                    Metoda = metoda
                });

                ctx.SaveChanges();
            }
        }

        public static void Upozorenje(Exception izuzetak, string klasa = null, string metoda = null, string poruka = null)
        {
            using (var ctx = new ApplicationDbContext(Constants.CONNECTION_STRING))
            {
                ctx.Logovi.Add(new Models.Log()
                {
                    Nivo = LogNivo.Upozorenje.ToString(),
                    Poruka = poruka ?? izuzetak?.Message,
                    Klasa = klasa,
                    Metoda = metoda,
                    Izuzetak = (poruka == null && izuzetak?.InnerException != null) ?
                        izuzetak.InnerException?.Message : izuzetak?.Message
                });

                ctx.SaveChanges();
            }
        }

        public static void Greska(Exception izuzetak, string klasa = null, string metoda = null, string poruka = null)
        {
            using (var ctx = new ApplicationDbContext(Constants.CONNECTION_STRING))
            {
                ctx.Logovi.Add(new Models.Log()
                {
                    Nivo = LogNivo.Greska.ToString(),
                    Poruka = poruka ?? izuzetak?.Message,
                    Klasa = klasa,
                    Metoda = metoda,
                    Izuzetak = (poruka == null && izuzetak?.InnerException != null) ? 
                        izuzetak.InnerException?.Message : izuzetak?.Message
                });

                ctx.SaveChanges();
            }
        }
    }
}
