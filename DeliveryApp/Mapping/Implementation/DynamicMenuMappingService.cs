using DeliveryApp.DTOs;
using DeliveryApp.Mapping.Interfaces;
using DeliveryApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryApp.Mapping.Implementation
{
    public class DynamicMenuMappingService
         :MappingService<DynamicManu, DynamicMenuEntity>
        , IDynamicMenuMappingService
    {
        public override DynamicMenuEntity DtoToEntity(DynamicManu dto)
        {
            if (dto == null)
            {
                return null;
            }

            DynamicMenuEntity entity = new DynamicMenuEntity
            {
                ID = dto.ID,
                Title = dto.Title,
                Api = dto.Api,
                Icon = dto.Icon,
                Order = dto.Order,
                SubItems = DtosToEntities(dto.SubItems).ToList()
            };
            return entity;
        }

        public override DynamicManu EntityToDto(DynamicMenuEntity entity)
        {
            if (entity == null)
            {
                return null;
            }

            DynamicManu dto = new DynamicManu
            {
                ID = entity.ID,
                Title = entity.Title,
                Api = entity.Api,
                Icon = entity.Icon,
                Order = entity.Order,
                SubItems = EntitiesToDtos(entity.SubItems).ToList()
            };
            return dto;
        }
    }
}
