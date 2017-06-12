using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

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
        public Nullable<System.DateTime> IsDeleted { get; set; }
        public Nullable<System.DateTime> CreatedAt { get; set; }

        [Display(Name = "Category name")]
        public virtual Category Category { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }

        public virtual Supplier Supplier { get; set; }
    }
}