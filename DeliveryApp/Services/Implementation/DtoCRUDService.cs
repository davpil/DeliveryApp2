using DeliveryApp.Data;
using DeliveryApp.DTOs;
using DeliveryApp.Exceptions;
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
    public abstract class DtoCRUDService<TDto, TEntity, TID> : IDtoCRUDService<TDto, TID>
        where TDto : Dto<TID>
        where TEntity : Entity<TID>
    {
        protected readonly ICRUDRepository<TEntity, TID> _repository;
        protected readonly IMappingService<TDto, TEntity> _mappingService;
        protected readonly ApplicationDbContext _context;

        public DtoCRUDService(ICRUDRepository<TEntity, TID> repository
            , IMappingService<TDto, TEntity> mappingService
            , ApplicationDbContext context)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mappingService = mappingService ?? throw new ArgumentNullException(nameof(mappingService));
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public virtual async Task<TDto> CreateAsync(TDto dto, Action<string, string> AddErrorMessage)
        {
            if (!await ValidateCrUpDataAsync(dto, AddErrorMessage))
            {
                return null;
            }

            TEntity entity = _mappingService.DtoToEntity(dto);
            TEntity createdEntity = await _repository.CreateAsync(entity);
            if (createdEntity!=null)
            {
                await _context.SaveChangesAsync();
            }

            createdEntity = await _repository.GetCompleteAsync(createdEntity.ID);
            return _mappingService.EntityToDto(createdEntity);
        }

        public virtual async Task<TDto> DeleteAsync(TID id, Action<string, string> AddErrorMessage)
        {
            if (!await _repository.AnyAsync(e=>e.Equals(id)))
            {
             throw new EntityNotFoundException<TEntity, TID>(id);
            }

            TEntity deletedEntity = await _repository.DeleteAsync(id);
            if (deletedEntity!=null)
            {
                await _context.SaveChangesAsync();
            }

            return _mappingService.EntityToDto(deletedEntity);
        }

        public virtual async Task<IEnumerable<TDto>> GetAllAsync()
        {
            return _mappingService.EntitiesToDtos(await _repository.GetAllCompleteAsync());
        }

        public virtual async Task<TDto> GetAsync(TID id)
        {
            return _mappingService.EntityToDto(await _repository.GetCompleteAsync(id));
        }

        public virtual async Task<TDto> UpdateAsync(TDto dto, Action<string, string> AddErrorMessage)
        {
            if (!await _repository.AnyAsync(e=>e.ID.Equals(dto.ID)))
            {
                throw new EntityNotFoundException<TEntity, TID>(dto.ID);
            }

            if (!await ValidateCrUpDataAsync(dto, AddErrorMessage))
            {
                return null;
            }

            TEntity entity = _mappingService.DtoToEntity(dto);
            TEntity updatedEntity = _repository.Update(entity);

            if (updatedEntity != null)
            {
                await _context.SaveChangesAsync();
            }

            updatedEntity = await _repository.GetCompleteAsync(updatedEntity.ID);
            return _mappingService.EntityToDto(updatedEntity);
        }

        public abstract Task<bool> ValidateCrUpDataAsync(TDto dto, Action<string, string> AddErrorMessage = null);
    }
}
