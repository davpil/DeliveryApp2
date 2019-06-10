using DeliveryApp.Data;
using DeliveryApp.Models;
using DeliveryApp.Repositories.Interfaces;
using DeliveryApp.Services.Interfaces;
using DeliveryApp.Synchro.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryApp.Synchro.Implementation
{
    public abstract class BaseSyncService<TEntity, TID> where TEntity:Entity<TID>, IStringifyable
    {
        protected readonly ICRUDRepository<TEntity, TID> _repository;
        protected readonly ISynchroService _synchro;
        protected readonly ApplicationDbContext _context;

        public BaseSyncService(ICRUDRepository<TEntity, TID> repository
            , ISynchroService synchro
            , ApplicationDbContext context)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _synchro = synchro ?? throw new ArgumentNullException(nameof(synchro));
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public abstract IEnumerable<TEntity> GetEntities();

        public virtual async Task AddEntitiesAsync()
        {
            if(!await _context.Set<TEntity>().AnyAsync())
            {
                using (var dbContextTransaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        IEnumerable<TEntity> actList = GetEntities();
                        await _context.Set<TEntity>().AddRangeAsync(actList);
                        await _context.SaveChangesAsync();

                        // Create also the hash and save into db for Activity.
                        await _synchro.AddHashToDBAsync(new List<IStringifyable>(actList), typeof(ActivityEntity).Name);

                        dbContextTransaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        // Example: Conection lost
                        dbContextTransaction.Rollback();
                        throw ex;
                    }
                }
            }
        }
    }
}
