using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace fbpm.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Welcome to Dhruthi Infra Projects - FBPM Application";

            return View();
        }

        public ActionResult About()
        {
            return View();
        }
    }
}
