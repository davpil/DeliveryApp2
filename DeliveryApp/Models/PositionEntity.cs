using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryApp.Models
{
    public class PositionEntity:Entity<int>
    { 
        public string Title { get; set; }
        public string Description { get; set; }
        [DefaultValue(10)]
        public double Order { get; set; } = 10;
        //public virtual ICollection<RoleEntity> RoleEntity { get; set; } = new List<RoleEntity>();
    }
}
