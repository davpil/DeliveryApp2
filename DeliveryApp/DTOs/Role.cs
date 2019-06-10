using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryApp.DTOs
{
    public class Role:Dto<string>
    {
        public string Name { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        public IEnumerable<string> Activities { get; set; }
        /// <summary>
        /// Indicates that Role is predefined in the software
        /// and its CRUD doesn't available for any user.
        /// </summary>
       // public bool Predefined { get; set; } = false;

        /// <summary>
        /// Navigation property for the Activities this role possesses.
        /// </summary>
       // public virtual ICollection<RoleActivity> RoleActivity { get; set; } = new List<RoleActivity>();
    }
}
