using DeliveryApp.Data;
using DeliveryApp.DTOs;
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
    public class PositionService : DtoCRUDService<Position, PositionEntity, int>, IPositionService
    {
        public PositionService(IPositionRepository repository
            , IPositionMappingService mappingService
            , ApplicationDbContext context)
            : base(repository, mappingService, context)
        {
        }

        public override Task<bool> ValidateCrUpDataAsync(Position dto, Action<string, string> AddErrorMessage = null)
        {
            throw new NotImplementedException();
        }
    }
}
