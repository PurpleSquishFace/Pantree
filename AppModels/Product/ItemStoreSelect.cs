using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pantree.Core.AppModels
{
    public class ItemStoreSelect
    {
        public int ProductID { get; set; }
        public int StoreID { get; set; }

        public List<LocationView> Locations { get; set; }

        public ItemStoreSelect() { }

        public ItemStoreSelect(int productID, List<LocationView> locations)
        {
            ProductID = productID;
            Locations = locations;
        }
    }
}
