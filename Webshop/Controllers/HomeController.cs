using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Webshop.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var count = Session["count"];

            if (Session["count"] == null)
            ViewBag.ItemCount = "No items in cart yet";
            else
            ViewBag.ItemCount = count;

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}