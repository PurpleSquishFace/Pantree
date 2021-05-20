using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pantree.Core.Models
{
    [Table("Products.tbl_Items")]
    public class tbl_Items
    {
        [Key]
        public int ItemID { get; set; }
        public string ItemName { get; set; }
        public int ProductID { get; set; }
        public int StoreID { get; set; }
        public int Quantity { get; set; }
        public string Notes { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? LastUpdated { get; set; }
        public bool Deleted { get; set; }
        public DateTime? DeletedDate { get; set; }
    }
}
