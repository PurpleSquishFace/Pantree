using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Pantree.Core.AppModels
{
    public class LocationCreate
    {
        [Display(Name = "Location Name")]
        [Required(AllowEmptyStrings = false)]
        public string LocationName { get; set; }

        [Display(Name = "Description")]
        public string LocationDescription { get; set; }
    }
}
