using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;

namespace Webshop.Controllers
{
    public class ShopBasketController : Controller
    {
        // GET: ShopBasket
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult OrderNow(int id)
        {
            return Content(id.ToString());
        }

    }
}
