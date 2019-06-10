using DeliveryApp.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryApp.Services.Interfaces
{
    public interface IEmployeeService:IDtoCRUDService<Employee, Guid>
    {

    }
}
