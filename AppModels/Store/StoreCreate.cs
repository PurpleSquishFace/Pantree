using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Pantree.Core.AppModels
{
    public class StoreCreate
    {
        [Display(Name = "Store Name")]
        [Required(AllowEmptyStrings = false)]
        public string StoreName { get; set; }
               
        [Display(Name = "Description")]
        public string StoreDescription { get; set; }
        
        [Display(Name = "Location")]
        [Required]
        public int LocationID { get; set; }

        public List<LocationView> Locations { get; set; }
    }
}
