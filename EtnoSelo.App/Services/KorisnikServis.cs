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
    public class KorisnikServis
    {
        KorisnikDao _korisnikDao;
        AdresaDao _adresaDao;
        RolaDao _rolaDao;
        public KorisnikServis()
        {
            _adresaDao = new AdresaDao();
            _korisnikDao = new KorisnikDao();
            _rolaDao = new RolaDao();
        }
        /// <summary>
        /// Mapiraj korisnika u db model i dodaj u bazu
        /// </summary>
        /// <param name="korisnik">Korisnik model</param>
        /// <returns>ID novog korisnika</returns>
        public int RegistrujKorisnika(KorisnikModel korisnik)
        {
            //Provjeri da li je korisnicko ime zauzeto
            var postojeciKorisnik = _korisnikDao.ProvjeriKorisnickoIme(korisnik.KorisnickoIme);

            //Ako je korisnicko ime zauzeto vrati -1
            if(postojeciKorisnik != null)
                return -1;

            //Premapiraj db model
            Korisnik model = new Korisnik()
            {
                Adresa = korisnik.Adresa,
                Email = korisnik.Email,
                Ime = korisnik.Ime,
                KorisnickoIme = korisnik.KorisnickoIme,
                Lozinka = korisnik.Lozinka,
                Prezime = korisnik.Prezime,
                Telefon = korisnik.Telefon,
                //TODO - Prepraviti na pravu vrijednost
                DatumRodjenja = DateTime.Now.AddYears(-100),
                DatumZaposlenja = DateTime.Now.AddYears(-100),
                GradId = korisnik.Grad,
                RolaId = (int)EtnoSelo.DAL.Shared.Rola.Klijent,
                Aktivan = true
            };

            //Dodaj korisnika u bazu
            return _korisnikDao.DodajKorisnickiNalog(model);
        }

        public Korisnik DobaviKorinikaPoId(int id)
        {
            return _korisnikDao.DobaviKorisnikaPoId(id);
        }

        public Korisnik DobaviKorisnikaPoKredencijalima(string korisnickoIme, string lozinka)
        {
            return _korisnikDao.ProvjeriKorisnickiNalog(korisnickoIme, lozinka);
        }

        /// <summary>
        /// Inicijalizuj model sa pocetnim vrijednostima
        /// </summary>
        /// <returns>Inicijalizovani model</returns>
        public KorisnikModel InicijalizujModel()
        {
            var korisnik = new KorisnikModel();

            try
            {
                korisnik.Drzava = 1;
                korisnik.Grad = 1;

                //Dobavi sve drzave i konvertuj u SelectListItem
                korisnik.Drzave = DobaviDrzave();

                //Dobavi gradove po drzavi i konvertuj u SelectListItem
                korisnik.Gradovi = DobaviSelectListuGradovaPoDrzavi(korisnik.Drzava);
            }
            catch(Exception ex)
            {
                LoggingHelper.Greska(ex, nameof(KorisnikServis), nameof(InicijalizujModel));
                return null;
            }

            return korisnik;
        }

        /// <summary>
        /// Dobavi sve drzave i mapiraj u SelectListItem 
        /// </summary>
        /// <returns>Lista drzava</returns>
        public List<SelectListItem> DobaviDrzave()
        {
            return _adresaDao.DobaviDrzave().Select(x => new SelectListItem()
            {
                Text = x.Naziv,
                Value = x.Id.ToString()
            }).ToList();
        }

        /// <summary>
        /// Dobavi gradove po drzavi i mapiraj u SelectListItem
        /// </summary>
        /// <returns>Lista gradova</returns>
        ///
        public List<SelectListItem> DobaviSelectListuGradovaPoDrzavi(int drzavaId)
        {
            return _adresaDao.DobaviGradovePoDrzavi(drzavaId).Select(x => new SelectListItem()
            {
                Text = x.Naziv,
                Value = x.Id.ToString()
            }).ToList();
        }

        /// <summary>
        /// Dobavi klijenta po id-u
        /// </summary>
        /// <param name="id">ID Korisnika</param>
        /// <returns>Korisnik</returns>
        public KlijentModel DobaviKlijentaPoId(int id)
        {
            try
            {
                var korisnikTemp = _korisnikDao.DobaviKorisnikaPoId(id);

                return MapirajKlijenta(korisnikTemp);
            }
            catch (Exception ex)
            {
                LoggingHelper.Greska(ex, nameof(KorisnikServis), nameof(DobaviKlijentaPoId));
                return null;
            }
        }

        /// <summary>
        /// Dobavi gradove po drzavi i mapiraj u view model
        /// </summary>
        /// <param name="drzavaId">Drzava id</param>
        /// <returns>Lista gradova</returns>
        public List<GradModel> DobaviGradovePoDrzavi(int drzavaId)
        {
            try
            {
                var gradovi = _adresaDao.DobaviGradovePoDrzavi(drzavaId).Select(x => new GradModel()
                {
                    Id = x.Id,
                    Naziv = x.Naziv
                }).ToList();

                return gradovi;
            }
            catch(Exception ex)
            {
                LoggingHelper.Greska(ex, nameof(KorisnikServis), nameof(DobaviGradovePoDrzavi));
                return null;
            }
        }

        /// <summary>
        /// Dobavi sve korisnike cija rola je Klijent
        /// I mapiraj u KlijentModel
        /// </summary>
        /// <returns>Lista Klijenata</returns>
        public List<KlijentModel> DobaviSveKlijente()
        {
            try
            {
                //Mapiraj korisnike u klijen model
                return _korisnikDao.DobaviSveKlijente().Select(x => MapirajKlijenta(x)).ToList();
            }
            catch (Exception ex)
            {
                LoggingHelper.Greska(ex, nameof(KorisnikServis), nameof(DobaviSveKlijente));
                return null;
            }
        }

        /// <summary>
        /// Dobavi klijente po imenu i prezimenu i mapiraj u KlijentModel
        /// </summary>
        /// <param name="imePrezime">Ime i prezime klijenta</param>
        /// <returns>Lista Klijenata</returns>
        public List<KlijentModel> DobaviKlijentePoImenuIPrezimenu(string imePrezime)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(imePrezime))
                    return null;

                //Izbaci nepotrebne space znakove i podjeli po spaces
                //Pretpostavka je da su unesene samo dvije rijeci
                //Ako su unesene vise od dvije rijeci bice ignorirane
                var imeIPrezime = imePrezime.Trim().Replace("  ", " ").Split(' ');

                //Prva rijec je mapirana u ime i ako realno to moze biti i prezime
                string ime = imeIPrezime[0];

                //Prezime je druga rijec, ako ista postoji, i ako realno to moze biti i ime
                string prezime = null;
                if (imeIPrezime.Length > 1)
                    prezime = imeIPrezime[1];

                //Mapiraj korisnike u klijen model
                return _korisnikDao.DobaviKlijentePoImenuIPrezimenu(ime, prezime).Select(x => MapirajKlijenta(x)).ToList();
            }
            catch (Exception ex)
            {
                LoggingHelper.Greska(ex, nameof(KorisnikServis), nameof(DobaviKlijentePoImenuIPrezimenu));
                return null;
            }
        }

        #region privatne metode

        /// <summary>
        /// Mapiraj korisnik model u KlijentModel
        /// </summary>
        /// <param name="korisnik">Korisnik</param>
        /// <returns>Klijent</returns>
        private KlijentModel MapirajKlijenta(Korisnik korisnik)
        {
            try
            {
                return new KlijentModel()
                {
                    Id = korisnik.Id,
                    Ime = korisnik.Ime,
                    Prezime = korisnik.Prezime,
                    Telefon = korisnik.Telefon,
                    Email = korisnik.Email,
                    Adresa = korisnik.Adresa,
                    Aktivan = korisnik.Aktivan,
                    DatumRodjenja = korisnik.DatumRodjenja.ToShortDateString(),
                    DatumZaposlenja = korisnik.DatumZaposlenja.ToShortDateString(),
                    Drzava = korisnik.Grad?.Drzava?.Naziv,
                    Grad = korisnik.Grad?.Naziv
                };
            }
            catch (Exception ex)
            {
                LoggingHelper.Greska(ex, nameof(KorisnikServis), nameof(MapirajKlijenta));
                throw;
            }
        }

        #endregion

        #region
        //Ivan Džida

        public List<Korisnik> DobaviSveKorisnike()
        {
            return _korisnikDao.DobaviSveKorisnike().ToList();
        }

        public int DodajNovogKorisnika(Korisnik korisnik)
        {
            //Provjeri da li je korisnicko ime zauzeto
            var postojeciKorisnik = _korisnikDao.ProvjeriKorisnickoIme(korisnik.KorisnickoIme);

            //Ako je korisnicko ime zauzeto vrati -1
            if (postojeciKorisnik != null)
                return -1;

            Korisnik model = new Korisnik()
            {
                Adresa = korisnik.Adresa,
                Email = korisnik.Email,
                Ime = korisnik.Ime,
                KorisnickoIme = korisnik.KorisnickoIme,
                Lozinka = korisnik.Lozinka,
                Prezime = korisnik.Prezime,
                Telefon = korisnik.Telefon,
                //TODO - Prepraviti na pravu vrijednost
                DatumRodjenja = DateTime.Now.AddYears(-100),
                DatumZaposlenja = DateTime.Now,
                GradId = korisnik.Grad.Id,
                RolaId = (int)EtnoSelo.DAL.Shared.Rola.Klijent,
                Aktivan = true
            };

            //Dodaj korisnika u bazu
            _korisnikDao.DodajKorisnickiNalog(model);

            return 0;
        }

        public int IzmjeniKorisnika(KorisniciUrediVM korisnik)
        {

            Korisnik model = _korisnikDao.DobaviKorisnikaPoId(korisnik.korisnik.Id);

            model.Ime = korisnik.korisnik.Ime;
            model.Prezime = korisnik.korisnik.Prezime;
            model.KorisnickoIme = korisnik.korisnik.KorisnickoIme;
            model.Email = korisnik.korisnik.Email;
            model.DatumZaposlenja = korisnik.korisnik.DatumZaposlenja;
            model.DatumRodjenja = korisnik.korisnik.DatumRodjenja;
            model.Adresa = korisnik.korisnik.Adresa;
            model.Telefon = korisnik.korisnik.Telefon;

            //dodati jos rolu i grad


            _korisnikDao.IzmjeniKorisnika(model);

            return 0;
        }

        public int PromjeniStatus(int Id)
        {
            Korisnik model = _korisnikDao.DobaviKorisnikaPoId(Id);
            model.Aktivan = !model.Aktivan;
            _korisnikDao.PrimjeniStatus(model);

            return 0;
        }

        public int DodajKorisnika(KorisniciDodajVM korisnik)
        {
            //Provjeri da li je korisnicko ime zauzeto
            var postojeciKorisnik = _korisnikDao.ProvjeriKorisnickoIme(korisnik.korisnik.KorisnickoIme);

            //Ako je korisnicko ime zauzeto vrati -1
            if (postojeciKorisnik != null)
                return -1;

            Korisnik model = new Korisnik()
            {
                Ime = korisnik.korisnik.Ime,
                Prezime = korisnik.korisnik.Prezime,
                KorisnickoIme = korisnik.korisnik.KorisnickoIme,
                Lozinka = korisnik.korisnik.Lozinka,
                Email = korisnik.korisnik.Email,
                DatumRodjenja = korisnik.korisnik.DatumRodjenja,
                DatumZaposlenja = korisnik.korisnik.DatumZaposlenja,
                Adresa = korisnik.korisnik.Adresa,
                Telefon = korisnik.korisnik.Telefon,
                Aktivan = true,
                RolaId = korisnik.korisnik.RolaId,
                GradId = korisnik.korisnik.GradId

            };
            
           
            //Dodaj korisnika u bazu
            return _korisnikDao.DodajKorisnickiNalog(model);
        }

        public List<GradModel> DobaviSveGradove()
        {
            try
            {
                var gradovi = _adresaDao.DobaviSveGradove().Select(x => new GradModel()
                {
                    Id = x.Id,
                    Naziv = x.Naziv
                }).ToList();

                return gradovi;
            }
            catch (Exception ex)
            {
                LoggingHelper.Greska(ex, nameof(KorisnikServis), nameof(DobaviSveGradove));
                return null;
            }
        }

        public List<Models.Rola> DobaviRole()
        {
            try
            {
                var role = _rolaDao.DobaviRole().Select(x => new Models.Rola()
                {
                    Id = x.Id,
                    Naziv = x.Naziv,
                    Opis = x.Opis
                }).ToList();

                return role;
            }
            catch (Exception ex)
            {
                LoggingHelper.Greska(ex, nameof(KorisnikServis), nameof(DobaviRole));
                return null;
            }
        }


        #endregion


    }



}