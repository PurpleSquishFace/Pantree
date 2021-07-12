using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pantree.Core.AppModels
{
    public class LocationView
    {
        public int LocationID { get; set; }
        public string LocationName { get; set; }
        public int UserID { get; set; }
        public bool LocationOwner { get; set; }

        public List<StoreView> Stores { get; set; }
    }
}
