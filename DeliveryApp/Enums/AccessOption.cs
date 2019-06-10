using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryApp.Enums
{
    [Flags]
    public enum AccessOption
    {
        None = 0,
        Index = 1,
        Create = 2,
        Update = 4,
        Delete = 8,
        Details = 16,

        FullAccess = Index | Create | Update | Delete | Details,
    }
}
