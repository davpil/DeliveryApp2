using DeliveryApp.Mapping.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryApp.Mapping.Implementation
{
    public class MappingService<TDto, TEntity> : IMappingService<TDto, TEntity>
        where TDto : class
        where TEntity : class
    {
        public virtual IEnumerable<TEntity> DtosToEntities(IEnumerable<TDto> dtos)
        {
            if (dtos == null)
            {
                return null;
            }

            List<TEntity> entities = new List<TEntity>();
            foreach (TDto d in dtos)
            {
                entities.Add(DtoToEntity(d));
            }
            return entities;
        }

        public virtual TEntity DtoToEntity(TDto dto)
        {
            if (dto == null)
            {
                return null;
            }
            return dto as TEntity;
        }

        public virtual IEnumerable<TDto> EntitiesToDtos(IEnumerable<TEntity> entities)
        {
            if (entities == null)
            {
                return null;
            }

            List<TDto> dtos = new List<TDto>();
            foreach (TEntity e in entities)
            {
                dtos.Add(EntityToDto(e));
            }
            return dtos;
        }

        public virtual TDto EntityToDto(TEntity entity)
        {
            if (entity == null)
            {
                return null;
            }
            return entity as TDto;
        }
    }
}
