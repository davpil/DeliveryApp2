using DeliveryApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryApp.Repositories.Interfaces
{
    public interface ICRUDRepository<TEntity, TID> : IRepository<TEntity, TID>
           where TEntity : Entity<TID>
    {
        Task<TEntity> CreateAsync(TEntity entity);

        Task<IEnumerable<TEntity>> CreateRangeAsync(IEnumerable<TEntity> entities);

        TEntity Update(TEntity entity);

        Task<TEntity> DeleteAsync(TID id);

        /// <summary>
        /// In some cases service examins an entity which should be deleted.
        /// No neccesity Select it one more time for deletion
        /// </summary>
        /// <param name="entity">Entity to be deleted</param>
        /// <returns>Deleted entity</returns>
        TEntity Remove(TEntity entity);
    }
}
