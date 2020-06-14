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
    public class PonudaDao
    {
        /// <summary>
        /// Dobavi ponude kojima je datum vazenja manji od trenutnog datuma
        /// </summary>
        /// <returns>Lista ponuda</returns>
        public List<Ponuda> DobaviAktivnePonude()
        {
            try
            {
                using (var ctx = new ApplicationDbContext(Constants.CONNECTION_STRING))
                {
                    return ctx.Ponuda.Where(x => x.DatumDo <= DateTime.Now).ToList();
                }
            }
            catch (Exception ex)
            {
                LoggingHelper.Greska(ex, nameof(PonudaDao), nameof(DobaviAktivnePonude));
                return null;
            }
        }

        public Ponuda DobaviPonuduPoId(int id)
        {
            try
            {
                using (var ctx = new ApplicationDbContext(Constants.CONNECTION_STRING))
                {
                    return ctx.Ponuda
                        .Include(x => x.PonudeAktivnosti)
                        .Include(x => x.JedinicePonude)
                        .FirstOrDefault(x => x.Id == id);
                }
            }
            catch (Exception ex)
            {
                LoggingHelper.Greska(ex, nameof(PonudaDao), nameof(DobaviPonuduPoId));
                return null;
            }
        }

        public Ponuda ObrisiPonuduPoId(int id)
        {
            try
            {
                using (var ctx = new ApplicationDbContext(Constants.CONNECTION_STRING))
                {
                    var ponuda = ctx.Ponuda.Remove(ctx.Ponuda.FirstOrDefault(x => x.Id == id));
                    ctx.SaveChanges();
                    return ponuda;
                }
            }
            catch (Exception ex)
            {
                LoggingHelper.Greska(ex, nameof(PonudaDao), nameof(ObrisiPonuduPoId));
                return null;
            }
        }

        public IEnumerable<PonudaAktivnost> ObrisiPonudaAktivnostPoPonudaId(int id)
        {
            try
            {
                using (var ctx = new ApplicationDbContext(Constants.CONNECTION_STRING))
                {
                    var obrisani = ctx.PonudeAktivnosti.RemoveRange(ctx.PonudeAktivnosti.Where(x => x.PonudaId == id));
                    ctx.SaveChanges();
                    return obrisani;
                }
            }
            catch (Exception ex)
            {
                LoggingHelper.Greska(ex, nameof(PonudaDao), nameof(ObrisiPonudaAktivnostPoPonudaId));
                return null;
            }
        }

        public IEnumerable<JedinicaPonuda> ObrisiJedinicaPonudaPoPonudaId(int id)
        {
            try
            {
                using (var ctx = new ApplicationDbContext(Constants.CONNECTION_STRING))
                {
                    var obrisani =  ctx.JedinicaPonuda.RemoveRange(ctx.JedinicaPonuda.Where(x => x.PonudaId == id));
                    ctx.SaveChanges();
                    return obrisani;
                }
            }
            catch (Exception ex)
            {
                LoggingHelper.Greska(ex, nameof(PonudaDao), nameof(ObrisiJedinicaPonudaPoPonudaId));
                return null;
            }
        }

        /// <summary>
        /// Dobavi sve tipove smjestaja
        /// </summary>
        /// <returns>Lsita tipova smjestaja</returns>
        public List<TipSmjestaja> DobaviTipoveSmjestaja()
        {
            try
            {
                using (var ctx = new ApplicationDbContext(Constants.CONNECTION_STRING))
                {
                    return ctx.TipSmjestaja.Include(x => x.Jedinice).ToList();
                }
            }
            catch (Exception ex)
            {
                LoggingHelper.Greska(ex, nameof(PonudaDao), nameof(DobaviTipoveSmjestaja));
                return null;
            }
        }

        /// <summary>
        /// Dobavi jedinice filtrirane po tipu smjestaja
        /// </summary>
        /// <param name="tipId">tip smjestaja id</param>
        /// <returns>Lista jedinica</returns>
        public List<Jedinica> DobaviJedinicePoTipu(int tipId)
        {
            try
            {
                using (var ctx = new ApplicationDbContext(Constants.CONNECTION_STRING))
                {
                    return ctx.Jedinica.Include(x=>x.JediniceCijenici).Where(x => x.TipSmjestajaId == tipId).ToList();
                }
            }
            catch (Exception ex)
            {
                LoggingHelper.Greska(ex, nameof(PonudaDao), nameof(DobaviJedinicePoTipu));
                return null;
            }
        }
        /// <summary>
        /// Dobavi sve aktivnosti
        /// </summary>
        /// <returns>Lista aktivnosti</returns>
        public List<Aktivnost> DobaviSveAktivnosti()
        {
            try
            {
                using (var ctx = new ApplicationDbContext(Constants.CONNECTION_STRING))
                {
                    return ctx.Aktivnost.ToList();
                }
            }
            catch (Exception ex)
            {
                LoggingHelper.Greska(ex, nameof(PonudaDao), nameof(DobaviSveAktivnosti));
                return null;
            }
        }

        public Ponuda DodajPonudu(Ponuda ponuda)
        {
            try
            {
                using (var ctx = new ApplicationDbContext(Constants.CONNECTION_STRING))
                {

                    var tempPonuda = ctx.Ponuda.Where(x => x.Id == ponuda.Id).FirstOrDefault();

                    if(tempPonuda == null)
                        tempPonuda = ctx.Ponuda.Add(ponuda);
                    else
                    {
                        tempPonuda.DatumDo = ponuda.DatumDo;
                        tempPonuda.DatumOd = ponuda.DatumOd;
                        tempPonuda.Naziv = ponuda.Naziv;
                        tempPonuda.Popust = ponuda.Popust;
                    }

                    ctx.SaveChanges();

                    return tempPonuda;
                }
            }
            catch (Exception ex)
            {
                LoggingHelper.Greska(ex, nameof(PonudaDao), nameof(DodajPonudu));
                return null;
            }
        }

        public bool DodajJedinice(List<JedinicaPonuda> jedinice)
        {
            try
            {
                using (var ctx = new ApplicationDbContext(Constants.CONNECTION_STRING))
                {
                    ctx.JedinicaPonuda.AddRange(jedinice);
                    ctx.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                LoggingHelper.Greska(ex, nameof(PonudaDao), nameof(DodajJedinice));
                return false;
            }
            return true;
        }

        public bool DodajAktivnosti(List<PonudaAktivnost> aktivnosti)
        {
            try
            {
                using (var ctx = new ApplicationDbContext(Constants.CONNECTION_STRING))
                {
                    ctx.PonudeAktivnosti.AddRange(aktivnosti);
                    ctx.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                LoggingHelper.Greska(ex, nameof(PonudaDao), nameof(DodajAktivnosti));
                return false;
            }
            return true;
        }


        public List<Ponuda> DobaviPonudePoDatumuRezervacije(DateTime datumOd, DateTime datumDo)
        {
            try
            {
                using (var ctx = new ApplicationDbContext(Constants.CONNECTION_STRING))
                {
                    var dOd = datumOd.Date.AddDays(-1);
                    var dDo = datumDo.Date.AddDays(1);

                    return ctx.Ponuda.Include(x => x.Rezervacije).Where(x =>
                      x.DatumOd > dOd &&
                      x.DatumDo < dDo).ToList();
                }
            }
            catch (Exception ex)
            {
                LoggingHelper.Greska(ex, nameof(PonudaDao), nameof(DobaviPonudePoDatumuRezervacije));
                return null;
            }
        }
    }
}
