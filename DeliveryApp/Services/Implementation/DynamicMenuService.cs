using DeliveryApp.Constants;
using DeliveryApp.Data;
using DeliveryApp.DTOs;
using DeliveryApp.Mapping.Interfaces;
using DeliveryApp.Models;
using DeliveryApp.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryApp.Services.Implementation
{
    public class DynamicMenuService : IDynamicMenuService
    {
        protected readonly IDynamicMenuMappingService _dynamicMenuMappingService;
        protected readonly UserManager<UserEntity> _userManager;
        protected readonly IHttpContextAccessor _httpContextAccessor;
        protected readonly ApplicationDbContext _context;

        public DynamicMenuService(IDynamicMenuMappingService dynamicMenuMappingService
            , UserManager<UserEntity> userManager
            , IHttpContextAccessor httpContextAccessor
            , ApplicationDbContext context)
        {
            _dynamicMenuMappingService = dynamicMenuMappingService ?? throw new ArgumentNullException(nameof(dynamicMenuMappingService));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public Task<DynamicManu> CreateAsync(DynamicManu dto, Action<string, string> AddErrorMessage)
        {
            throw new NotImplementedException();
        }

        public Task<DynamicManu> DeleteAsync(int id, Action<string, string> AddErrorMessage)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<DynamicManu>> GetAllAsync()
        {
            IList<DynamicMenuEntity> menuEntities = await _context.DynamicMenuEntity
                                                                .Include(m => m.SubItems)
                                                                .Where(m => m.ParentID == null)
                                                                .ToListAsync();

            string userID;
            bool isSuperAdmin;
            UserEntity currentUser = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
            if (currentUser != null)
            {
                userID = currentUser.Id;
                isSuperAdmin = await _userManager.IsInRoleAsync(currentUser, RoleNames.SuperAdmin);
            }
            else
            {
                userID = null;
                isSuperAdmin = false;
            }

            List<int> allowedMenus = new List<int>();

            // Not Logged-in user. Allow only public items.

            if (string.IsNullOrEmpty(userID))
            {
                allowedMenus = await (from menus in _context.DynamicMenuEntity
                                      join menuActiv in _context.DynamicMenuActivityEntity on menus.ID equals menuActiv.DynamicMenuEntityID
                                      where menuActiv.ActivityEntityID == ActivityNames.Public
                                      select menus.ID).ToListAsync();
            }

            else if (!isSuperAdmin)
            {
                allowedMenus = await (from menus in _context.DynamicMenuEntity
                                      join menuActiv in _context.DynamicMenuActivityEntity on menus.ID equals menuActiv.DynamicMenuEntityID
                                      join roleActiv in _context.RoleActivityEntity on menuActiv.ActivityEntityID equals roleActiv.ActivityEntityID
                                      join userRole in _context.UserRoles on roleActiv.RoleEntityId equals userRole.RoleId
                                      where userRole.UserId == userID
                                      select menus.ID).ToListAsync();
                                   
            }

            if (!isSuperAdmin)
            {
                for (int n = menuEntities.Count - 1; n >= 0; n--)
                {
                    for (int m = menuEntities[n].SubItems.Count - 1; m >= 0; m--)
                    {
                        if (!allowedMenus.Contains(menuEntities[n].SubItems[m].ID))
                        {
                            menuEntities[n].SubItems.RemoveAt(m);
                        }
                    }

                    // Parent menu item doesn't have matching activity and SubItems array is empty

                    if (!allowedMenus.Contains(menuEntities[n].ID) && menuEntities[n].SubItems.Count == 0)
                    {
                        menuEntities.RemoveAt(n);
                    }
                }
            }

            return _dynamicMenuMappingService.EntitiesToDtos(menuEntities);
        }

        public Task<DynamicManu> GetAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<DynamicManu> UpdateAsync(DynamicManu dto, Action<string, string> AddErrorMessage)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ValidateCrUpDataAsync(DynamicManu dto, Action<string, string> AddErrorMessage = null)
        {
            throw new NotImplementedException();
        }
    }
}
