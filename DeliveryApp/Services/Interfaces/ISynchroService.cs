using DeliveryApp.Synchro.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryApp.Services.Interfaces
{
    public interface ISynchroService
    {
        Task AddHashToDBAsync(List<IStringifyable> listOfItems, string name);
    }
}
