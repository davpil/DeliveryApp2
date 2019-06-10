using DeliveryApp.DTOs;
using DeliveryApp.Mapping.Interfaces;
using DeliveryApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryApp.Mapping.Implementation
{
    public class ActivityMappingService
        : MappingService<Activity, ActivityEntity>
        , IActivityMappingService
    {
        public override ActivityEntity DtoToEntity(Activity dto)
        {
            if (dto == null)
            {
                return null;
            }
            ActivityEntity entity = new ActivityEntity
            {
                ID = dto.ID,
            };

            if (dto.Children != null && dto.Children.Any())
            {
                entity.Children = DtosToEntities(dto.Children);
            }
            return entity;
        }

        public override Activity EntityToDto(ActivityEntity entity)
        {
            if (entity == null)
            {
                return null;
            }
            Activity dto = new Activity
            {
                ID = entity.ID,
            };

            if (entity.Children != null && entity.Children.Any())
            {
                dto.Children = EntitiesToDtos(entity.Children);
            }
            return dto;
        }
    }
}