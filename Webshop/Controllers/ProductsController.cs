using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Webshop.Classes;
using Webshop.Models;

namespace Webshop.Controllers
{
    public class ProductsController : Controller
    {
        private ShopEntities db = new ShopEntities();

        // GET: Products
        public ActionResult Index()
        {
            var products = db.Products.Include(p => p.Category).Include(p => p.Supplier);
            return View(products.ToList());
        }


        [HttpPost]
        public ActionResult AddToCart(int id, string quantity)
        {
            ShopCartItem item = new ShopCartItem();

            item.ID = id;
            item.Quantity = Int32.Parse(quantity);

            if (Session["cart"] == null)
            {
                List<ShopCartItem> shopCart = new List<ShopCartItem>();

                shopCart.Add(item);
                
                Session["cart"] = shopCart;
                ViewBag.cart = shopCart.Count();
                
                Session["count"] = 1;


            }
            else
            {
                List<ShopCartItem> shopCart = (List<ShopCartItem>)Session["cart"];
                shopCart.Add(item);
                Session["cart"] = shopCart;
                ViewBag.cart = shopCart.Count();
                Session["count"] = Convert.ToInt32(Session["count"]) + 1;

            }
            return RedirectToAction("Index", "Home");
        }


        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            ViewBag.CategoryID = new SelectList(db.Categories, "ID", "Name");
            ViewBag.SupplierID = new SelectList(db.Suppliers, "ID", "Name");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Description,Weight,Price,Quantity,SupplierID,CategoryID,Image,IsDeleted,CreatedAt")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryID = new SelectList(db.Categories, "ID", "Name", product.CategoryID);
            ViewBag.SupplierID = new SelectList(db.Suppliers, "ID", "Name", product.SupplierID);
            return View(product);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryID = new SelectList(db.Categories, "ID", "Name", product.CategoryID);
            ViewBag.SupplierID = new SelectList(db.Suppliers, "ID", "Name", product.SupplierID);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Description,Weight,Price,Quantity,SupplierID,CategoryID,Image,IsDeleted,CreatedAt")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryID = new SelectList(db.Categories, "ID", "Name", product.CategoryID);
            ViewBag.SupplierID = new SelectList(db.Suppliers, "ID", "Name", product.SupplierID);
            return View(product);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
