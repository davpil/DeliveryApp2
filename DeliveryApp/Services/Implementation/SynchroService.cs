using DeliveryApp.Data;
using DeliveryApp.Helpers;
using DeliveryApp.Models;
using DeliveryApp.Services.Interfaces;
using DeliveryApp.Synchro.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryApp.Services.Implementation
{
    public class SynchroService:ISynchroService
    {
        protected readonly ApplicationDbContext _context;

        public SynchroService(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task AddHashToDBAsync(List<IStringifyable> listOfItems, string name)
        {
            SynchroEntity synchro = new SynchroEntity
            {
                ClassName = name,
                HashCode = MD5Helper.GenerateMD5Sum(listOfItems)
            };

            await _context.SynchroEntity.AddAsync(synchro);
            await _context.SaveChangesAsync();
        }
    }
}
