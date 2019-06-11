using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryApp.Models
{
    public class OrderEntity : Entity<int>
    {
        public Guid EmployeeEntityID { get; set; }
        public EmployeeEntity EmployeeEntity { get; set; }

        public Guid BuyerEntityID { get; set; }
        public BuyerEntity BuyerEntity { get; set; }

        public int ProductID { get; set; }
        public ProductEntity ProductEntity { get; set; }

        public DateTime? OrderDay { get; set; }
        public DateTime? DeliveryDay { get; set; }

        public int ProductCount { get; set; }
        //[NotMapped]
        //public decimal TotalPrice
        //{
        //    get
        //    {
        //        return ProductEntity.SellingPrice * ProductCount;
        //    }
        //}
    }
}
