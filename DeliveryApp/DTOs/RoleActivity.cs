using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryApp.DTOs
{
    public class RoleActivity:Dto<int>
    {
        public string ActivityID { get; set; }
        public Activity Activity { get; set; }

        /// <summary>
        /// Navigation property to RoleEntity
        /// </summary>
        public string RoleId { get; set; }
        public Role Role { get; set; }
    }
}
