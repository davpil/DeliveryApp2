using DeliveryApp.Constants;
using DeliveryApp.Models;
using DeliveryApp.Synchro.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryApp.Data
{
    public class DbInitializer
    {
        static async Task CreateProgrammerUserAsync(ApplicationDbContext context, IServiceProvider serviceProvider)
        {
            RoleManager<RoleEntity> roleManager = serviceProvider.GetRequiredService<RoleManager<RoleEntity>>();

            if (!(await context.UserEntity.AnyAsync()))
            {
                var users = new[] {
                    new { Role = RoleNames.Programmer, UserName = "programmer@Delivery.app", Email = "programmer@Delivery.app", UserPWD = "DemoProg!(1",  },
                    new { Role = RoleNames.SuperAdmin, UserName = "superadmin@Delivery.app", Email = "superadmin@Delivery.app", UserPWD = "DemoSuperAdm!(1" }
                };
                foreach (var user in users)
                {
                    if (!await roleManager.RoleExistsAsync(user.Role))
                    {
                        RoleEntity predefinedRole = new RoleEntity { Name = user.Role, Predefined = true };
                        await roleManager.CreateAsync(predefinedRole);
                    }
                }

                UserManager<UserEntity> userManager = serviceProvider.GetRequiredService<UserManager<UserEntity>>();
                foreach (var u in users)
                {
                    UserEntity user = new UserEntity()
                    {
                        UserName = u.UserName,
                        Email = u.Email,
                        Predefined = true,
                    };
                    var chkUser = await userManager.CreateAsync(user, u.UserPWD);
                    if (chkUser.Succeeded)
                    {
                        var result1 = await userManager.AddToRoleAsync(user, u.Role);
                    }
                }
            }
        }
        public static void Initialize(IServiceProvider serviceProvider)
        {
            ApplicationDbContext context = serviceProvider.GetRequiredService<ApplicationDbContext>();
            context.Database.EnsureCreated();

            CreateProgrammerUserAsync(context, serviceProvider).GetAwaiter().GetResult();

            Type type = typeof(ISynchronizable);
            IEnumerable<Type> types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => type.IsAssignableFrom(p) && !p.IsInterface);
            foreach (Type t in types)
            {
                ISynchronizable temp = (ISynchronizable)ActivatorUtilities.CreateInstance(serviceProvider, t);
                temp.AddEntitiesAsync().GetAwaiter().GetResult();
            }
        }
    }
}
