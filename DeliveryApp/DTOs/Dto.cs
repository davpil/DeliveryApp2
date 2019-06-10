using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryApp.DTOs
{
    public abstract class Dto<TID>
    {
        public TID ID { get; set; }
    }
}
