using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Services;
using Webshop.Classes;
using Webshop.Models;

namespace Webshop.Controllers
{
    public class ShopCartAPIController : ApiController
    {
        [HttpGet]
        [Route("api/ShopCartAPI/UpdateQuantity")]
        public IHttpActionResult UpdateQuantity(int id, int qty)
        {
            List<ShopCart> shopCart = SessionManager.CartList;

            var Item = shopCart.Find(c => c.ID == id);
            shopCart.Remove(Item);

            ShopCart newItem = new ShopCart();

            newItem.ID = id;
            newItem.Quantity = qty;

            shopCart.Add(newItem);

            SessionManager.CartList = shopCart;

            return Ok();
        }




        [HttpGet]
        [Route("api/ShopCartAPI/Delete")]
        public IHttpActionResult Delete(int id)
        {
            List<ShopCart> shopCart = SessionManager.CartList;

            var Item = shopCart.Find(c => c.ID == id);
            shopCart.Remove(Item);

            SessionManager.CartList = shopCart;

            HttpContext.Current.Session["count"] = Convert.ToInt32(HttpContext.Current.Session["count"]) - 1;
            
            return Ok();

        }
    }
}
