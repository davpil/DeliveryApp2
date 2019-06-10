using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryApp.Models
{
    public class SynchroEntity : Entity<int>
    {
        /// <summary>
        /// Keeps the name of the Model for which hash is calculated
        /// </summary>
        public string ClassName { get; set; }

        /// <summary>
        /// The calculated hash of the model
        /// </summary>
        public string HashCode { get; set; }

    }
}
