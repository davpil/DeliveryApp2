using DeliveryApp.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryApp.Services.Interfaces
{
    public interface IDtoCRUDService<TDto, TID>
    where TDto : Dto<TID>
    {
        Task<TDto> GetAsync(TID id);

        Task<TDto> CreateAsync(TDto dto, Action<string, string> AddErrorMessage);

        Task<TDto> UpdateAsync(TDto dto, Action<string, string> AddErrorMessage);

        Task<IEnumerable<TDto>> GetAllAsync();

        Task<TDto> DeleteAsync(TID id, Action<string, string> AddErrorMessage);

        Task<bool> ValidateCrUpDataAsync(TDto dto, Action<string, string> AddErrorMessage = null);
    }
}
