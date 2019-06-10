using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryApp.DTOs
{
    public class Login
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }

    /// <summary>
    /// DTO for registering a new user
    /// </summary>
    public class Register
    {
        [Required]
        public Guid PersonID { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        /// <summary>
        /// Names of roles
        /// </summary>
        public List<string> RoleNames { get; set; }
    }

    /// <summary>
    /// DTO for updating an existing user
    /// </summary>
    public class Update : Dto<string>
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        /// <summary>
        /// Role names
        /// </summary>
        public IList<string> Roles { get; set; }
    }

    /// <summary>
    /// DTO for updating an existing user
    /// </summary>
    public class UserShort : Dto<string>
    {
        [Display(Name = "Name")]
        public string Name { get; set; }
    }
}