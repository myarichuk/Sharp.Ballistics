using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sharp.Ballistics.Training.Controllers
{
    public class SiteController : Controller
    {
        public ActionResult Home()
        {
            return View();
        }       

        public ActionResult About()
        {
            return View();
        }
    }
}