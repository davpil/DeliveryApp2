using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryApp.Models
{
    public class BuyerEntity : Entity<Guid>
    {
        public string Name { get; set; }
        public string District { get; set; }
        public string Address { get; set; }

        /// <summary>
        /// Employee who work with Buyer and take Oredrs
        /// </summary>
        public Guid EmployeeEntityID { get; set; }
        public EmployeeEntity EmployeeEntity { get; set; }


    }
}
