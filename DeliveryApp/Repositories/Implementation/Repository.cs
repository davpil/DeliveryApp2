using DeliveryApp.Data;
using DeliveryApp.Models;
using DeliveryApp.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DeliveryApp.Repositories.Implementation
{
    public abstract class Repository<TEntity, TID> : IRepository<TEntity, TID>
        where TEntity : Entity<TID>
    {
        protected readonly ApplicationDbContext _context;
        protected IQueryable<TEntity> _completeQuery = null;

        public Repository(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public bool Any(Expression<Func<TEntity, bool>> predicate)
        {
            return _context.Set<TEntity>().Any(predicate);
        }

        public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _context.Set<TEntity>().AnyAsync(predicate);
        }

        public virtual IEnumerable<TEntity> GetAll()
        {
            return _context.Set<TEntity>().ToList<TEntity>();
        }

        public async virtual Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _context.Set<TEntity>().ToListAsync<TEntity>();
        }

        public TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return _context.Set<TEntity>().SingleOrDefault(predicate);
        }

        public async Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _context.Set<TEntity>().SingleOrDefaultAsync(predicate);
        }

        public virtual async Task<TEntity> GetCompleteAsync(TID id)
        {
            if (_completeQuery != null)
            {
                return await _completeQuery.SingleOrDefaultAsync(e => e.ID.Equals(id));
            }
            else
            {
                return await SingleOrDefaultAsync(e => e.ID.Equals(id));
            }
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllCompleteAsync()
        {
            if (_completeQuery != null)
            {
                return await _completeQuery.ToListAsync();
            }
            else
            {
                return await GetAllAsync();
            }
        }
    }
}
