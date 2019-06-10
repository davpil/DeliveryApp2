using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryApp.Models
{
    public class DynamicMenuActivityEntity : Entity<int>
    {
        /// <summary>
        /// Menu item
        /// </summary>
        public int DynamicMenuEntityID { get; set; }
        public DynamicMenuEntity MenuEntity { get; set; }

        /// <summary>
        /// Activity associated with MenuItem
        /// </summary>
        public string ActivityEntityID { get; set; }
        public ActivityEntity ActivityEntity { get; set; }
    }
}
