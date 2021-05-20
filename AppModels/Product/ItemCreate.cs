using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Pantree.Core.AppModels
{
    public class ItemCreate
    {
        public int ProductID { get; set; }
        public int StoreID { get; set; }

        [Display(Name = "Name")]
        public string ItemName { get; set; }

        [Display(Name = "Store")]
        public string StoreName { get; set; }

        [Display(Name = "Current Quantity")]
        public int CurrentQuantity { get; set; }

        public int Quantity { get; set; } = 1;
        public string Notes { get; set; }
    }
}
