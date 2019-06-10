using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryApp.Models
{
    public class RoleEntity : IdentityRole
    {
        public RoleEntity()
        {

        }

        public RoleEntity(string roleName) : base(roleName)
        {

        }

        /// <summary>
        /// The description of the role
        /// </summary>
        [Display(Name = "Description")]
        public string Description { get; set; }

        /// <summary>
        /// Indicates that Role is predefined in the software
        /// and its CRUD doesn't available for any user.
        /// </summary>
        public bool Predefined { get; set; } = false;

        /// <summary>
        /// Navigation property for the Activities this role possesses.
        /// </summary>
        public virtual ICollection<RoleActivityEntity> RoleActivityEntity { get; set; } = new List<RoleActivityEntity>();
    }
}
