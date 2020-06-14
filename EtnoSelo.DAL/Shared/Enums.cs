using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EtnoSelo.DAL.Shared
{
    public enum LogNivo
    {
        Info = 1,
        Upozorenje,
        Greska
    }

    public enum Rola
    {
        Klijent = 1,
        Menadzer = 2,
        Recepcionar = 3,
        Administrator = 4
    }

}
