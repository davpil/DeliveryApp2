using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryApp.DTOs
{
    public class DynamicManu:Dto<int>
    {
        public string Title { get; set; }
        public string Api { get; set; }
        public string Icon { get; set; }
        public double Order { get; set; } = 10;
        public List<DynamicManu> SubItems { get; set; }
    }
}
