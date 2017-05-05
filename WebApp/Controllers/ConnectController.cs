using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApp.Controllers
{
    public class ConnectController : Controller
    {
        // GET: Connect
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AddConnect()
        {
            return View();
        }
    }
}