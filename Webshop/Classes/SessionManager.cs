using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using Webshop.Models;

namespace Webshop.Classes
{
    public class SessionManager
    {
        public static List<ShopCart> CartList
        {
            get { return HttpContext.Current.Session["cart"] as List<ShopCart>; }
            set { HttpContext.Current.Session["cart"] = value; }
        }
    }

}

