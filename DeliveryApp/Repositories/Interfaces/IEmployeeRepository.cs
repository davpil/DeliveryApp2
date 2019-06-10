using DeliveryApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryApp.Repositories.Interfaces
{
    public interface IEmployeeRepository: ICRUDRepository<EmployeeEntity, Guid> 
    {
        //Task<EmployeeEntity> GetEmployeeByIdAsync(Guid id);

        //Task<EmployeeEntity> GetEmployeeByPositionAsync(int positionId);

        // Task<EmployeeEntity> CreateEmployeeAsync(EmployeeEntity employeeEntity, Action<string, string> AddErrorMessage);

        //  Task<EmployeeEntity> UpdateEmployeeAsync(EmployeeEntity employeeEntity, Action<string, string> AddErrorMessage);

        //Task<EmployeeEntity> DeleteEmployeeByIdAsync(Guid id);

        // Task<IEnumerable<EmployeeEntity>> GetAllEmployeesAsync();
    }
}
