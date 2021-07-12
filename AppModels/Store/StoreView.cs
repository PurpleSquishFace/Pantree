using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pantree.Core.AppModels
{
    public class StoreView
    {
        public int StoreID { get; set; }
        public string StoreName { get; set; }
        public int LocationID { get; set; }
        public bool StoreOwner { get; set; }

        public List<ItemView> Items { get; set; }
    }
}
