using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Webshop.Models;

namespace Webshop.Controllers
{
    public class ProductsAPIController : ApiController
    {
        private ShopEntities db = new ShopEntities();


        [HttpGet]
        [Route("Sort")]
        public IQueryable Sort(string sortBy)
        {
            var products = db.Products.Include(p => p.Category).Include(p => p.Supplier);

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

            return products;
        }


        [HttpGet]
        [Route("Filter")]
        public IQueryable Filter(int filterBy)
        {
            var catID = db.Categories.Find(filterBy);

            var products = (from items in db.Products
                            where items.Category.ID == catID.ID
                            select items).Include(p => p.Category).Include(p => p.Supplier);

            return products;
        }


    }
}


