using DeliveryApp.DTOs;
using DeliveryApp.Mapping.Interfaces;
using DeliveryApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryApp.Mapping.Implementation
{
    public class PositionMappingService : MappingService<Position, PositionEntity>, IPositionMappingService
    {
        //private readonly IRoleMappingService _roleMappingService;
        //public PositionMappingService(IRoleMappingService roleMappingService)
        //{
        //    _roleMappingService = roleMappingService ?? throw new ArgumentNullException(nameof(roleMappingService));
        //}
        public override PositionEntity DtoToEntity(Position dto)
        {
            if (dto == null)
            {
                return null;
            }

            PositionEntity entity = new PositionEntity
            {
                ID = dto.ID,
                Description=dto.Description,
                Order=dto.Order,
                Title=dto.Title,
            };

            //List<RoleEntity> roleEntities = new List<RoleEntity>();
            //foreach (Role r in dto.Role)
            //{
            //    roleEntities.Add(_roleMappingService.DtoToEntity(r));
            //}

            //entity.RoleEntity = roleEntities;
            
            return entity;
        }

        public override Position EntityToDto(PositionEntity entity)
        {
            Position dto = new Position
            {
                ID = entity.ID,
                Description=entity.Description,
                Order=entity.Order,
                Title=entity.Title
            };

            //List<Role> roleDtos = new List<Role>();
            //foreach (RoleEntity r in entity.RoleEntity)
            //{
            //    roleDtos.Add(_roleMappingService.EntityToDto(r));
            //}

            //dto.Role = roleDtos;

            return dto;
        }
    }
}
