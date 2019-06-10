using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryApp.Models
{
    public abstract class Entity<TID>
    {
        public virtual TID ID { get; set; }
    }
}
