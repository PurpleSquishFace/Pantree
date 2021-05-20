using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pantree.Core.AppModels
{
    public class ItemView
    {
        public int ItemID { get; set; }
        public string ItemName { get; set; }
        public int ProductID { get; set; }
        public int StoreID { get; set; }
        public int Quantity { get; set; }
        public string Notes { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
