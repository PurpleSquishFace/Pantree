using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pantree.Core.AppModels
{
    public class ShoppingListCreate
    {
        public int StoreID { get; set; }
        public int ItemID { get; set; }
        public int UserID { get; set; }
    }
}
