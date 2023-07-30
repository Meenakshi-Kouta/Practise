using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LMSMVC.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Learning Management System";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Contaact Us";

            return View();
        }
    }
}