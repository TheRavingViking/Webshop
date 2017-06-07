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
                List<OrderDetail> ShoppingCartItems = SessionManager.CartList;

                List<Product> shopCart = new List<Product>();

                foreach (OrderDetail Item in ShoppingCartItems)
                {
                    var item = db.Products.Find(Item.ProductID);
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
            OrderDetail item = new OrderDetail();

            item.ProductID = id;
            item.Quantity = Int32.Parse(quantity);

            if (SessionManager.CartList == null)
            {
                List<OrderDetail> shopCart = new List<OrderDetail>();

                shopCart.Add(item);

                SessionManager.CartList = shopCart;
                ViewBag.cart = shopCart.Count();

                Session["count"] = 1;
            }
            else
            {
                List<OrderDetail> shopCart = SessionManager.CartList;
                shopCart.Add(item);
                SessionManager.CartList = shopCart;
                ViewBag.cart = shopCart.Count();
                Session["count"] = Convert.ToInt32(Session["count"]) + 1;

            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult RemoveFromCart(int id)
        {
            List<OrderDetail> ShoppingCartItems = SessionManager.CartList;

            var item = (from items in ShoppingCartItems
                       where items.ProductID == id
                       select items).FirstOrDefault();

            ShoppingCartItems.Remove(item);

            Session["count"] = Convert.ToInt32(Session["count"]) - 1;

            return RedirectToAction("Index", "ShoppingCart");
        }

        [HttpPost]
        public ActionResult EditQuantity(int id, string quantity)
        {
            List<OrderDetail> ShoppingCartItems = SessionManager.CartList;

            var item = (from items in ShoppingCartItems
                where items.ProductID == id
                select items).FirstOrDefault();

            item.Quantity = Int32.Parse(quantity);
            
            return RedirectToAction("Index", "ShoppingCart");
        }


    }
}
