using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
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
            if (Session["cart"] != null)
            {
                List<OrderDetail> ShoppingCartItems = (List<OrderDetail>) Session["cart"];

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

            if (Session["cart"] == null)
            {
                List<OrderDetail> shopCart = new List<OrderDetail>();

                shopCart.Add(item);

                Session["cart"] = shopCart;
                ViewBag.cart = shopCart.Count();

                Session["count"] = 1;
            }
            else
            {
                List<OrderDetail> shopCart = (List<OrderDetail>)Session["cart"];
                shopCart.Add(item);
                Session["cart"] = shopCart;
                ViewBag.cart = shopCart.Count();
                Session["count"] = Convert.ToInt32(Session["count"]) + 1;

            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult RemoveFromCart(int id)
        {
            List<OrderDetail> ShoppingCartItems = (List<OrderDetail>)Session["cart"];

            var item = (from items in ShoppingCartItems
                       where items.ProductID == id
                       select items).FirstOrDefault();

            ShoppingCartItems.Remove(item);

            return RedirectToAction("Index", "ShoppingCart");
        }


    }
}
