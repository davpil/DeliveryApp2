using DeliveryApp.Synchro.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryApp.Models
{
    public class DynamicMenuEntity:Entity<int>, IStringifyable
    {
        /// <summary>
        /// Title
        /// </summary>
        //[Required(ErrorMessage = "Title length must be from 2 to 100 symbols")]
        //[StringLength(100, MinimumLength = 2)]
        //[Display(Prompt = "Title", Name = "Title")]
        public string Title { get; set; }

        /// <summary>
        /// Underlying API
        /// </summary>
        public string Api { get; set; }

        /// <summary>
        /// Icon file name
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        /// Order in the lists. Could be a fractional number, so,
        /// for inserting between 1 and 2, you can assign 1.5 or 1.1111111
        /// </summary>
        public double Order { get; set; } = 10;

        /// <summary>
        /// Parent ID of the Menu Item. If item is top level ParentID = 0
        /// </summary>
        public int? ParentID { get; set; } = null;

        /// <summary>
        /// List of SubItems if Menu Item is Top Level item
        /// </summary>
        [ForeignKey("ParentID")]
        public List<DynamicMenuEntity> SubItems { get; set; } = new List<DynamicMenuEntity>();

        /// <summary>
        /// List of activities which required for access to this item
        /// </summary>
        [Display(Name = "Activities")]
        public List<DynamicMenuActivityEntity> DynamicMenuActivityEntities { get; set; }

        public string Stringify()
        {
            StringBuilder val = new StringBuilder($"{Title}+{Api}+{Order}");
            DynamicMenuActivityEntities?.ForEach(mae => val.Append($"+{mae.ActivityEntityID}"));
            SubItems?.ForEach(me => val.Append($",{me.Stringify()}"));
            return val.ToString();
        }
    }
}

