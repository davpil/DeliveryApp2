using DeliveryApp.Data;
using DeliveryApp.Models;
using DeliveryApp.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryApp.Repositories.Implementation
{
    public abstract class CRUDRepository<TEntity, TID> : Repository<TEntity, TID>, ICRUDRepository<TEntity, TID>
            where TEntity : Entity<TID>
    {
        public CRUDRepository(ApplicationDbContext context) : base(context)
        {
        }

        public virtual async Task<TEntity> CreateAsync(TEntity entity)
        {
            await _context.Set<TEntity>().AddAsync(entity);
            return entity;
        }

        public virtual async Task<IEnumerable<TEntity>> CreateRangeAsync(IEnumerable<TEntity> entities)
        {
            await _context.Set<TEntity>().AddRangeAsync(entities);
            return entities;
        }

        public virtual async Task<TEntity> DeleteAsync(TID id)
        {
            TEntity entity = await _context.Set<TEntity>().SingleOrDefaultAsync(b => b.ID.Equals(id));
            if (entity != null)
            {
                _context.Set<TEntity>().Remove(entity);
            }
            return entity;
        }

        public virtual TEntity Update(TEntity entity)
        {
            _context.Set<TEntity>().Update(entity);
            return entity;
        }

        public TEntity Remove(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
            return entity;
        }
    }
}
