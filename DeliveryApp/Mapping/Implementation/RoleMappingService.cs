using DeliveryApp.DTOs;
using DeliveryApp.Mapping.Interfaces;
using DeliveryApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryApp.Mapping.Implementation
{
    public class RoleMappingService : MappingService<Role, RoleEntity>
        , IRoleMappingService
    {
        public override RoleEntity DtoToEntity(Role dto)
        {
            if (dto == null)
            {
                return null;
            }
            RoleEntity entity = new RoleEntity
            {
                Id = dto.ID,
                Name = dto.Name,
                Description = dto.Description,
                Predefined = false,
                RoleActivityEntity = dto.Activities
                                        .Select(a => new RoleActivityEntity { ActivityEntityID = a, RoleEntityId = dto.ID })
                                        .ToList(),
            };
            return entity;
        }

        public override Role EntityToDto(RoleEntity entity)
        {
            if (entity == null)
            {
                return null;
            }
            Role dto = new Role
            {
                ID = entity.Id,
                Name = entity.Name,
                Description = entity.Description,
                Activities = entity.RoleActivityEntity
                                    .Select(ra => ra.ActivityEntityID)
                                    .ToList(),
            };
            return dto;
        }
    }
}
