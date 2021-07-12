using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Pantree.Core.AppModels
{
    public class LocationShareView
    {
        [Display(Name = "Location")]
        public int SharedLocationID { get; set; }

        [Display(Name = "Friend")]
        public int SharedUserID { get; set; }
        public List<LocationView> Locations { get; set; }

        public List<UserListView> Friends { get; set; }
    }
}
