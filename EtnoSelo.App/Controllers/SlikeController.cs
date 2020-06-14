using EtnoSelo.DAL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EtnoSelo.App.Controllers
{
    public class SlikeController : Controller
    {
        // GET: Slike
        [HttpGet]
        public ActionResult Index(int ponudaId, string naziv)
        {
            try
            {
                var radniDirektorij = System.Web.HttpContext.Current.Server.MapPath("~/Slike");

                var path = radniDirektorij + "\\" + ponudaId.ToString() + "\\" + naziv;

                if(System.IO.File.Exists(path))
                    return File(path, "image/jpeg");
                return Redirect("http://www.epurvanchal.com/media/k2/items/cache/d3b3799d6611d677944f5f86a500beb3_XL.jpg");
            }
            catch(Exception ex)
            {

                LoggingHelper.Greska(ex, nameof(SlikeController), nameof(Index));
                return Redirect("http://www.epurvanchal.com/media/k2/items/cache/d3b3799d6611d677944f5f86a500beb3_XL.jpg");
            }
        }
    }
}