using DeliveryApp.Constants;
using DeliveryApp.Data;
using DeliveryApp.Helpers;
using DeliveryApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryApp.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class AuthorizeActivityAttribute : AuthorizeAttribute, IAsyncAuthorizationFilter
    {
        private readonly string[] _activitiesList;
        private readonly bool _allowInTesting;

        public AuthorizeActivityAttribute(string[] activityValue, bool allowInTesting = false)
        {
            _activitiesList = activityValue;
            _allowInTesting = allowInTesting;
        }

        public AuthorizeActivityAttribute(string activityValue, bool allowInTesting = false)
        {
            _activitiesList = new string[] { activityValue };
            _allowInTesting = allowInTesting;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            if (context.HttpContext.User.IsInRole(RoleNames.SuperAdmin))
            {
                return;
            }

            if (_allowInTesting)
            {
                IHostingEnvironment environment = context.HttpContext.RequestServices
                                                .GetRequiredService<IHostingEnvironment>();
                if (environment.IsEnvironment("Testing"))
                {
                    return;
                }
            }

            ApplicationDbContext dbContext = context.HttpContext.RequestServices
                                                .GetRequiredService<ApplicationDbContext>();
            UserManager<UserEntity> userManager = context.HttpContext.RequestServices
                                                .GetRequiredService<UserManager<UserEntity>>();
            UserEntity currentUser = await userManager.GetUserAsync(context.HttpContext.User);

            if (currentUser == null)
            {
                context.Result = new ObjectResult(null)
                {
                    Value = null,
                    StatusCode = StatusCodes.Status401Unauthorized,
                };
            }
            else if (!SQLHelper.IsUserInActivity(dbContext, currentUser, _activitiesList))
            {
                context.Result = new ObjectResult(null)
                {
                    Value = null,
                    StatusCode = StatusCodes.Status403Forbidden
                };
            }
        }

    }
}