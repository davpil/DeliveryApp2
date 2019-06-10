using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryApp.Models
{
    public class RoleActivityEntity : Entity<int>
    {
        /// <summary>
        /// Navigation property to ActivityEntity
        /// </summary>
        public string ActivityEntityID { get; set; }
        public ActivityEntity ActivityEntity { get; set; }

        /// <summary>
        /// Navigation property to RoleEntity
        /// </summary>
        public string RoleEntityId { get; set; }
        public RoleEntity RoleEntity { get; set; }
    }
}
