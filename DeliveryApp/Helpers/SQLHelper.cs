using DeliveryApp.Data;
using DeliveryApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryApp.Helpers
{
    public static class SQLHelper
    {
        public static bool IsUserInActivity(ApplicationDbContext context, UserEntity user, string[] activity)
        {
            return (from usRoles in context.UserRoles
                    join actRoles in context.RoleActivityEntity on usRoles.RoleId equals actRoles.RoleEntityId
                    where usRoles.UserId == user.Id
                            && activity.Contains(actRoles.ActivityEntityID)
                    select actRoles.ActivityEntityID).Any();
        }

        public static bool IsUserInActivity(ApplicationDbContext context, UserEntity user, string activity)
        {
            return (from usRoles in context.UserRoles
                    join actRoles in context.RoleActivityEntity on usRoles.RoleId equals actRoles.RoleEntityId
                    where usRoles.UserId == user.Id
                            && activity == actRoles.ActivityEntityID
                    select actRoles.ActivityEntityID).Any();
        }
    }
}
