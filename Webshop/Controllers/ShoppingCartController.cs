using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Webshop.Classes;
using Webshop.Models;

namespace Webshop.Controllers
{
    public class ShoppingCartController : Controller
    {
        private ShopEntities db = new ShopEntities();

        // GET: ShoppingCart
        [Route("shoppingcart")]
        public ActionResult Index()
        {
            
            if (SessionManager.CartList != null)
            {
                List<ShopCart> ShoppingCartItems = SessionManager.CartList;

                List<Product> shopCart = new List<Product>();

                foreach (ShopCart Item in ShoppingCartItems)
                {
                    var item = db.Products.Find(Item.ID);
                    item.Quantity = Item.Quantity;
                    shopCart.Add(item);
                }
                return View(shopCart);
            }
            else
                ViewBag.NoItemsInCart = "No items in cart, add them first before ordering";
                return View();
        }
       
    }
}
