using EtnoSelo.DAL;
using EtnoSelo.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EtnoSelo.App.Services
{
    public class JedinicaServis
    {
        private JedinicaDao _jedinicaDAO;
        public JedinicaServis()
        {
            _jedinicaDAO = new JedinicaDao();
        }

        public List<Jedinica> DobaviJedinice()
        {
            return _jedinicaDAO.DobaviSveJedinice();
        }
    }
}