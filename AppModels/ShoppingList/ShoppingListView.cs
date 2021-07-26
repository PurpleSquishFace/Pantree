using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pantree.Core.AppModels
{
    public class ShoppingListView
    {
        public int ShoppingListID { get; set; }
        public int UserID { get; set; }
        public int ItemID { get; set; }
        public bool Purchased { get; set; }
        public string ItemName { get; set; }
    }
}
