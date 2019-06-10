using DeliveryApp.Data;
using DeliveryApp.Models;
using DeliveryApp.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryApp.Repositories.Implementation
{
    public class DynamicMenuRepository :CRUDRepository<DynamicMenuEntity, int>, IDynamicMenuRepository
    {
        public DynamicMenuRepository(ApplicationDbContext context):base(context)
        {

        }
    }
}
