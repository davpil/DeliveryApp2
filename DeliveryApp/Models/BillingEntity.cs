using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryApp.Models
{
    public class BillingEntity : Entity<int>
    {
        public int OrderEntityID { get; set; }
        public OrderEntity OrderEntity { get; set; }

        public DateTime? BillDate { get; set; }

        public decimal Price { get; set; }

        public decimal ProcessedAmount { get; set; }

        public string Comment { get; set; }
    }
}
