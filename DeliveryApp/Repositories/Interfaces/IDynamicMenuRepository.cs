﻿using DeliveryApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryApp.Repositories.Interfaces
{
    public interface IDynamicMenuRepository:ICRUDRepository<DynamicMenuEntity, int>
    {
    }
}
