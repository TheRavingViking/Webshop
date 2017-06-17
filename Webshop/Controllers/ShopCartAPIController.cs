using System;
using System.Collections.Generic;
using System.Data.Entity;
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
        private ShopEntities db = new ShopEntities();

        /*
          Reden gebruik sessions in webapi:

            Voordat de mail van erwin kwam had ik alles al gemaakt met sessions in de webapi. 
            daarnaast wil ik niet mijn database aanpassen omdat deze hetzelfde is als de eerste opdracht, nu kan ik allebei de applicaties gebruiken voor dezelfde applicatie.

            Ook heb ik veel research moeten doen naar een oplossing om sessions te gebruiken in webservices. ik kwam uiteindelijk op deze oplossing:

            http://www.wiliam.com.au/wiliam-blog/enabling-session-state-in-web-api
            
       */

        [HttpGet]
        [Route("AddToCart")]
        public IHttpActionResult AddToCart(int id, int qty)
        {
            //makes new shopcart item and sets values
            ShopCart item = new ShopCart();
            item.ID = id;
            item.Quantity = qty;
            
            //checks if cartlist is empty
            if (SessionManager.CartList == null)
            {
                //makes new list to go into the session
                List<ShopCart> shopCart = new List<ShopCart>();
                shopCart.Add(item);
                SessionManager.CartList = shopCart;
                //add value to count
                SessionManager.count = 1;
            }
            else
            {
                //finds product in list and adds it to quantity
                List<ShopCart> shopCart = SessionManager.CartList;
                var product = shopCart.Find(c => c.ID == id);
                if (product != null)
                {
                    product.Quantity += item.Quantity;
                }
                else
                {
                    shopCart.Add(item);
                    SessionManager.count = SessionManager.count + 1;
                }
            }
            return Ok();
        }


        [HttpGet]
        [Route("Delete")]
        public IHttpActionResult Delete(int id)
        {
            List<ShopCart> shopCart = SessionManager.CartList;
            var Item = shopCart.Find(c => c.ID == id);
            shopCart.Remove(Item);
            SessionManager.CartList = shopCart;
            SessionManager.count = SessionManager.count - 1;
            return Ok();

        }


        [HttpGet]
        [Route("UpdateQuantity")]
        public IHttpActionResult UpdateQuantity(int id, int qty)
        {
            List<ShopCart> shopCart = SessionManager.CartList;

            //finds item and removes it
            var Item = shopCart.Find(c => c.ID == id);
            shopCart.Remove(Item);

            //adds new item with updated quantity
            ShopCart newItem = new ShopCart();
            newItem.ID = id;
            newItem.Quantity = qty;
            shopCart.Add(newItem);
            SessionManager.CartList = shopCart;
            return Ok();
        }
    }
}
