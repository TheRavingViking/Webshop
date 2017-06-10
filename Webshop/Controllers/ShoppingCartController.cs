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


        [HttpPost]
        public ActionResult AddToCart(int id, string quantity)
        {
            ShopCart item = new ShopCart();

            item.ID = id;
            item.Quantity = Int32.Parse(quantity);

            if (SessionManager.CartList == null)
            {
                List<ShopCart> shopCart = new List<ShopCart>();
                shopCart.Add(item);
                SessionManager.CartList = shopCart;
                ViewBag.cart = shopCart.Count();
                Session["count"] = 1;
            }
            else
            {
                List<ShopCart> shopCart = SessionManager.CartList;

                var product = shopCart.Find(c => c.ID == id);

                if (product != null)
                {
                    product.Quantity += item.Quantity;
                }
                else
                {
                    shopCart.Add(item);
                    ViewBag.cart = shopCart.Count();
                    Session["count"] = Convert.ToInt32(Session["count"]) + 1;
                }


                SessionManager.CartList = shopCart;
            }
            return RedirectToAction("Index", "Home");
        }
        
    }
}
