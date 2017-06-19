using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Webshop.Models
{
    [MetadataType(typeof(OrderMetaData))]
    public partial class Order
    {
    }

    public class OrderMetaData
    {
        public int ID { get; set; }
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public Nullable<System.DateTime> OrderDate { get; set; }
        [Display(Name = "Shipping adress")]
        public string ShipAdress { get; set; }
        public int CustomerID { get; set; }
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public Nullable<System.DateTime> IsDeleted { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
