using DeliveryApp.Data;
using DeliveryApp.DTOs;
using DeliveryApp.Exceptions;
using DeliveryApp.Mapping.Interfaces;
using DeliveryApp.Models;
using DeliveryApp.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryApp.Services.Implementation
{
    public class RoleService : IRoleService
    {
        protected readonly IRoleMappingService _mappingService;
        protected readonly UserManager<UserEntity> _userManager;
        protected readonly RoleManager<RoleEntity> _roleManager;
        protected readonly ApplicationDbContext _context;

        public RoleService(IRoleMappingService mappingService
                    , UserManager<UserEntity> userManager
                    , RoleManager<RoleEntity> roleManager
                    , ApplicationDbContext context
                    )
        {
            _mappingService = mappingService ?? throw new ArgumentNullException(nameof(mappingService));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _roleManager = roleManager ?? throw new ArgumentNullException(nameof(roleManager));
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<Role> CreateAsync(Role dto, Action<string, string> AddErrorMessage)
        {
            if (!await ValidateCrUpDataAsync(dto, AddErrorMessage))
            {
                return null;
            }

            RoleEntity entity = _mappingService.DtoToEntity(dto);
            try
            {
                await _roleManager.CreateAsync(entity);
            }
            catch (Exception)
            {
                entity = null;
                AddErrorMessage?.Invoke("General", "Role with the same name already exists!");
            }
            return _mappingService.EntityToDto(entity);
        }

        public async Task<Role> DeleteAsync(string id, Action<string, string> AddErrorMessage)
        {
            RoleEntity deletedEntity = await _roleManager.FindByIdAsync(id);
            if (deletedEntity == null)
            {
                throw new EntityNotFoundException<Role, string>(id);
            }

            if (deletedEntity.Predefined)
            {
                AddErrorMessage?.Invoke("General", "A predefined Role can't be deleted!");
                return null;
            }

            IList<UserEntity> users = await _userManager.GetUsersInRoleAsync(deletedEntity.Name);
            if (users != null && users.Count > 0)
            {
                AddErrorMessage?.Invoke("General", "The Role is in use and can't be deleted!");
                return null;
            }

            _context.RoleActivityEntity.RemoveRange(
                        _context.RoleActivityEntity.Where(ra => ra.RoleEntityId == id));
            if (await _roleManager.DeleteAsync(deletedEntity) != IdentityResult.Success)
            {
                deletedEntity = null;
                AddErrorMessage?.Invoke("General", "An unexpected error!");
            }

            return _mappingService.EntityToDto(deletedEntity);
        }

        public async Task<IEnumerable<Role>> GetAllAsync()
        {
            return _mappingService.EntitiesToDtos(await _roleManager.Roles
                                                            .Include(r => r.RoleActivityEntity)
                                                            .Where(r => !r.Predefined)
                                                            .ToListAsync()
                                                 );
        }

        public async Task<Role> GetAsync(string id)
        {
            return _mappingService.EntityToDto(await _roleManager.Roles
                                                            .Include(r => r.RoleActivityEntity)
                                                            .Where(r => !r.Predefined && r.Id == id)
                                                            .SingleOrDefaultAsync()
                                                 );
        }

        public async Task<Role> UpdateAsync(Role dto, Action<string, string> AddErrorMessage)
        {
            if (!await ValidateCrUpDataAsync(dto, AddErrorMessage))
            {
                return null;
            }

            _context.RoleActivityEntity.RemoveRange(
                       _context.RoleActivityEntity.Where(ra => ra.RoleEntityId == dto.ID));

            RoleEntity dbRole = await _roleManager.FindByIdAsync(dto.ID);
            if (dbRole == null)
            {
                throw new EntityNotFoundException<RoleEntity, string>(dto.ID);
            }

            await _roleManager.SetRoleNameAsync(dbRole, dto.Name);
            dbRole.Description = dto.Description;
            dbRole.RoleActivityEntity = dto.Activities
                                            .Select(a => new RoleActivityEntity { RoleEntityId = dto.ID, ActivityEntityID = a })
                                            .ToList();
            if (await _roleManager.UpdateAsync(dbRole) != IdentityResult.Success)
            {
                dbRole = null;
                AddErrorMessage?.Invoke("General", "Role with the same name already exists!");
            }

            return _mappingService.EntityToDto(dbRole);
        }

        public async Task<bool> ValidateCrUpDataAsync(Role dto, Action<string, string> AddErrorMessage = null)
        {
            if (string.IsNullOrEmpty(dto.Name) || !dto.Activities.Any())
            {
                AddErrorMessage?.Invoke("General", "Role name can't be empty and it should has at least one Activity!");
                return false;
            }

            RoleEntity entity = await _roleManager.FindByNameAsync(dto.Name);
            if (entity != null && entity.Id != dto.ID)
            {
                AddErrorMessage?.Invoke("Name", "The Name must be unique!");
                return false;
            }

            return true;
        }
    }
}
