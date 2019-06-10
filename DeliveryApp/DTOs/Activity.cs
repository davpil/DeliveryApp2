using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryApp.DTOs
{
    public class Activity : Dto<string>
    {
        /// <summary>
        /// Group definition activity will have child activities
        /// </summary>
        public IEnumerable<Activity> Children { get; set; }

        /// <summary>
        /// Indicate that Activity should be shown as selected in the View
        /// </summary>
        [DefaultValue(false)]
        public bool Selected { get; set; }
    }
}
