using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryApp.Mapping.Interfaces
{
    public interface IMappingService<TDto, TEntity>
        where TDto : class
        where TEntity : class
    {
        TEntity DtoToEntity(TDto dto);
        TDto EntityToDto(TEntity entity);

        IEnumerable<TEntity> DtosToEntities(IEnumerable<TDto> dtos);
        IEnumerable<TDto> EntitiesToDtos(IEnumerable<TEntity> entity);
    }
}
