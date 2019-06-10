using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryApp.Exceptions
{
    public class EntityNotFoundException<TEntity, TID>:Exception
    {
        public EntityNotFoundException(TID id)
            : base($"The entity of type {nameof(TEntity)} with id={id} was not found.")
        {
        }
    }
}
