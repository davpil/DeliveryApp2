using DeliveryApp.DTOs;
using DeliveryApp.Mapping.Interfaces;
using DeliveryApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryApp.Mapping.Implementation
{
    public class EmployeeMappingService:MappingService<Employee, EmployeeEntity>, IEmployeeMappingService
    {
        public override EmployeeEntity DtoToEntity(Employee dto)
        {
            if (dto==null)
            {
                return null;
            }

            EmployeeEntity entity = new EmployeeEntity
            {
                ID = dto.ID,
                Email = dto.Email,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                PhoneNumber = dto.PhoneNumber
            };
            return entity;
        }

        public override Employee EntityToDto(EmployeeEntity entity)
        {
            Employee dto = new Employee
            {
                ID = entity.ID,
                Email = entity.Email,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                PhoneNumber = entity.PhoneNumber
            };
            return dto;
        }
    }
}
