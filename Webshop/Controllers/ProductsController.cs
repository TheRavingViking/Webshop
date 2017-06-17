using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
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

            var category = db.Categories.Distinct();
            ViewBag.Categories = category;

            return View(products.ToList());
        }

        public ActionResult Sort(string sortBy)
        {
            //default linq query
            var products = db.Products.Include(p => p.Category).Include(p => p.Supplier);

            //get the corresponding query by input
            switch (sortBy)
            {
                case "Name":
                    products = (from items in db.Products
                                orderby items.Name 
                                select items).Include(p => p.Category).Include(p => p.Supplier);
                    break;

                case "Price":
                    products = (from items in db.Products
                                orderby items.Price
                                select items).Include(p => p.Category).Include(p => p.Supplier);
                    break;
                case "Quantity":
                    products = (from items in db.Products
                                orderby items.Quantity
                                select items).Include(p => p.Category).Include(p => p.Supplier);
                    break;

            }

            //makes a viewbag for catagories dropdown
            var category = db.Categories.Distinct();
            ViewBag.Categories = category;

            return View("Index", products.ToList());

        }

        public ActionResult Filter(int filterBy)
        {

            //finds category
            var catID = db.Categories.Find(filterBy);

            //linq query based on catid
            var products = (from items in db.Products
                            where items.Category.ID == catID.ID
                            select items).Include(p => p.Category).Include(p => p.Supplier);

            //makes a viewbag for catagories dropdown
            var category = db.Categories.Distinct();
            ViewBag.Categories = category;

            return View("Index", products.ToList());


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
            ViewBag.ID = id;
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
