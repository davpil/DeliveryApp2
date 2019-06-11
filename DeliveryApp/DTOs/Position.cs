using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryApp.DTOs
{
    public class Position : Dto<int>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        [DefaultValue(10)]
        public double Order { get; set; } = 10;
        public virtual ICollection<Role> Role { get; set; } = new List<Role>();
    }
}
