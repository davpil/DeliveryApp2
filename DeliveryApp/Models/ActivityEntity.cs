﻿using DeliveryApp.Synchro.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryApp.Models
{
    public class ActivityEntity : Entity<string>, IStringifyable
    {
        /// <summary>
        /// Each entity must have ID (Primery Key) in the table.
        /// In this case ID generated by program and it is 
        /// a user friendly name of activity
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public override string ID { get; set; }

        /// <summary>
        /// Order in the lists. Could be a fractional number, so,
        /// for inserting between 1 and 2, you can assign 1.5 or 1.1111111
        /// </summary>
        [DefaultValue(10)]
        public double Order { get; set; } = 10;

        /// <summary>
        /// Some of Activities can define a group and others could belong to this group
        /// GroupID is ID of group. GroupID = 0 means that activity defines a group, othervise
        /// GroupID value specified a group to whom this activity belongs 
        /// </summary>
        public string ParentID { get; set; }

        /// <summary>
        /// Group definition activity will have child activities
        /// </summary>
        [ForeignKey("ParentID")]
        public IEnumerable<ActivityEntity> Children { get; set; }


        // Making some string value from the class data for calculating the hash
        public string Stringify()
        {
            StringBuilder val = new StringBuilder($"{ID}+{Order}");
            if (Children != null)
            {
                foreach (ActivityEntity ae in Children)
                {
                    val.Append($",{ae.Stringify()}");
                }
            }
            return val.ToString();
        }
    }
}

