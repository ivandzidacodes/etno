using EtnoSelo.DAL.Models;
using EtnoSelo.DAL.Shared;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EtnoSelo.DAL
{
    public class KorisnikDao
    {
        /// <summary>
        /// Provjerava korisnika po imenu i lozinci
        /// </summary>
        /// <param name="korisnickoIme">korisnicko ime</param>
        /// <param name="lozinka">lozinka</param>
        /// <returns></returns>
        public Korisnik ProvjeriKorisnickiNalog(string korisnickoIme, string lozinka)
        {
            try
            {
                using (var ctx = new ApplicationDbContext(Constants.CONNECTION_STRING))
                {
                    var korisnik = ctx.Korisnik.Include(x => x.Rola).FirstOrDefault(x =>
                        string.Compare(x.KorisnickoIme, korisnickoIme, true) == 0 && x.Lozinka == lozinka);
                    //korisnik.Rola = ctx.Rola.Where(x => x.Id == korisnik.RolaId).FirstOrDefault();
                    return korisnik;
                }
            }
            catch (Exception ex)
            {
                LoggingHelper.Greska(ex, nameof(KorisnikDao), nameof(ProvjeriKorisnickiNalog));
                return null;
            }
        }

        /// <summary>
        /// Dodaje korisnika u bazu
        /// </summary>
        /// <param name="korisnik">Korisnik model</param>
        /// <returns>Id dodanog korisnika</returns>
        public int DodajKorisnickiNalog(Korisnik korisnik)
        {
            try
            {
                using (var ctx = new ApplicationDbContext(Constants.CONNECTION_STRING))
                {
                    ctx.Korisnik.Add(korisnik);
                    return ctx.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                LoggingHelper.Greska(ex, nameof(KorisnikDao), nameof(DodajKorisnickiNalog));
                return 0;
            }
        }

        /// <summary>
        /// Dobavi korisnika po id-u
        /// </summary>
        /// <param name="id">id korisnika</param>
        /// <returns>Korisnik</returns>
        public Korisnik DobaviKorisnikaPoId(int id)
        {
            try
            {
                using (var ctx = new ApplicationDbContext(Constants.CONNECTION_STRING))
                {
                    var korisnik = ctx.Korisnik.Include(x=>x.Grad.Drzava).FirstOrDefault(x => x.Id == id);
                    return korisnik;
                }
            }
            catch (Exception ex)
            {
                LoggingHelper.Greska(ex, nameof(KorisnikDao), nameof(DobaviKorisnikaPoId));
                return null;
            }
        }
        /// <summary>
        /// Provjerava da li je korisnicko ime zauzeto 
        /// i vraca korisnika sa datim imenom ako postoji
        /// </summary>
        /// <param name="korisnickoIme">Korisnicko ime</param>
        /// <returns>Korisnik sa daim imenom</returns>
        public Korisnik ProvjeriKorisnickoIme(string korisnickoIme)
        {
            try
            {
                using (var ctx = new ApplicationDbContext(Constants.CONNECTION_STRING))
                {
                    var korisnik = ctx.Korisnik.FirstOrDefault(x =>
                        string.Compare(x.KorisnickoIme, korisnickoIme, true) == 0);
                    return korisnik;
                }
            }
            catch (Exception ex)
            {
                LoggingHelper.Greska(ex, nameof(KorisnikDao), nameof(ProvjeriKorisnickoIme));
                return null;
            }
        }

        //Kod vezan za manipulaciju sa korisnicima cija rola je klijent
        #region Klijent
        /// <summary>
        /// Dobavi sve korisnike cija rola je klijent
        /// </summary>
        /// <returns>Lista korisnika</returns>
        public List<Korisnik> DobaviSveKlijente()
        {
            try
            {
                using (var ctx = new ApplicationDbContext(Constants.CONNECTION_STRING))
                {
                    //Dobavi sve korisnike i filtriraj po role == Klijent
                    var klijenti = ctx.Korisnik.Where(x => x.RolaId == (int)Shared.Rola.Klijent)
                        .Include(x =>x.Grad.Drzava).ToList();
                    return klijenti;
                }
            }
            catch (Exception ex)
            {
                LoggingHelper.Greska(ex, nameof(KorisnikDao), nameof(DobaviSveKlijente));
                return null;
            }
        }

        /// <summary>
        /// Dobavi klijente filtrirane po imenu i prezimenu
        /// Metoda radi contains imena i prezima po istom parametru
        /// </summary>
        /// <returns>Lista Klijenata</returns>
        public List<Korisnik> DobaviKlijentePoImenuIPrezimenu(string ime, string prezime)
        {
            try
            {
                using (var ctx = new ApplicationDbContext(Constants.CONNECTION_STRING))
                {
                    //Dobavi sve klijente filtrirane po imenu i prezimenu
                    //Uporedjuju se ime i prezime sa imenom i prezimenom obostrano
                    //Iz razloga sto korisnik moze unijeti "ime prezime" ili "prezime ime"
                    var klijenti = ctx.Korisnik.Where(x => x.RolaId == (int)Shared.Rola.Klijent &&
                        (x.Ime.Contains(ime) || x.Prezime.Contains(prezime)
                        || x.Ime.Contains(prezime) || x.Prezime.Contains(ime)))
                        .Include(x => x.Grad.Drzava).ToList();
                    return klijenti;
                }
            }
            catch (Exception ex)
            {
                LoggingHelper.Greska(ex, nameof(KorisnikDao), nameof(DobaviKlijentePoImenuIPrezimenu));
                return null;
            }
        }

        #endregion


        #region
        //Ivan Džida

        public List<Korisnik> DobaviSveKorisnike()
        {
            try
            {
                using (var ctx = new ApplicationDbContext(Constants.CONNECTION_STRING))
                {
                    //Dobavi sve korisnike
                    var korisnici = ctx.Korisnik.Where(x => x.RolaId != (int)Shared.Rola.Klijent)
                        .Include(x => x.Grad.Drzava).ToList();
                    return korisnici;
                }
            }
            catch (Exception ex)
            {
                LoggingHelper.Greska(ex, nameof(KorisnikDao), nameof(DobaviSveKorisnike));
                return null;
            }
        }

        public int DodajKorisnika(Korisnik korisnik)
        {
            try
            {
                using (var ctx = new ApplicationDbContext(Constants.CONNECTION_STRING))
                {
                    ctx.Korisnik.Add(korisnik);
                    return ctx.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                LoggingHelper.Greska(ex, nameof(KorisnikDao), nameof(DodajKorisnika));
                return 0;
            }
        }

        public int PrimjeniStatus(Korisnik korisnik)
        {
            try
            {
                using (var ctx = new ApplicationDbContext(Constants.CONNECTION_STRING))
                {
                    
                    Korisnik k = ctx.Korisnik.Where(x => x.Id == korisnik.Id).FirstOrDefault();
                    k.Aktivan = korisnik.Aktivan;
                    
                    return ctx.SaveChanges();

                }
            }
            catch (Exception ex)
            {
                LoggingHelper.Greska(ex, nameof(KorisnikDao), nameof(PrimjeniStatus));
                return 0;
            }
        }

        public int IzmjeniKorisnika(Korisnik korisnik)
        {
            try
            {
                using (var ctx = new ApplicationDbContext(Constants.CONNECTION_STRING))
                {
                    Korisnik k = ctx.Korisnik.Where(x => x.Id == korisnik.Id).FirstOrDefault();
                   
                    k.Ime = korisnik.Ime;
                    k.Prezime = korisnik.Prezime;
                    k.KorisnickoIme = korisnik.KorisnickoIme;
                    k.Email = korisnik.Email;
                    k.DatumZaposlenja = korisnik.DatumZaposlenja;
                    k.DatumRodjenja = korisnik.DatumRodjenja;
                    k.Adresa = korisnik.Adresa;
                    k.Telefon = korisnik.Telefon;

                    return ctx.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                LoggingHelper.Greska(ex, nameof(KorisnikDao), nameof(IzmjeniKorisnika));
                return 0;
            }
        }

        public List<Korisnik> DobaviNoveKorisnikePoDatumu(DateTime datumOd, DateTime datumDo)
        {
            try
            {
                using (var ctx = new ApplicationDbContext(Constants.CONNECTION_STRING))
                {
                    var _datumOd = datumOd.AddDays(-1);
                    var _datumDo = datumDo.AddDays(-1);
                    return ctx.Korisnik.Where(
                                        x => x.DatumZaposlenja > _datumOd 
                                        && 
                                        x.DatumZaposlenja < _datumDo)
                                        .ToList();
                }
            }
            catch (Exception ex)
            {
                LoggingHelper.Greska(ex, nameof(KorisnikDao), nameof(DobaviNoveKorisnikePoDatumu));
                return null;
            }
        }



        #endregion

    }
}
