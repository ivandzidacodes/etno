using EtnoSelo.DAL.Models;
using EtnoSelo.DAL.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EtnoSelo.DAL
{
    /// <summary>
    /// AdresaDao sadrzi metode za dobavljanje podataka 
    /// o adresi, gradu i drzavi iz baze
    /// </summary>
    public class AdresaDao
    {
        /// <summary>
        /// Dobavi sve drzave
        /// </summary>
        /// <returns>Lista drzava</returns>
        public List<Drzava> DobaviDrzave()
        {
            using (var ctx = new ApplicationDbContext(Constants.CONNECTION_STRING))
            {
                return ctx.Drzava.ToList();
            }
        }

        /// <summary>
        /// Dobavi sve gradove za datu drzavu
        /// </summary>
        /// <param name="drzava">Id Drzave</param>
        /// <returns>Lista gradova</returns>
        public List<Grad> DobaviGradovePoDrzavi(int drzava)
        {
            using (var ctx = new ApplicationDbContext(Constants.CONNECTION_STRING))
            {
                return ctx.Grad.Where(x => x.DrzavaId == drzava).ToList();
            }
        }

        //Ivan Džida

        public List<Grad> DobaviSveGradove()
        {
            using (var ctx = new ApplicationDbContext(Constants.CONNECTION_STRING))
            {
                return ctx.Grad.ToList();
            }
        }
    }
}
