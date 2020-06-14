using EtnoSelo.App.Models;
using EtnoSelo.App.Services;
using EtnoSelo.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;

namespace EtnoSelo.App.Shared
{
    public static class SessionFacade
    {
        public static Korisnik Korisnik
        {
            get
            {
                var korisnik = HttpContext.Current.Session[SessionVariables.LOGOVANI_KORISNIK];

                if(korisnik == null)
                {
                    var idClaim = ((ClaimsIdentity)HttpContext.Current.User.Identity).Claims.Where(x => x.Type == "Id").FirstOrDefault();

                    int id = 0;
                    int.TryParse(idClaim?.Value, out id);

                    if (id == 0)
                        return null;

                    KorisnikServis korisnikSvc = new KorisnikServis();

                    var tempKorisnik = korisnikSvc.DobaviKorinikaPoId(id);
                    HttpContext.Current.Session[SessionVariables.LOGOVANI_KORISNIK] = tempKorisnik;
                }

                return HttpContext.Current.Session[SessionVariables.LOGOVANI_KORISNIK] as Korisnik;
            }
            set
            {
                HttpContext.Current.Session.Add(SessionVariables.LOGOVANI_KORISNIK, value);
            }
        }

        public static int KorisnikId
        {
            get
            {
                var idClaim =  ((ClaimsIdentity)HttpContext.Current.User.Identity).Claims.Where(x => x.Type == "Id").FirstOrDefault();

                int id = 0;
                int.TryParse(idClaim?.Value, out id);

                return id;
            }
        }

        public static PonudaModel AktivnaPonuda
        {
            get
            {
                return HttpContext.Current.Session[SessionVariables.AKTIVNA_PONUDA] as PonudaModel;
            }
            set
            {
                HttpContext.Current.Session.Add(SessionVariables.AKTIVNA_PONUDA, value);
            }
        }

        public static void PocistiSesiju()
        {
             HttpContext.Current.Session.Clear();
        }
    }
}