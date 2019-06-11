using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryApp.Models
{
    public class ProductEntity:Entity<int>
    {
        public string Title { get; set; }
        public string Producer { get; set; }
        public string Barcode { get; set; }
        public DateTime? ProduceDay { get; set; }
        public DateTime? UsebleUntil { get; set; }
        public decimal CostPrice { get; set; }
        public decimal SellingPrice { get; set; }
    }
}
