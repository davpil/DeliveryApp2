using DeliveryApp.Data;
using DeliveryApp.Models;
using DeliveryApp.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryApp.Repositories.Implementation
{
    public class ActivityRepository : CRUDRepository<ActivityEntity, string>, IActivityRepository
    {
        public ActivityRepository(ApplicationDbContext context) : base(context)
        {
            _completeQuery = _context.ActivityEntity
                                .Include(a => a.Children)
                                .Where(a => a.ParentID == null)
                                .OrderBy(a => a.Order);
        }
    }
}
