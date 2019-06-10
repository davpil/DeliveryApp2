using DeliveryApp.Constants;
using DeliveryApp.Data;
using DeliveryApp.Enums;
using DeliveryApp.Helpers;
using DeliveryApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public abstract class SecureController : ControllerBase
    {
        protected readonly Dictionary<AccessOption, string> _accessOptions;

        public SecureController(Dictionary<AccessOption, string> accessOptions) => _accessOptions = accessOptions;

        /// <summary>
        /// Access Options of the current User for the given bundle of APIs.
        /// </summary>
        /// <returns>Bitmask of the access options</returns>
        [HttpGet]
        [Route("GetAccessOptions")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<AccessOption> GetAccessOptions()
        {
            if (HttpContext.User.IsInRole(RoleNames.SuperAdmin))
            {
                return AccessOption.FullAccess;
            }

            ApplicationDbContext dbContext = HttpContext.RequestServices
                                                .GetRequiredService<ApplicationDbContext>();
            UserManager<UserEntity> userManager = HttpContext.RequestServices
                                                .GetRequiredService<UserManager<UserEntity>>();
            UserEntity currentUser = await userManager.GetUserAsync(HttpContext.User);

            if (currentUser == null)
            {
                return AccessOption.None;
            }

            AccessOption result = AccessOption.None;
            foreach (KeyValuePair<AccessOption, string> kvp in _accessOptions)
            {
                if (SQLHelper.IsUserInActivity(dbContext, currentUser, kvp.Value))
                {
                    result |= kvp.Key;
                }
            }
            return result;
        }
    }
}
