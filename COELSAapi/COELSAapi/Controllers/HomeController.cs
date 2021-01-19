using COELSAapi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

namespace COELSAapi.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            UtilCodes.DestinatarioEmail = WebConfigurationManager.AppSettings["DestinatarioEmail"];
            ViewBag.Title = "Home Page";

            return View();
        }

        
    }
}
