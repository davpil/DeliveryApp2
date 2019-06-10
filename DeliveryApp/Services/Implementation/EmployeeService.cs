using DeliveryApp.Data;
using DeliveryApp.DTOs;
using DeliveryApp.Mapping.Interfaces;
using DeliveryApp.Models;
using DeliveryApp.Repositories.Interfaces;
using DeliveryApp.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryApp.Services.Implementation
{
    public class EmployeeService:DtoCRUDService<Employee, EmployeeEntity, Guid>, IEmployeeService
    {
        public EmployeeService(IEmployeeRepository repository
            , IEmployeeMappingService mappingService
            , ApplicationDbContext context)
            :base(repository, mappingService, context)
        {      
        }

        public override async Task<bool> ValidateCrUpDataAsync(Employee dto, Action<string, string> AddErrorMessage = null)
        {
            return true;
        }
    }
}
