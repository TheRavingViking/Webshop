using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Webshop.Models
{
    [MetadataType(typeof(ProductMetaData))]
    public partial class Product
    {
    }

    public class ProductMetaData
    {
        public int ID { get; set; }
        [Display(Name = "Product name")]
        public string Name { get; set; }
        public string Description { get; set; }
        public string Weight { get; set; }
        public string Price { get; set; }
        public Nullable<int> Quantity { get; set; }
        public Nullable<int> SupplierID { get; set; }
        public Nullable<int> CategoryID { get; set; }
        public byte[] Image { get; set; }
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        [Display(Name = "Deleted")]
        public Nullable<System.DateTime> IsDeleted { get; set; }
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        [Display(Name = "Created")]
        public Nullable<System.DateTime> CreatedAt { get; set; }

        [JsonIgnore]
        [Display(Name = "Category name")]
        public virtual Category Category { get; set; }
        [JsonIgnore]
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
        [JsonIgnore]
        public virtual Supplier Supplier { get; set; }
    }
}