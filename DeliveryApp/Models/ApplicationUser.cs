using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryApp.Models
{
    // Add profile data for application users by adding properties to the UserEntity class
    public class UserEntity : IdentityUser
    {
        /// <summary>
        /// Navigation property to Person linked to User
        /// </summary>
        public Guid? PersonEntityID { get; set; }
        public PersonEntity PersonEntity { get; set; }

        /// <summary>
        /// Indicates that user is predefined in the software
        /// and its CRUD doesn't available for any user.
        /// </summary>
        public bool Predefined { get; set; } = false;
    }
}
