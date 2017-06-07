using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Webshop.Models;

namespace Webshop.Classes
{
    public class SessionManager
    {
        public static List<OrderDetail> CartList
        {
            get { return (List<OrderDetail>)HttpContext.Current.Session["cart"]; }
            set { HttpContext.Current.Session["cart"] = value; }
        }
    }

}

