using EtnoSelo.DAL.Models;
using EtnoSelo.DAL.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EtnoSelo.DAL
{
    public class RolaDao
    {
        public List<Models.Rola> DobaviRole()
        {
            using (var ctx = new ApplicationDbContext(Constants.CONNECTION_STRING))
            {
                return ctx.Rola.ToList();
            }
        }
    }
}
